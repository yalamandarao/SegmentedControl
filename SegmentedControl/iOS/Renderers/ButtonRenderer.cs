using System;
using System.ComponentModel;
using System.Reflection;
using CoreGraphics;
using SegmentedControl.Core.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SegmentedControl.Core.Controls.Button),
                          typeof(SegmentedControl.iOS.Renderers.ButtonRenderer))]
namespace SegmentedControl.iOS.Renderers
{
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        #region Properties

        UIButton _button;

        Core.Controls.Button BaseElement
        {
            get
            {
                return Element as Core.Controls.Button;
            }
        }

        #endregion

        #region Life-cycle methods

        protected override void Dispose(bool disposing)
        {
            if (disposing
                && Control != null)
            {
                Control.TouchUpInside -= TouchUpInside;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementPropertyChanged(object sender,
                                                         PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null
                || BaseElement == null)
            {
                return;
            }


            if (e.PropertyName == Core.Controls.Button.BackgroundColorProperty.PropertyName)
            {
                UpdateNormalBackgroundColor();
            }
            else if (e.PropertyName == Core.Controls.Button.CustomCornerRadiusProperty.PropertyName)
            {
                UpdatePressedBackgroundColor();
            }
            else if (e.PropertyName == Core.Controls.Button.PressedBackgroundColorProperty.PropertyName)
            {
                UpdateCornerRadius();
            }
            else if (e.PropertyName == Core.Controls.Button.DisabledTextColorProperty.PropertyName)
            {
                UpdateDisabledBackgroundColor();
            }
            else if (e.PropertyName == Core.Controls.Button.NormalBackgroundDrawableProperty.PropertyName)
            {
                UpdateNormalBackgroundDrawable();
            }
            else if (e.PropertyName == Core.Controls.Button.PressedBackgroundDrawableProperty.PropertyName)
            {
                UpdatePressedBackgroundDrawable();
            }
            else if (e.PropertyName == Core.Controls.Button.DisabledBackgroundDrawableProperty.PropertyName)
            {
                UpdateDisabledBackgroundDrawable();
            }
            else if (e.PropertyName == Core.Controls.Button.PressedTextColorProperty.PropertyName)
            {
                UpdateSelectedTextColor();
            }
            else if (e.PropertyName == Core.Controls.Button.DisabledTextColorProperty.PropertyName)
            {
                UpdateDisabledTextColor();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            InitButton();

            if (Control == null
                || BaseElement == null)
            {
                return;
            }
            Control.Layer.CornerRadius = (nfloat)5.0;
            UpdateDisabledBackgroundDrawable();
            UpdatePressedBackgroundDrawable();
            UpdateNormalBackgroundDrawable();
            UpdateNormalBackgroundColor();
            UpdatePressedBackgroundColor();
            UpdateDisabledBackgroundColor();
            UpdateCornerRadius();
            UpdateDisabledTextColor();
            UpdateDisabledTextColor();
            UpdateSelectedTextColor();
        }

        #endregion

        #region Internal methods

        void InitButton()
        {
            if (_button == null
                && Control != null)
            {
                _button = new UIButton(UIButtonType.Custom);
                SetNativeControl(_button);
                Control.TouchUpInside += TouchUpInside;

                InvokeMethod(this, "UpdateText", null);
                InvokeMethod(this, "UpdateFont", null);
                InvokeMethod(this, "UpdateBorder", null);
                InvokeMethod(this, "UpdateImage", null);
                InvokeMethod(this, "UpdateTextColor", null);
            }
        }

        void TouchUpInside(object sender, EventArgs e)
        {
            InvokeMethod(this, "OnButtonTouchUpInside", sender, e);
        }

        void UpdateNormalBackgroundColor()
        {
            if (BaseElement.BackgroundColor == Color.Default)
            {
                return;
            }

            UIImage source = ImageFromColor(BaseElement.BackgroundColor.ToUIColor());
            Control.SetBackgroundImage(source, UIControlState.Normal);
        }

        void UpdatePressedBackgroundColor()
        {
            if (BaseElement.PressedBackgroundColor == Color.Default)
            {
                return;
            }

            UIImage source = ImageFromColor(BaseElement.PressedBackgroundColor.ToUIColor());
            Control.SetBackgroundImage(source, UIControlState.Highlighted);
            Control.SetBackgroundImage(source, UIControlState.Selected);
        }

        void UpdateCornerRadius()
        {
            Control.Layer.CornerRadius = (nfloat)BaseElement.CustomCornerRadius;
            Control.ClipsToBounds = true;
        }

        void UpdateDisabledBackgroundColor()
        {
            if (BaseElement.DisabledBackgroundColor == Color.Default)
            {
                return;
            }

            UIImage source = ImageFromColor(BaseElement.DisabledBackgroundColor.ToUIColor());
            Control.SetBackgroundImage(source, UIControlState.Disabled);
        }

        UIImage ImageFromColor(UIColor color)
        {
            CGRect rect = new CGRect(0.0f, 0.0f, 1.0f, 1.0f);
            UIGraphics.BeginImageContext(rect.Size);
            CGContext context = UIGraphics.GetCurrentContext();

            context.SetFillColor(color.CGColor);
            context.FillRect(rect);

            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }

        void UpdateNormalBackgroundDrawable()
        {
            string drawableName = BaseElement.NormalBackgroundDrawable?.File;
            if (drawableName == null)
            {
                return;
            }

            UIImage source = CreateBackgroundImage(drawableName);
            Control.SetBackgroundImage(source, UIControlState.Normal);
        }

        void UpdatePressedBackgroundDrawable()
        {
            string drawableName = BaseElement.PressedBackgroundDrawable?.File;
            if (drawableName == null)
            {
                return;
            }

            UIImage source = CreateBackgroundImage(drawableName);

            Control.AdjustsImageWhenHighlighted = source != null;

            Control.SetBackgroundImage(source, UIControlState.Highlighted);
            Control.SetBackgroundImage(source, UIControlState.Selected);
        }

        void UpdateDisabledBackgroundDrawable()
        {
            string drawableName = BaseElement.DisabledBackgroundDrawable?.File;
            if (drawableName == null)
            {
                return;
            }

            UIImage source = CreateBackgroundImage(drawableName);
            Control.SetBackgroundImage(source, UIControlState.Disabled);
        }

        UIImage CreateBackgroundImage(string drawableName)
        {
            UIImage source = UIImage.FromBundle(drawableName);
            if (!Equals(BaseElement.EdgeInsets, default(EdgeInsets)))
            {
                var mode = BaseElement.ResizeMode == ResizeMode.Stretch
                          ? UIImageResizingMode.Stretch
                          : UIImageResizingMode.Tile;

                source = source.CreateResizableImage(
                  new UIEdgeInsets(BaseElement.EdgeInsets.Top,
                                   BaseElement.EdgeInsets.Left,
                                   BaseElement.EdgeInsets.Bottom,
                                   BaseElement.EdgeInsets.Right),
                  mode
                );
                Control.ContentMode = UIViewContentMode.ScaleToFill;
            }
            return source;
        }

        void UpdateDisabledTextColor()
        {
            UIColor color = BaseElement.DisabledTextColor.ToUIColor();
            Control.SetTitleColor(color, UIControlState.Disabled);
        }

        void UpdateSelectedTextColor()
        {
            UIColor color = (BaseElement.SelectedTextColor == Color.Default
                            ? BaseElement.TextColor
                            : BaseElement.SelectedTextColor).ToUIColor();

            Control.SetTitleColor(color, UIControlState.Highlighted);
            Control.SetTitleColor(color, UIControlState.Selected);
        }

        #endregion

        public object InvokeMethod(object target, string methodName, params object[] args)
        {
            Type t = target.GetType();
            MethodInfo mi = null;

            while (t != null)
            {
                mi = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

                if (mi != null) break;

                t = t.BaseType;
            }

            if (mi == null)
            {
                throw new Exception(string.Format("Method '{0}' not found in type hierarchy.", methodName));
            }
            return mi.Invoke(target, args);
        }
    }
}
