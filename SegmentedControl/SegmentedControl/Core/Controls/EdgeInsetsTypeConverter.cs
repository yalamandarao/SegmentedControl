using System;
using System.Globalization;
using Xamarin.Forms;

namespace SegmentedControl.Core.Controls
{
    public class EdgeInsetsTypeConverter : TypeConverter
    {
        public EdgeInsetsTypeConverter()
        {
        }

        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(EdgeInsets)}");
            }

            value = value.Trim();

            string[] insets;

            /* XAML */
            if (value.Contains(",")
                && (insets = value.Split(',')).Length == 4
                && TryParse(insets[0], out float t)
                && TryParse(insets[1], out float l)
                && TryParse(insets[2], out float b)
                && TryParse(insets[3], out float r))
            {
                return new EdgeInsets(t, l, b, r);
            }
            /* CSS */
            else if (value.Contains(" ")
                     && (insets = value.Split(' ')).Length == 4
                     && TryParse(insets[0], out t)
                     && TryParse(insets[1], out r)
                     && TryParse(insets[2], out b)
                     && TryParse(insets[3], out l))
            {
                return new EdgeInsets(t, l, b, r);
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(EdgeInsets)}");
        }

        bool TryParse(string val, out float result)
        {
            return float.TryParse(val, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }
    }
}

