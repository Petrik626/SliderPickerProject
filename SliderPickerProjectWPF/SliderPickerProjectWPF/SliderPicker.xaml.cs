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
        public static readonly DependencyProperty ContentSliderProperty;
        #endregion
        #region STATIC EVENTS
        public static readonly RoutedEvent TitleChangedEvent;
        public static readonly RoutedEvent SliderValueChangedEvent;
        public static readonly RoutedEvent ContentSliderChangedEvent;
        #endregion
        #region CONSTRUCTORS
        static SliderPicker()
        {
            FrameworkPropertyMetadata titleMetadata = new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleChanged);
            FrameworkPropertyMetadata sliderMetadata = new FrameworkPropertyMetadata(new double(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSliderValueChanged);
            FrameworkPropertyMetadata contentMetadata = new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentSliderChanged);


            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SliderPicker), titleMetadata);
            SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderPicker), sliderMetadata);
            ContentSliderProperty = DependencyProperty.Register("ContentSlider", typeof(string), typeof(SliderPicker), contentMetadata);

            TitleChangedEvent = EventManager.RegisterRoutedEvent("TitleChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            SliderValueChangedEvent = EventManager.RegisterRoutedEvent("SliderValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            ContentSliderChangedEvent = EventManager.RegisterRoutedEvent("ContentSliderChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
        }

        public SliderPicker()
        {
            InitializeComponent();
        }
        #endregion
        #region STATIC METHODS
        private static void OnSliderValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = (SliderPicker)sender;
            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            sliderPicker.SliderValue = (double)e.NewValue;
            sliderPicker.ContentSlider = ((double)e.NewValue).ToString();
            args.RoutedEvent = SliderValueChangedEvent;

            sliderPicker.RaiseEvent(args);
        }

        private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = (SliderPicker)sender;

            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            sliderPicker.Title = (string)e.NewValue;
            args.RoutedEvent = TitleChangedEvent;

            sliderPicker.RaiseEvent(args);
        }

        private static void OnContentSliderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = (SliderPicker)sender;
            RoutedPropertyChangedEventArgs<string> args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());

            args.RoutedEvent = ContentSliderChangedEvent;
            sliderPicker.ContentSlider = (string)e.NewValue;
            sliderPicker.RaiseEvent(args);
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

        public string ContentSlider
        {
            get => (string)GetValue(ContentSliderProperty);
            set => SetValue(ContentSliderProperty, value);
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

        public event RoutedPropertyChangedEventHandler<string> ContentSliderChanged
        {
            add { AddHandler(ContentSliderChangedEvent, value); }
            remove { RemoveHandler(ContentSliderChangedEvent, value); }
        }
        #endregion
    }
}
