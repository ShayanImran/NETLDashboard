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
        bool isCreated = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Sensor1_LiveGraph_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
                graphGrid.Children.Clear();
                
                ConstantChangesChart temperatureChart = new ConstantChangesChart();
                temperatureChart.Height = graphGrid.Height;
                temperatureChart.Width = graphGrid.Width;
                graphGrid.Children.Add(temperatureChart);
                        
        }


        private void TreeViewItem_Sensor1_HistoricalGraph_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            ZoomingAndPanning histChart = new ZoomingAndPanning();
            histChart.Height = graphGrid.Height;
            histChart.Width = graphGrid.Width;
            graphGrid.Children.Add(histChart);
        }
    }
}
