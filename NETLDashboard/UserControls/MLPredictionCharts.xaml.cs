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
        public MLPredictionCharts(double correct)
        {
            InitializeComponent();
            correct = Math.Round(correct * 100, 2);
            CorrectValue = new ChartValues<Double> { correct};
            MaliciousValue = new ChartValues<Double> { 100 - correct };

            DataContext = this;
        }

        public ChartValues<Double> CorrectValue { get; set; }
        public ChartValues<Double> MaliciousValue { get; set; }
    }
}
