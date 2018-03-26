
using Xamarin.Forms;

namespace SegmentedControl.Core.Controls
{
    /// <summary>
    /// Edge insets class definition specific for iOS only.
    /// </summary>
    [TypeConverter(typeof(EdgeInsetsTypeConverter))]
    public struct EdgeInsets
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public float Bottom { get; set; }
        public float Right { get; set; }

        public EdgeInsets(float top, float left, float bottom, float right) : this()
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        bool Equals(EdgeInsets other)
        {
            return Top.Equals(other.Top)
                  && Left.Equals(other.Left)
                  && Bottom.Equals(other.Bottom)
                  && Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is EdgeInsets && Equals((EdgeInsets)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(EdgeInsets left, EdgeInsets right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EdgeInsets left, EdgeInsets right)
        {
            return !left.Equals(right);
        }

        public void Deconstruct(out double top, out double left, out double bottom, out double right)
        {
            top = Top;
            left = Left;
            bottom = Bottom;
            right = Right;
        }
    }
}
