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
    public MainPage()
    {
      this.InitializeComponent();
      
    }
    Vector2 scrollPos = new Vector2();
    float speed = 100f;
    float tw = 0;
    private void canvas_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
    {
      float elapsed = (float)args.Timing.ElapsedTime.TotalSeconds; // calc once, for speed.
      scrollPos.X -= (elapsed * speed);
      //if (scrollPos.X < -scroller_layout.DrawBounds.Width)
      if (scrollPos.X < -scrollerTarget.Bounds.Width)
      {
        scrollPos.X = tw;
      }
    }
    string about = "This is just a scrolling demo";// with a very long text to see how it works on Windows Phone (Lumia 950 XL).";
    CanvasTextFormat scroller_format = new CanvasTextFormat { FontSize = 50.0f, WordWrapping = CanvasWordWrapping.NoWrap };
    CanvasTextLayout scroller_layout = null;
    //FPSCounter fpsCounter = new FPSCounter();
    CanvasRenderTarget scrollerTarget;
    private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
    {
      args.DrawingSession.DrawImage(scrollerTarget, scrollPos);
      //fpsCounter.Draw(args.DrawingSession);
    }
    private void canvas_Loaded(object sender, RoutedEventArgs e)
    {

    }
    private void canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
    {
      scroller_layout = new CanvasTextLayout(canvas, about, scroller_format, 0.0f, 0.0f);
      scrollPos = new Vector2((float)canvas.ActualWidth, ((float)canvas.ActualHeight / 2) - ((float)scroller_layout.DrawBounds.Height / 2));
      tw = (float)canvas.ActualWidth;

      scrollerTarget = new CanvasRenderTarget(canvas, (float)scroller_layout.LayoutBounds.Width, (float)scroller_layout.LayoutBounds.Height);
      using (CanvasDrawingSession drawingSession = scrollerTarget.CreateDrawingSession())
      {
        drawingSession.Clear(Colors.Blue);
        drawingSession.DrawTextLayout(scroller_layout, new Vector2(0,0), Colors.White);
      }
    }
    private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {

      tw = (float)canvas.ActualWidth;
    }
  }
}
