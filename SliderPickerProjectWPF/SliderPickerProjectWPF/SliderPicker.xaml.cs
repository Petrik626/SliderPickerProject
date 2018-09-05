using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SliderPickerProjectWPF
{
    /// <summary>
    /// Логика взаимодействия для SliderPicker.xaml
    /// </summary>
    public partial class SliderPicker : UserControl
    {
        #region STATIC PROPERTIES
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty SliderValueProperty;
        #endregion

        #region STATIC EVENTS
        public static readonly RoutedEvent TitleChangedEvent;
        public static readonly RoutedEvent SliderValueChangedEvent;
        #endregion
        #region CONSTRUCTORS
        static SliderPicker()
        {
            FrameworkPropertyMetadata titleMetadata = new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleChanged);
            FrameworkPropertyMetadata sliderMetadata = new FrameworkPropertyMetadata(new double(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSliderValueChanged);

            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SliderPicker), titleMetadata);
            SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderPicker), sliderMetadata);

            TitleChangedEvent = EventManager.RegisterRoutedEvent("TitleChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            SliderValueChangedEvent = EventManager.RegisterRoutedEvent("SliderValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
        }

        public SliderPicker()
        {
            InitializeComponent();
        }
        #endregion

        #region STATIC METHODS
        private static void OnSliderValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker slider = (SliderPicker)sender;

            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            slider.SliderValue = (double)e.NewValue;
            args.RoutedEvent = SliderValueChangedEvent;

            slider.RaiseEvent(args);
        }

        private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker slider = (SliderPicker)sender;

            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            slider.Title = (string)e.NewValue;
            args.RoutedEvent = TitleChangedEvent;

            slider.RaiseEvent(args);
        }
        #endregion

        #region PROPERTIES
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public double SliderValue
        {
            get => (double)GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }
        #endregion

        #region EVENST
        public event RoutedPropertyChangedEventHandler<double> SliderValueChanged
        {
            add { AddHandler(SliderValueChangedEvent, value); }
            remove { RemoveHandler(SliderValueChangedEvent, value); }
        }

        public event RoutedPropertyChangedEventHandler<string> TitleChanged
        {
            add { AddHandler(TitleChangedEvent, value); }
            remove { RemoveHandler(TitleChangedEvent, value); }
        }
        #endregion
    }
}
