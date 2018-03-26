using System;
using Xamarin.Forms;
namespace SegmentedControl.Core.Controls
{
    public class Button : Xamarin.Forms.Button
    {
        public static readonly BindableProperty PressedBackgroundColorProperty =
          BindableProperty.Create(
            propertyName: "PressedBackgroundColor",
            returnType: typeof(Color),
            declaringType: typeof(Button),
            defaultValue: default(Color));

        public static readonly BindableProperty DisabledBackgroundColorProperty =
          BindableProperty.Create(
            propertyName: "DisabledBackgroundColor",
            returnType: typeof(Color),
            declaringType: typeof(Button),
            defaultValue: default(Color));

        public static readonly BindableProperty NormalBackgroundDrawableProperty =
          BindableProperty.Create(
            propertyName: "NormalDrawable",
            returnType: typeof(FileImageSource),
            declaringType: typeof(Button),
            defaultValue: default(FileImageSource));

        public static readonly BindableProperty PressedBackgroundDrawableProperty =
          BindableProperty.Create(
            propertyName: "PressedBackgroundDrawable",
            returnType: typeof(FileImageSource),
            declaringType: typeof(Button),
            defaultValue: default(FileImageSource));

        public static readonly BindableProperty DisabledBackgroundDrawableProperty =
          BindableProperty.Create(
            propertyName: "DisabledBackgroundDrawable",
            returnType: typeof(FileImageSource),
            declaringType: typeof(Button),
            defaultValue: default(FileImageSource));

        public static readonly BindableProperty EdgeInsetsProperty = BindableProperty.Create(
          propertyName: nameof(EdgeInsets),
          returnType: typeof(EdgeInsets),
          declaringType: typeof(Entry),
          defaultValue: default(EdgeInsets));

        public static readonly BindableProperty ResizeModeProperty = BindableProperty.Create(
          propertyName: nameof(ResizeMode),
          returnType: typeof(ResizeMode),
          declaringType: typeof(Entry),
          defaultValue: default(ResizeMode));

        public static readonly BindableProperty PressedTextColorProperty =
          BindableProperty.Create(
            propertyName: "PressedTextColor",
            returnType: typeof(Color),
            declaringType: typeof(Button),
            defaultValue: default(Color));

        public static readonly BindableProperty DisabledTextColorProperty =
          BindableProperty.Create(
            propertyName: "DisabledTextColor",
            returnType: typeof(Color),
            declaringType: typeof(Button),
            defaultValue: default(Color));

        public static readonly BindableProperty CustomCornerRadiusProperty =
          BindableProperty.Create(
            propertyName: "CustomCornerRadius",
            returnType: typeof(float),
            declaringType: typeof(Button),
            defaultValue: default(float));

        public float CustomCornerRadius
        {
            get { return (float)GetValue(CustomCornerRadiusProperty); }
            set { SetValue(CustomCornerRadiusProperty, value); }
        }

        public Color PressedBackgroundColor
        {
            get { return (Color)GetValue(PressedBackgroundColorProperty); }
            set { SetValue(PressedBackgroundColorProperty, value); }
        }

        public Color DisabledBackgroundColor
        {
            get { return (Color)GetValue(DisabledBackgroundColorProperty); }
            set { SetValue(DisabledBackgroundColorProperty, value); }
        }

        public FileImageSource NormalBackgroundDrawable
        {
            get { return (FileImageSource)GetValue(NormalBackgroundDrawableProperty); }
            set { SetValue(NormalBackgroundDrawableProperty, value); }
        }

        public FileImageSource PressedBackgroundDrawable
        {
            get { return (FileImageSource)GetValue(PressedBackgroundDrawableProperty); }
            set { SetValue(PressedBackgroundDrawableProperty, value); }
        }

        public FileImageSource DisabledBackgroundDrawable
        {
            get { return (FileImageSource)GetValue(DisabledBackgroundDrawableProperty); }
            set { SetValue(DisabledBackgroundDrawableProperty, value); }
        }

        public Color SelectedTextColor
        {
            get { return (Color)GetValue(PressedTextColorProperty); }
            set { SetValue(PressedTextColorProperty, value); }
        }

        public Color DisabledTextColor
        {
            get { return (Color)GetValue(DisabledTextColorProperty); }
            set { SetValue(DisabledTextColorProperty, value); }
        }

        public EdgeInsets EdgeInsets
        {
            get { return (EdgeInsets)GetValue(EdgeInsetsProperty); }
            set { SetValue(EdgeInsetsProperty, value); }
        }

        public ResizeMode ResizeMode
        {
            get { return (ResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }
    }
}
