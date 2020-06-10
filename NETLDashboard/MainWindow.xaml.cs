using NETLDashboard.UserControls.ComponentOverviews;
using NETLDashboard__.NET_Framework_;
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
        bool component = false;
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {   //The radio button switch updates the part of the screen that displays the live and historical graphs
            component = false;
            GraphGrid.Children.Clear();

            var button = sender as RadioButton;
            if (string.Compare("Live Graph", button.Content.ToString()) == 0)
            {
                if (Physical.IsSelected)
                {
                    GraphGrid.Children.Clear();
                    ConstantChangesChart temperatureChart = new ConstantChangesChart(0);
                    temperatureChart.Height = graphGrid.Height;
                    temperatureChart.Width = graphGrid.Width;
                    Grid.SetRow(temperatureChart, 1);
                    GraphGrid.Children.Add(temperatureChart);
                }
                else if (Virtual.IsSelected)
                {
                    GraphGrid.Children.Clear();
                    ConstantChangesChart temperatureChart = new ConstantChangesChart(1);
                    temperatureChart.Height = graphGrid.Height;
                    temperatureChart.Width = graphGrid.Width;
                    Grid.SetRow(temperatureChart, 1);
                    GraphGrid.Children.Add(temperatureChart);
                }

            }
            if (string.Compare("Historical Graph", button.Content.ToString()) == 0)
            {
                String startDate, endDate;

                // The Set date range needs to pop up here otherwise it'll return all the data in the database.

                DateTimeWindow selectDates = new DateTimeWindow();
                selectDates.ShowDialog();
                startDate = selectDates.startDate.ToString("yyyyMMdd");
                endDate = selectDates.endDate.ToString("yyyyMMdd");
                selectDates.Close();
                ZoomingAndPanning histChart = new ZoomingAndPanning(startDate,endDate);
                histChart.Height = graphGrid.Height;
                histChart.Width = graphGrid.Width;
                Grid.SetRow(histChart, 1);
                GraphGrid.Children.Add(histChart);
            }
        }

        private void TreeViewItem_Furnace_Sensor1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            GraphGrid.Children.Clear();
            //Currently a placeholder until the backend is in the final iteration.
            LiveGraph.Visibility = Visibility.Visible;
            HistoricalGraph.Visibility = Visibility.Visible;
            LiveGraph.IsChecked = false;
            LiveGraph.IsChecked = true;
            Physical.IsSelected = true;
            Virtual.IsSelected = false;
        }

        private void TreeViewItem_Furnace_Sensor2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            GraphGrid.Children.Clear();
            LiveGraph.Visibility = Visibility.Visible;
            HistoricalGraph.Visibility = Visibility.Visible;
            LiveGraph.IsChecked = false;
            LiveGraph.IsChecked = true;
            Physical.IsSelected = false;
            Virtual.IsSelected = true;
        }


        private void Tree_Item_Boiler_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Boiler.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Boiler.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                BoilerOverview boilerOverview = new BoilerOverview(3);
                Grid.SetRow(boilerOverview, 1);
                GraphGrid.Children.Add(boilerOverview);
                component = false;
            }         
        }

        private void Tree_Item_Furnace_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if(!component && Tree_Item_Furnace.IsSelected)
            {
                component = true;
            }
            if(component && Tree_Item_Furnace.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                FurnaceOverview furnaceOverview = new FurnaceOverview(3);
                Grid.SetRow(furnaceOverview, 1);
                GraphGrid.Children.Add(furnaceOverview);
                component = false;
            }
        }

        private void Tree_Item_Stack_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Stack.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Stack.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                StackOverview stackOverview = new StackOverview(3);
                GraphGrid.Children.Add(stackOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Turbine_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Turbine.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Turbine.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                TurbineOverview turbineOverview = new TurbineOverview(3);
                GraphGrid.Children.Add(turbineOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Tubes_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Tubes.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Tubes.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                TubesOverview tubesOverview = new TubesOverview(3);
                GraphGrid.Children.Add(tubesOverview);
                component = false;
            }            

        }

        private void Tree_Item_Container_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Container.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Container.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                ContainerOverview containerOverview = new ContainerOverview(3);
                GraphGrid.Children.Add(containerOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Condenser_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Condenser.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Condenser.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                CondenserOverview condenserOverview = new CondenserOverview(3);
                GraphGrid.Children.Add(condenserOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Generator_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Generator.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Generator.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                GeneratorOverview generatorOverview = new GeneratorOverview(3);
                GraphGrid.Children.Add(generatorOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Transformer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Transformer.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Transformer.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                TransformerOverview transformerOverview = new TransformerOverview(3);
                GraphGrid.Children.Add(transformerOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Pulverizer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Pulverizer.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Pulverizer.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                PulverizerOverview pulverizerOverview = new PulverizerOverview(3);
                GraphGrid.Children.Add(pulverizerOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Coal_Supplier_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Coal_Supplier.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Coal_Supplier.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                CoalSupplierOverview coalSupplierOverview = new CoalSupplierOverview(3);
                GraphGrid.Children.Add(coalSupplierOverview);
                component = false;
            }
            
        }

        private void Tree_Item_Conveyor_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Conveyor.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Conveyor.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                ConveyorOverview conveyorOverview = new ConveyorOverview(3);
                GraphGrid.Children.Add(conveyorOverview);
                component = false;
            }
           
        }

        private void Tree_Item_Transmission_Lines_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Transmission_Lines.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Transmission_Lines.IsSelected)
            {
                LiveGraph.Visibility = Visibility.Hidden;
                HistoricalGraph.Visibility = Visibility.Hidden;
                GraphGrid.Children.Clear();
                TransmissionLinesOverview transmissionLinesOverview = new TransmissionLinesOverview(3);
                GraphGrid.Children.Add(transmissionLinesOverview);
                component = false;
            }
            
        }
    }
}
