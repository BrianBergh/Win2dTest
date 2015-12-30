using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Win2dTest
{
  public static class Win2dExtensions
  {
    public static Vector4 ToVector4(this Color color)
    {
      return new Vector4(
          (float)color.R / 255.0f,
          (float)color.G / 255.0f,
          (float)color.B / 255.0f,
          (float)color.A / 255.0f);
    }
  }
}
