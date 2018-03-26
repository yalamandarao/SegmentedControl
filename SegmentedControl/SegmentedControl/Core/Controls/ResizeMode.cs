using System;
using Xamarin.Forms;

namespace SegmentedControl.Core.Controls
{
    [TypeConverter(typeof(ResizeModeTypeConverter))]
    public enum ResizeMode
    {
        Stretch,
        Tile
    }

    public class ResizeModeTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                if (Enum.TryParse(value, out TextAlignment direction))
                    return direction;

                if (value.Equals("stretch", StringComparison.OrdinalIgnoreCase))
                {
                    return ResizeMode.Stretch;
                }
                if (value.Equals("tile", StringComparison.OrdinalIgnoreCase))
                {
                    return ResizeMode.Tile;
                }
            }
            throw new InvalidOperationException(string.Format(
              "Cannot convert \"{0}\" into {1}", value,
              typeof(ResizeMode)));
        }
    }
}
