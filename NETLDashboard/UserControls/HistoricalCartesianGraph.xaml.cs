using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using NETLDashboard__.NET_Framework_;

//This is taken from the LiveCharts documentation, and modified to fit the project: https://lvcharts.net/App/examples/v1/wpf/Zooming%20and%20panning
namespace Wpf.CartesianChart.ZoomingAndPanning
{
    public partial class ZoomingAndPanning : INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;
        private String startDate;
        private String endDate;
        private String procedureName;

        public ZoomingAndPanning(string procedureName)
        {
            InitializeComponent();
            this.procedureName = procedureName;
            startDate = Start.SelectedDate.Value.Date.ToString("yyyyMMdd");
            endDate =   End.SelectedDate.Value.Date.ToString("yyyyMMdd");
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

            XFormatter = val => new DateTime((long)val).ToString("MM/dd/yy hh:mm");
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

        public ChartValues<DateTimePoint> GetData(String start, String end)
        {
            Db fiu = new Db();
            List<DateTimePoint> dateTimePointList = fiu.getHistoricalDataPoints(procedureName, start, end).ToList();
            var values = new ChartValues<DateTimePoint>();
            List<DateTimePoint> plottedVals = new List<DateTimePoint>(); //DateTimePoint is a LiveGraph data type used for this particular graph type

            for (int i = 0; i < dateTimePointList.Count(); i += 50)//Right now this is used to speed up load times. Once you use the geared library, compression is almost pointless.
            {
                plottedVals.Add(dateTimePointList[i]); //This adds the values from the data list, then increments the days by 1. 
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
        //This event handler resets the graph for the new date rangge once the user presses the view button again.
        private void SelectDates(object sender, RoutedEventArgs e) 
        {
            //Create DatePicker selection window, then redraw the entire graph
            if(SeriesCollection.Count != 0)
            {
                SeriesCollection.Clear();
            }
            startDate = Start.SelectedDate.Value.Date.ToString("yyyyMMdd");
            endDate = End.SelectedDate.Value.Date.ToString("yyyyMMdd");
            SeriesCollection = new SeriesCollection //Creates the series to fill the graph
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

}