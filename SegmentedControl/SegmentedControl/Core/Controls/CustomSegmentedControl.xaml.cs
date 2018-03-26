using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using SegmentedControl.Core.Controls;

namespace SegmentedControl.Mobile.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSegmentedControl : ContentView
    {

        #region Properties

        public IEnumerable<string> ItemSource
        {
            get { return (IEnumerable<string>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public ICommand IndexChangedCommand
        {
            get { return (ICommand)GetValue(IndexChangedCommandProperty); }
            set { SetValue(IndexChangedCommandProperty, value); }
        }

        #endregion

        #region Private Properties

        List<Button> _tabButtons = new List<Button>();

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty IndexChangedCommandProperty = BindableProperty.Create(
          nameof(IndexChangedCommand),
          typeof(ICommand),
            typeof(CustomSegmentedControl),
          default(ICommand),
          BindingMode.TwoWay);

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
          nameof(ItemSource),
          typeof(IEnumerable<string>),
            typeof(CustomSegmentedControl),
          default(IEnumerable<string>),
          BindingMode.TwoWay,
          propertyChanged: (bindable, oldValue, newValue) => {

            var control = (CustomSegmentedControl)bindable;
              control.LayoutButtons();
          });

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
          nameof(SelectedIndex),
          typeof(int),
            typeof(CustomSegmentedControl),
          default(int),
          BindingMode.TwoWay,
          propertyChanged: (bindable, oldValue, newValue) => {

            var control = (CustomSegmentedControl)bindable;

              if (oldValue != newValue)
              {
                  control._tabButtons[(int)oldValue].IsEnabled = true;
                  control._tabButtons[(int)newValue].IsEnabled = false;
                  control.IndexChangedCommand?.Execute((int)newValue);
              }
          });

        #endregion

        #region Internal methods

        void LayoutButtons()
        {
            var itemCount = ItemSource?.Count() ?? 0;
            var buttonCount = _tabButtons.Count();
            var newTabButtons = new List<Button>();

            relativeLayout.Children.Clear();

            if (itemCount == 0)
            {
                return;
            }

            for (var idx = 0; idx < itemCount; idx++)
            {
                var button = (idx < buttonCount) ? _tabButtons[idx] : null;

                if (idx == 0)
                {
                    button = MakeLeftButton(button, ItemSource.ElementAt(idx));
                }
                else if (idx == itemCount - 1)
                {
                    button = MakeRightButton(button, ItemSource.ElementAt(idx));
                }
                else
                {
                    button = MakeCenterButton(button, ItemSource.ElementAt(idx));
                }

                button.IsEnabled = true;
                button.CommandParameter = idx;
                button.Command = new Command((arg) =>
                {
                    SelectedIndex = (int)arg;
                });

                newTabButtons.Add(button);

                if (idx == 0)
                {
                    relativeLayout.Children.Add(button,
                      Constraint.Constant(0),
                      Constraint.Constant(0),
                      Constraint.RelativeToParent(
                        (parent) => itemCount == 0 ? 0 : parent.Width / itemCount),
                      Constraint.RelativeToParent((parent) => parent.Height));
                }
                else
                {
                    relativeLayout.Children.Add(button,
                      Constraint.RelativeToView(newTabButtons[idx - 1],
                        (r, v) => itemCount == 0 ? 0 : v.X + r.Width / itemCount),
                      Constraint.Constant(0),
                      Constraint.RelativeToParent(
                        (parent) => itemCount == 0 ? 0 : parent.Width / itemCount),
                      Constraint.RelativeToParent((parent) => parent.Height));
                }
            }
            _tabButtons = newTabButtons;
            _tabButtons[SelectedIndex].IsEnabled = false;
        }

        Button MakeLeftButton(Button button = null, string text = "")
        {
            if (button == null)
            {
                button = new Button();
            }

            button.EdgeInsets = new EdgeInsets { Top = 10, Left = 10, Right = 10, Bottom = 10 };
            button.DisabledBackgroundDrawable = "tab_left_selected";
            button.NormalBackgroundDrawable = "tab_left_normal";
            button.PressedBackgroundDrawable = "tab_left_selected";
            button.TextColor = (Color)Application.Current.Resources["BrightBlue"];
            button.DisabledTextColor = Color.White;
            button.SelectedTextColor = Color.White;
            button.Text = text;
            button.Font = Font.SystemFontOfSize(13, FontAttributes.Bold);
            return button;
        }

        Button MakeRightButton(Button button = null, string text = "")
        {
            if (button == null)
            {
                button = new Button();
            }

            button.EdgeInsets = new EdgeInsets { Top = 10, Left = 10, Right = 10, Bottom = 10 };
            button.DisabledBackgroundDrawable = "tab_right_selected";
            button.NormalBackgroundDrawable = "tab_right_normal";
            button.PressedBackgroundDrawable = "tab_right_selected";
            button.TextColor = (Color)Application.Current.Resources["BrightBlue"];
            button.DisabledTextColor = Color.White;
            button.SelectedTextColor = Color.White;
            button.Text = text;
            button.Font = Font.SystemFontOfSize(13, FontAttributes.Bold);
            return button;
        }

        Button MakeCenterButton(Button button = null, string text = "")
        {
            if (button == null)
            {
                button = new Button();
            }

            button.EdgeInsets = new EdgeInsets { Top = 10, Left = 10, Right = 10, Bottom = 10 };
            button.Margin = new Thickness { Left = -1 };
            button.DisabledBackgroundDrawable = "tab_center_selected";
            button.NormalBackgroundDrawable = "tab_center_normal";
            button.PressedBackgroundDrawable = "tab_center_selected";
            button.TextColor = (Color)Application.Current.Resources["BrightBlue"];
            button.DisabledTextColor = Color.White;
            button.SelectedTextColor = Color.White;
            button.Text = text;
            button.Font = Font.SystemFontOfSize(13, FontAttributes.Bold);
            return button;
        }

        #endregion

        public CustomSegmentedControl()
        {
            InitializeComponent();
        }
    }
}
