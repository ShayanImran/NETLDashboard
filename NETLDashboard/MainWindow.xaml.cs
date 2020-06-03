using System;
using System.Windows;
using System.Windows.Controls;
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
        {   //The radio button switch updates the part of the screen that displays the live and historical graphs
            graphGrid.Children.Clear();

            var button = sender as RadioButton;
            if (string.Compare("Live Graph", button.Content.ToString()) == 0)
            {
                if (Physical.IsSelected)
                {
                    graphGrid.Children.Clear();
                    ConstantChangesChart temperatureChart = new ConstantChangesChart(0);
                    temperatureChart.Height = graphGrid.Height;
                    temperatureChart.Width = graphGrid.Width;
                    graphGrid.Children.Add(temperatureChart);
                }
                else if (Virtual.IsSelected)
                {
                    graphGrid.Children.Clear();
                    ConstantChangesChart temperatureChart = new ConstantChangesChart(1);
                    temperatureChart.Height = graphGrid.Height;
                    temperatureChart.Width = graphGrid.Width;
                    graphGrid.Children.Add(temperatureChart);
                }

            }
            if (string.Compare("Historical Graph", button.Content.ToString()) == 0)
            {
                ZoomingAndPanning histChart = new ZoomingAndPanning();
                histChart.Height = graphGrid.Height;
                histChart.Width = graphGrid.Width;
                graphGrid.Children.Add(histChart);
            }
        }

        private void TreeViewItem_Boiler_Sensor1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            //Currently a placeholder until the backend is in the final iteration.
            LiveGraph.IsChecked = false;
            LiveGraph.IsChecked = true;
            Physical.IsSelected = true;
            Virtual.IsSelected = false;
        }

        private void TreeViewItem_Boiler_Sensor2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            //Currently a placeholder until the backend is in the final iteration.
            LiveGraph.IsChecked = false;
            LiveGraph.IsChecked = true;
            Physical.IsSelected = false;
            Virtual.IsSelected = true;
        }

        private void Furnace_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            ComponentDashboard furnaceDashboard = new ComponentDashboard(8);
            graphGrid.Children.Add(furnaceDashboard);
        }
    }
}
