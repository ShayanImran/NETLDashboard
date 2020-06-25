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
    /// Interaction logic for GraphSelectButtons.xaml
    /// </summary>
    public partial class GraphSelectButtons : UserControl
    {
        private String LiveGraphProcedure;
        private String HistoricalGraphProcedure;

        public GraphSelectButtons(String LiveGraphProcedure, String HistoricalGraphProcedure)
        {
            
            this.LiveGraphProcedure = LiveGraphProcedure;
            this.HistoricalGraphProcedure = HistoricalGraphProcedure;

            InitializeComponent();

            //var button = sender as RadioButton;
            //if (string.Compare("Live Graph", button.Content.ToString()) == 0)
            //{
            //        GraphGrid.Children.Clear();
            //        ConstantChangesChart temperatureChart = new ConstantChangesChart(0);
            //        temperatureChart.Height = graphGrid.Height;
            //        temperatureChart.Width = graphGrid.Width;
            //        Grid.SetRow(temperatureChart, 1);
            //        GraphGrid.Children.Add(temperatureChart);
            //}
            //if (string.Compare("Historical Graph", button.Content.ToString()) == 0)
            //{
            //    // The Set date range needs to pop up here otherwise it'll return all the data in the database.

            //    ZoomingAndPanning histChart = new ZoomingAndPanning();
            //    histChart.Height = graphGrid.Height;
            //    histChart.Width = graphGrid.Width;
            //    Grid.SetRow(histChart, 1);
            //    GraphGrid.Children.Add(histChart);
            //}
        }
    }
}
