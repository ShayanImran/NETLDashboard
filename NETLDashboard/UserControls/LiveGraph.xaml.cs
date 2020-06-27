using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using System.Threading;
using System.ComponentModel;
using NETLDashboard__.NET_Framework_;


namespace NETLDashboard
{
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}

namespace NETLDashboard
{
    public partial class LiveGraph : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private string _yAxisName;
        private string procedureName;
        string yLabelName;

        // Name space and class names need to be same
        public LiveGraph()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //saves the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            ChartValues = new ChartValues<MeasureModel>();

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

            //AxisStep forces the distance between each separator in the X axis
            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);

            //Starts plotting points in a seperate thread

            IsReading = true;
            DataContext = this;
         
        }
        public LiveGraph(String procedureName, String yLabel)
        {
            InitializeComponent();
            colr = "blue";
            this.procedureName = procedureName;
            yaxis.Title = yLabel;
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value)
                .Fill(item => item.Value > 570 || item.Value < 540 ? DangerBrush : null);

            DangerBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            //saves the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            ChartValues = new ChartValues<MeasureModel>();

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

            //AxisStep forces the distance between each separator in the X axis
            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);


            //Starts plotting points in a seperate thread

            IsReading = true;
            DataContext = this;

        }

        public Brush DangerBrush { get; set; }
        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }
        public string colr { get; set; }
   
        public bool IsReading { get; set; }

        public void Read()
        {
            //Connects to the FIU database to read the data.
            Db fiu = new Db();
            //This is always true, so the only way to exit out is to close the application or change the window to a tab that clears the container it resides in.
            while (IsReading)
            {
                Thread.Sleep(1000); //The thread needs to pause in order prevent the gui from locking up
                var now = DateTime.Now; //Gets the current date and time
                
               
                    ChartValues.Add(new MeasureModel //Sets the date and time, along with the current temperature sensor value.
                    {
                        DateTime = now,
                        Value = fiu.getLastVirtualEntry(procedureName)

                    });
               
                SetAxisLimits(now);
                //lets only use the last 11 values to prevent the graphics from slowing down. 
                if (ChartValues.Count > 15) ChartValues.RemoveAt(0);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(9).Ticks; // and 11 seconds behind
        }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}