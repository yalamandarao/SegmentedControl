using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SegmentedControl.Core.Controls.Button),
                          typeof(SegmentedControl.Droid.Renderers.ButtonRenderer))]
namespace SegmentedControl.Droid.Renderers
{
    public class ButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        #region Properties

        Core.Controls.Button BaseElement
        {
            get { return Element as Core.Controls.Button; }
        }

        #endregion

        #region Initializations

        public ButtonRenderer(Context context) : base(context)
        {
        }

        #endregion

        #region Life-cycle methods

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null
                || Element == null)
            {
                return;
            }

            if (e.PropertyName == Core.Controls.Button.NormalBackgroundDrawableProperty.PropertyName
                || e.PropertyName == Core.Controls.Button.PressedBackgroundDrawableProperty.PropertyName
                || e.PropertyName == Core.Controls.Button.DisabledBackgroundDrawableProperty.PropertyName)
            {
                UpdateBackgroundDrawable();
            }
            else if (e.PropertyName == Core.Controls.Button.BackgroundColorProperty.PropertyName
                     || e.PropertyName == Core.Controls.Button.PressedBackgroundColorProperty.PropertyName
                     || e.PropertyName == Core.Controls.Button.DisabledTextColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
            else if (e.PropertyName == Core.Controls.Button.PressedTextColorProperty.PropertyName
                     || e.PropertyName == Core.Controls.Button.DisabledTextColorProperty.PropertyName
                     || e.PropertyName == Core.Controls.Button.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }

            else if (e.PropertyName == Core.Controls.Button.CustomCornerRadiusProperty.PropertyName)
            {
                SetCornerRadius();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null
                || Element == null)
            {
                return;
            }


            Control.SetPadding(0, 0, 0, 0);
            UpdateBackgroundDrawable();
            UpdateBackgroundColor();
            UpdateTextColor();
            SetCornerRadius();

        }

        #endregion

        #region Internal methods

        void UpdateBackgroundDrawable()
        {
            string drawableName = BaseElement.NormalBackgroundDrawable?.File;
            string pressedDrawable = BaseElement.PressedBackgroundDrawable?.File;
            string disabledDrawable = BaseElement.DisabledBackgroundDrawable?.File;
            if (drawableName != null
                || pressedDrawable != null
                || disabledDrawable != null)
            {
                var stateListDrawable = new StateListDrawable();

                if (disabledDrawable != null)
                {
                    var drawable = Context.GetDrawable(disabledDrawable);
                    stateListDrawable.AddState(
                      new[] { -Android.Resource.Attribute.StateEnabled },
                      drawable);
                }

                if (pressedDrawable != null)
                {
                    var drawable = Context.GetDrawable(pressedDrawable);
                    stateListDrawable.AddState(
                      new[] { Android.Resource.Attribute.StateEnabled,
                    Android.Resource.Attribute.StatePressed},
                      drawable);
                }

                if (drawableName != null)
                {
                    var drawable = Context.GetDrawable(drawableName);
                    stateListDrawable.AddState(
                      new[] { Android.Resource.Attribute.StateEnabled },
                      drawable);
                }
                Control.Background = stateListDrawable;
            }
        }

        new void UpdateBackgroundColor()
        {
            base.UpdateBackgroundColor();

            Android.Graphics.Color normalColor = BaseElement.BackgroundColor.ToAndroid();
            Android.Graphics.Color pressedColor = BaseElement.PressedBackgroundColor.ToAndroid();
            Android.Graphics.Color disabledColor = BaseElement.DisabledBackgroundColor.ToAndroid();

            if (pressedColor != 0
                || disabledColor != 0)
            {
                var stateListColor = new StateListDrawable();

                if (disabledColor != 0)
                {
                    stateListColor.AddState(
                      new[] {
              -Android.Resource.Attribute.StateEnabled
                      },
                      new ColorDrawable(disabledColor));
                }

                if (pressedColor != 0)
                {
                    stateListColor.AddState(
                      new[] {
              Android.Resource.Attribute.StateEnabled,
              Android.Resource.Attribute.StatePressed
                      },
                      new ColorDrawable(pressedColor));
                }

                stateListColor.AddState(new[] { Android.Resource.Attribute.StateEnabled },
                                        new ColorDrawable(normalColor));

                Control.SetBackground(stateListColor);
            }
        }

        void UpdateTextColor()
        {
            int normalColor = BaseElement.TextColor.ToAndroid();
            int selectedColor = BaseElement.SelectedTextColor.ToAndroid();
            int disabledColor = BaseElement.DisabledTextColor.ToAndroid();

            var stateListColor = new ColorStateList(new int[][] {
        new int[] { -Android.Resource.Attribute.StateEnabled
        },
        new int[] { Android.Resource.Attribute.StateEnabled,
                    Android.Resource.Attribute.StatePressed
        },
        new int[] { Android.Resource.Attribute.StateEnabled,
                    Android.Resource.Attribute.StateSelected
        },
        new int[] { Android.Resource.Attribute.StateEnabled
        }
      },
            new int[] {
        disabledColor == 0 ? normalColor : disabledColor,
        selectedColor == 0 ? normalColor : selectedColor,
        selectedColor == 0 ? normalColor : selectedColor,
        normalColor,
            });

            Control.SetTextColor(stateListColor);
        }



        void SetCornerRadius()
        {
            var radius = (float)BaseElement.CustomCornerRadius;
            if (!(radius > 0))
            {
                return;
            }
            base.UpdateBackgroundColor();
            Android.Graphics.Color normalColor = BaseElement.BackgroundColor.ToAndroid();
            Android.Graphics.Color pressedColor = BaseElement.PressedBackgroundColor.ToAndroid();

            int[][] states = new int[2][]
                      {
        new int[] { Android.Resource.Attribute.StatePressed, 0},
        new int[] { Android.Resource.Attribute.StateEnabled, 0}
                      };

            int[] colors = new int[2]
                      {
        pressedColor,
        normalColor
            };

            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetColor(new Android.Content.Res.ColorStateList(states, colors));
            gradientDrawable.SetCornerRadius(BaseElement.CustomCornerRadius * 2);
            Control.SetBackground(gradientDrawable);
        }

        #endregion
    }
}
