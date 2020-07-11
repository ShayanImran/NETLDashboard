﻿using System;
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

namespace NETLDashboard
{
    public partial class HistoricalGraph : INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;
        private String startDate;
        private String endDate;
        private String procedureName;

        public HistoricalGraph(String procedureName, String yLabel)
        {
            startDate = DateTime.Now.ToString("yyyyMMdd");
            endDate = DateTime.Now.ToString("yyyyMMdd");
    
            InitializeComponent();
            Y.Title = yLabel;
            this.procedureName = procedureName;
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
                    StrokeThickness = 0,
                    PointGeometrySize = 6
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

        public ChartValues<DateTimePoint> GetData(String start, String end)
        {
            Db fiu = new Db();
            List<DateTimePoint> dateTimePointList = fiu.getHistoricalDataPoints(procedureName, start, end).ToList();
            var values = new ChartValues<DateTimePoint>();
            List<DateTimePoint> plottedVals = new List<DateTimePoint>();
       
            for (int i = 0; i < dateTimePointList.Count(); i += 50)
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
        /*
        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.

            X.MinValue = double.NaN;
            X.MaxValue = double.NaN;
            Y.MinValue = double.NaN;
            Y.MaxValue = double.NaN;
        }
        */
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
}