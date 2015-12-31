/*
  Written by Brian Niels Bergh Dec. 2015
  GD Software
  http://www.gdsoftware.dk
*/
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Win2dTest
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    Vector2 scrollPos = new Vector2();
    float speed = 100f;
    float tw = 0;
    string about = "This is just a scrolling demo with a very long text to see how it works on Windows Phone (Lumia 950 XL) vs Desktop.";
    CanvasTextFormat scroller_format = new CanvasTextFormat { FontSize = 50.0f, WordWrapping = CanvasWordWrapping.NoWrap };
    CanvasTextLayout scroller_layout = null;
    FPSCounter fpsCounter = new FPSCounter();
    CanvasRenderTarget scrollerTarget;
    bool showW2DFpsCounter = true;
    bool useRenderTarget = false;


    public MainPage()
    {
      this.InitializeComponent();
      SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
      {
        App.Current.Exit();
      };

    }

    private void canvas_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
    {
      float elapsed = (float)args.Timing.ElapsedTime.TotalSeconds; // cast once, for speed.

      scrollPos.X -= (elapsed * speed);
      if ((useRenderTarget ? (scrollPos.X < -scrollerTarget.Bounds.Width) : (scrollPos.X < -scroller_layout.LayoutBounds.Width)))
        scrollPos.X = tw;
    }

    private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
    {

      if (useRenderTarget)
        args.DrawingSession.DrawImage(scrollerTarget, scrollPos);
      else
        args.DrawingSession.DrawTextLayout(scroller_layout, scrollPos, Colors.White);

      if (showW2DFpsCounter)
        fpsCounter.Draw(args.DrawingSession);

    }

    private void canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
    {
      scroller_layout = new CanvasTextLayout(canvas, about, scroller_format, 0.0f, 0.0f);
      scrollPos = new Vector2((float)canvas.ActualWidth, 2f/*((float)canvas.ActualHeight / 2) - ((float)scroller_layout.DrawBounds.Height / 2)*/);
      tw = (float)canvas.ActualWidth;
      
      // create renderTarget
      scrollerTarget = new CanvasRenderTarget(canvas, (float)scroller_layout.LayoutBounds.Width, (float)scroller_layout.LayoutBounds.Height);
      using (CanvasDrawingSession drawingSession = scrollerTarget.CreateDrawingSession())
      {
        drawingSession.Clear(Colors.Transparent);
        drawingSession.DrawTextLayout(scroller_layout, new Vector2(0,0), Colors.White);
      }
    }
    private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      // Instead of dispatching to ActualWidth in each update event, we grab the width here.
      tw = (float)canvas.ActualWidth;
    }

    private void chkWin2DFPS_Click(object sender, RoutedEventArgs e)
    {
      showW2DFpsCounter = chkWin2DFPS.IsChecked.Value; // again, to prevent accessing this in a dispatcher (in the draw event)
    }

    private void chkUWPFPS_Click(object sender, RoutedEventArgs e)
    {
      App.Current.DebugSettings.EnableFrameRateCounter = chkUWPFPS.IsChecked.Value;
    }

    private void chkRenderTarget_Click(object sender, RoutedEventArgs e)
    {
      useRenderTarget = chkRenderTarget.IsChecked.Value;
    }
  }
}
