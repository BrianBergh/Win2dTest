﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Win2dTest
{
  public class FPSCounter
  {
    private DateTime lastTick = DateTime.Now;
    private float fps = 0;
    private float fpsCount = 0;
    public Vector2 Position { get; set; } = new Vector2(2, 2);
    public Color Color { get; set; } = Colors.Yellow;
    public CanvasTextFormat TextFormat { get; set; } = new CanvasTextFormat { FontSize = 12.0f, WordWrapping = CanvasWordWrapping.NoWrap };
    public string Header { get; set; } = null;
    public bool CountInDrawMethod { get; set; } = false;
    public void CountFPS()
    {
      TimeSpan ft = (DateTime.Now - lastTick);
      if (ft.TotalMilliseconds >= 1000)
      {
        fps = fpsCount;
        fpsCount = 0;
        lastTick = DateTime.Now;
      }
      else {
        fpsCount++;
      }
    }
    public void Draw(CanvasDrawingSession session)
    {
      try
      {
        if (CountInDrawMethod)
          CountFPS();
        session.DrawText((string.IsNullOrEmpty(Header) ? "" : Header +":") + fps.ToString("0") + " FPS", Position, Color, TextFormat);
      }
      catch
      { }
    }
  }
}
