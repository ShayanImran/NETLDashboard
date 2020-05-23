using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using NETLDashboard__.NET_Framework_;
using Wpf.CartesianChart.ConstantChanges;
using Wpf.CartesianChart.ZoomingAndPanning;

namespace NETLDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            graphGrid.Children.Clear();

            var button = sender as RadioButton;
            if (string.Compare("Live Graph", button.Content.ToString()) == 0)
            {
                ConstantChangesChart temperatureChart = new ConstantChangesChart();
                temperatureChart.Height = graphGrid.Height;
                temperatureChart.Width = graphGrid.Width;
                graphGrid.Children.Add(temperatureChart);
            }
            if(string.Compare("Historical Graph", button.Content.ToString()) == 0)
            {
                ZoomingAndPanning histChart = new ZoomingAndPanning();
                histChart.Height = graphGrid.Height;
                histChart.Width = graphGrid.Width;
                graphGrid.Children.Add(histChart);
            }
        }

        private void TreeViewItem_Boiler_Sensor1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LiveGraph.IsChecked = true;
        }
    }
}
