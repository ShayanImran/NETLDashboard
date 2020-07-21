using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for MLPredictionCharts.xaml
    /// </summary>
    /// 
    //The pi chart is from LiveCharts and can be found here: https://lvcharts.net/App/examples/v1/wpf/Pie%20Chart
    public partial class MLPredictionCharts : UserControl
    {
        public MLPredictionCharts(double correct)
        {
            InitializeComponent();
            correct = Math.Round(correct * 100, 2); //Rounds the number to two decimal places, avoiding a messy value displayed on the chart.
            CorrectValue = new ChartValues<Double> { correct};
            MaliciousValue = new ChartValues<Double> { 100 - correct };

            DataContext = this;
        }

        public ChartValues<Double> CorrectValue { get; set; }
        public ChartValues<Double> MaliciousValue { get; set; }
    }
}
