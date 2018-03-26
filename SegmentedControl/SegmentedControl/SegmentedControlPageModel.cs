using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using SembcorpSecure.Data;
using Xamarin.Forms;


namespace SegmentedControl
{
    public class SegmentedControlPageModel : FreshMvvm.FreshBasePageModel, INotifyPropertyChanged
    {
        public bool OneTabVisible { get; set; }

        public bool TwoTabVisible { get; set; } = true;

        public bool ThreeTabVisible { get; set; }

        public IEnumerable<string> TabTitles
        {
            get => new List<string>
        {
          "One",
          "Two",
          "Three"
        };
        }

        public int SelectedTabIndex { get; set; } = 1;

        public ICommand TabSelected
        {
            get => new Command((index) =>
            {
                OneTabVisible = (int)index == 0;
                TwoTabVisible = (int)index == 1;
                ThreeTabVisible = (int)index == 2;
            });
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is Tuple<int, ResponseType> data)
            {
                OneTabVisible = data.Item2 == ResponseType.One;
                TwoTabVisible = data.Item2 == ResponseType.Two;
                ThreeTabVisible = data.Item2 == ResponseType.Three;

                if (OneTabVisible)
                {
                    SelectedTabIndex = 0;
                }
                else if (TwoTabVisible)
                {
                    SelectedTabIndex = 1;
                }
                else if (ThreeTabVisible)
                {
                    SelectedTabIndex = 2;
                }
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public SegmentedControlPageModel()
        {
        }
    }
}
