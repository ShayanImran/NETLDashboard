using NETLDashboard.UserControls.ComponentOverviews;
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


        private void Tree_Item_Boiler_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            BoilerOverview boilerOverview = new BoilerOverview(3);
            graphGrid.Children.Add(boilerOverview);
        }

        private void Tree_Item_Furnace_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            FurnaceOverview furnaceOverview = new FurnaceOverview(3);
            graphGrid.Children.Add(furnaceOverview);
        }

        private void Tree_Item_Stack_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            StackOverview stackOverview = new StackOverview(3);
            graphGrid.Children.Add(stackOverview);
        }

        private void Tree_Item_Turbine_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            TurbineOverview turbineOverview = new TurbineOverview(3);
            graphGrid.Children.Add(turbineOverview);
        }

        private void Tree_Item_Tubes_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            TubesOverview tubesOverview = new TubesOverview(3);
            graphGrid.Children.Add(tubesOverview);

        }

        private void Tree_Item_Container_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            ContainerOverview containerOverview = new ContainerOverview(3);
            graphGrid.Children.Add(containerOverview);
        }

        private void Tree_Item_Condenser_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            CondenserOverview condenserOverview = new CondenserOverview(3);
            graphGrid.Children.Add(condenserOverview);
        }

        private void Tree_Item_Generator_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            GeneratorOverview generatorOverview = new GeneratorOverview(3);
            graphGrid.Children.Add(generatorOverview);
        }

        private void Tree_Item_Transformer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            TransformerOverview transformerOverview = new TransformerOverview(3);
            graphGrid.Children.Add(transformerOverview);
        }

        private void Tree_Item_Pulverizer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            PulverizerOverview pulverizerOverview = new PulverizerOverview(3);
            graphGrid.Children.Add(pulverizerOverview);
        }

        private void Tree_Item_Coal_Supplier_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            CoalSupplierOverview coalSupplierOverview = new CoalSupplierOverview(3);
            graphGrid.Children.Add(coalSupplierOverview);
        }

        private void Tree_Item_Conveyor_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            ConveyorOverview conveyorOverview = new ConveyorOverview(3);
            graphGrid.Children.Add(conveyorOverview);
        }

        private void Tree_Item_Transmission_Lines_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            graphGrid.Children.Clear();
            TransmissionLinesOverview transmissionLinesOverview = new TransmissionLinesOverview(3);
            graphGrid.Children.Add(transmissionLinesOverview);
        }
    }
}
