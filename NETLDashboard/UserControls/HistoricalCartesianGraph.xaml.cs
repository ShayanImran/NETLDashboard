using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using NETLDashboard__.NET_Framework_;
using NETLDashboard;

namespace Wpf.CartesianChart.ZoomingAndPanning
{
    public partial class ZoomingAndPanning : INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;
        private String startDate;
        private String endDate;       

        public ZoomingAndPanning()
        {
            InitializeComponent();
            startDate = Start.SelectedDate.Value.Date.ToString("yyyyMMdd");
            endDate = End.SelectedDate.Value.Date.ToString("yyyyMMdd");
            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(33, 148, 241), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = GetData(startDate,endDate),
                    //Fill = gradientBrush,
                    StrokeThickness = 0,
                    PointGeometrySize = 3
                }
            };

            ZoomingMode = ZoomingOptions.X;

            XFormatter = val => new DateTime((long)val).ToString("MM dd yy");
            YFormatter = val => val.ToString("G");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged();
            }
        }

        private void ToogleZoomingMode(object sender, RoutedEventArgs e)
        {
            switch (ZoomingMode)
            {
                case ZoomingOptions.None:
                    ZoomingMode = ZoomingOptions.X;
                    break;
                case ZoomingOptions.X:
                    ZoomingMode = ZoomingOptions.Y;
                    break;
                case ZoomingOptions.Y:
                    ZoomingMode = ZoomingOptions.Xy;
                    break;
                case ZoomingOptions.Xy:
                    ZoomingMode = ZoomingOptions.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ChartValues<DateTimePoint> GetData(String start, String end)
        {
            Db fiu = new Db();
            var values = new ChartValues<DateTimePoint>();
            List<double> data = fiu.getVirtualHistoricalData(start, end).ToList(); //Copies the data returned by the database and stores it in a list
            List<double> temp = new List<double>();
            List<DateTimePoint> plottedVals = new List<DateTimePoint>();
            for(int i = 0; i < data.Count(); i+= 5)
            {
                if(temp.Count == 0 || temp.Last() != data[i] && temp.Last() != data[i] - 5 && temp.Last() != data[i] + 5)
                {
                    temp.Add(data[i]); //This adds the values from the data list, then increments the days by 1.
                }
            }
            for(int i = 0; i < temp.Count(); i+=5)
            {
                plottedVals.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), temp[i]));
            }
            values.AddRange(plottedVals);
            return values;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.

            X.MinValue = double.NaN;
            X.MaxValue = double.NaN;
            Y.MinValue = double.NaN;
            Y.MaxValue = double.NaN;
        }

        private void SelectDates(object sender, RoutedEventArgs e)
        {
            //Create DatePicker selection window, then redraw the entire graph
            if(SeriesCollection.Count != 0)
            {
                SeriesCollection.Clear();
            }
            startDate = Start.SelectedDate.Value.Date.ToString("yyyyMMdd");
            endDate = End.SelectedDate.Value.Date.ToString("yyyyMMdd");
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = GetData(startDate,endDate),
                    StrokeThickness = 0,
                    PointGeometrySize = 3
                }
            };

            ZoomingMode = ZoomingOptions.X;

            XFormatter = val => new DateTime((long)val).ToString("dd MMM");
            YFormatter = val => val.ToString("G");

            DataContext = this;
        }

    }

    public class ZoomingModeCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ZoomingOptions)value)
            {
                case ZoomingOptions.None:
                    return "None";
                case ZoomingOptions.X:
                    return "X";
                case ZoomingOptions.Y:
                    return "Y";
                case ZoomingOptions.Xy:
                    return "XY";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}