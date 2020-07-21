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
       //This user control was made to make it easier to display the data in an easy to read format. Allows for the dynamic creation of results.
        public MLResults(String algorithmName, String componentName, String accuracyScore, String results, double pieChartCorrVals)
        {
            InitializeComponent();

            algorithm.Content = algorithmName;
            component.Content = componentName;
            metrics.Content =accuracyScore;
            ResultsLabel.Content = results;
           
            MLPredictionCharts chart = new MLPredictionCharts(pieChartCorrVals);
            chart.Width = 175;
            chart.Height = 175;
            resultsGrid.Children.Add(chart);
            Grid.SetColumn(chart, 4);

        }

    }
}
