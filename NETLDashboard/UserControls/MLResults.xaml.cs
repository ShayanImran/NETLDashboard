using LiveCharts;
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

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for MLResults.xaml
    /// </summary>
    public partial class MLResults : UserControl
    {
       
        public MLResults(String algorithmName, String componentName, String metricsUsed, String f1ScoreNum, double pieChartCorrVals)
        {
            InitializeComponent();

            algorithm.Content = algorithmName;
            component.Content = componentName;
            metrics.Content = metricsUsed;
            f1Score.Content = f1ScoreNum;
            MLPredictionCharts chart = new MLPredictionCharts(pieChartCorrVals);
            chart.Width = 175;
            chart.Height = 175;
            resultsGrid.Children.Add(chart);
            Grid.SetColumn(chart, 4);

            //Model.Content = "Model Name";
            //Algorithm.Content = "Algorithm";
            //Accuracy.Content = "Accuracy Score";
            //F1.Content = "F1 Score";
        }

        //public String ModelName { get; set; }
        //public String AlgoType { get; set; }
        //public String AccScore { get; set; }
        //public String F1Score { get; set; }

    }
}
