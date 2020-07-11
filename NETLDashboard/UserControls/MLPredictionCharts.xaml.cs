using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for MLPredictionCharts.xaml
    /// </summary>
    public partial class MLPredictionCharts : UserControl
    {
        public MLPredictionCharts()
        {
            InitializeComponent();

            CorrectValue = new ChartValues<Double> { 60 };
            MaliciousValue = new ChartValues<Double> { 40 };

            DataContext = this;
        }

        public ChartValues<Double> CorrectValue { get; set; }
        public ChartValues<Double> MaliciousValue { get; set; }
    }
}
