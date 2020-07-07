using NETLDashboard.UserControls;
using NETLDashboard.UserControls.ComponentOverviews;
using System.Windows;


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

        private void Tree_Item_Furnace_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Furnace.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Furnace.IsSelected)
            {
                MainGrid.Children.Clear();
                FurnaceOverview furnaceOverview = new FurnaceOverview();
                MainGrid.Children.Add(furnaceOverview);
                component = false;
            }
        }
        private void TreeViewItem_Furnace_FurnaceTempPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalTemp = new GraphSelectButtons("SensorData_FurnaceGetLastPhysicalTempValue", "SensorData_FurnaceGetPhysicalTempValuesByDate", "Temperature (P)");
            MainGrid.Children.Add(physicalTemp);

        }

        private void TreeViewItem_Furnace_FurnaceGasVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualGas = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualGasValue", "SensorData_FurnaceGetVirtualGasValuesByDate", "Gas (V)");
            MainGrid.Children.Add(virtualGas);

        }

        private void TreeViewItem_Furnace_FurnaceAirFlowVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualAirflow = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualAirFlowValue", "SensorData_FurnaceGetVirtualAirFlowValuesByDate", "Air Flow (V)");
            MainGrid.Children.Add(virtualAirflow);

        }
        private void TreeViewItem_Furnace_FurnaceParticulateVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualParticulate = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualParticulateValue", "SensorData_FurnaceGetVirtualParticulateValuesByDate", "Particulate (V)");
            MainGrid.Children.Add(virtualParticulate);

        }

        private void Tree_Item_Boiler_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Boiler.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Boiler.IsSelected)
            {
                MainGrid.Children.Clear();
                BoilerOverview boilerOverview = new BoilerOverview(3);
                MainGrid.Children.Add(boilerOverview);

                component = false;
            }
        }

        private void TreeViewItem_BoilerPressurePhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalPressure = new GraphSelectButtons("SensorData_BoilerGetLastPhysicalPressureValue", "SensorData_BoilerGetPhysicalPressureValuesByDate", "Pressure (P)");
            MainGrid.Children.Add(physicalPressure);

        }

        private void TreeViewItem_BoilerTempVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualTemp = new GraphSelectButtons("SensorData_BoilerGetLastVirtualTempValue", "SensorData_BoilerGetVirtualTempValuesByDate", "Temperature (V)");
            MainGrid.Children.Add(virtualTemp);

        }

        private void TreeViewItem_BoilerPhVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualPH = new GraphSelectButtons("SensorData_BoilerGetLastVirtualPHValue", "SensorData_BoilerGetVirtualPhValuesByDate", "Water Ph (V)");
            MainGrid.Children.Add(virtualPH);

        }

        private void TreeViewItem_BoilerWaterLevelVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualWaterLevel = new GraphSelectButtons("SensorData_BoilerGetLastVirtualWaterLevelValue", "SensorData_BoilerGetVirtualWaterLevelValuesByDate", "Water Level (V)");
            MainGrid.Children.Add(virtualWaterLevel);

        }

        private void Tree_Item_Stack_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Stack.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Stack.IsSelected)
            {
                MainGrid.Children.Clear();
                StackOverview stackOverview = new StackOverview(3);
                MainGrid.Children.Add(stackOverview);

                component = false;
            }
        }
        private void TreeViewItem_StackGasPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalGas = new GraphSelectButtons("SensorData_StackGetLastPhysicalGasValue", "SensorData_StackGetPhysicalGasValuesByDate", "Gas (P)");
            MainGrid.Children.Add(physicalGas);
        }

        private void TreeViewItem_StackTempVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualTemp = new GraphSelectButtons("SensorData_StackGetLastVirtualTempValue", "SensorData_StackGetVirtualTempValuesByDate", "Temperature (V)");
            MainGrid.Children.Add(virtualTemp);
        }

        private void TreeViewItem_StackParticulateVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualParticulate = new GraphSelectButtons("SensorData_StackGetLastVirtualParticulateValue", "SensorData_StackGetVirtualParticulateValuesByDate", "Particulate (V)");
            MainGrid.Children.Add(virtualParticulate);
        }

        private void TreeViewItem_StackAirFlowVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualAirFlow = new GraphSelectButtons("SensorData_StackGetLastVirtualAirFlowValue", "SensorData_StackGetVirtualAirFlowValuesByDate", "Air Flow (V)");
            MainGrid.Children.Add(virtualAirFlow);
        }

        private void Tree_Item_Turbine_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!component && Tree_Item_Turbine.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Turbine.IsSelected)
            {
                MainGrid.Children.Clear();
                TurbineOverview turbineOverview = new TurbineOverview(3);
                MainGrid.Children.Add(turbineOverview);

                component = false;
            }

        }

        private void TreeViewItem_TurbineVibrationPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalVibration = new GraphSelectButtons("SensorData_TurbineGetLastPhysicalVibrationValue", "SensorData_TurbineGetPhysicalVibrationValuesByDate", "Vibration (P)");
            MainGrid.Children.Add(physicalVibration);

        }

        private void Tree_Item_Tubes_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Tubes.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Tubes.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        TubesOverview tubesOverview = new TubesOverview(3);
            //        GraphGrid.Children.Add(tubesOverview);
            //        component = false;
            //    }            

        }

        private void Tree_Item_Container_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Container.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Container.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        ContainerOverview containerOverview = new ContainerOverview(3);
            //        GraphGrid.Children.Add(containerOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Condenser_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Condenser.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Condenser.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        CondenserOverview condenserOverview = new CondenserOverview(3);
            //        GraphGrid.Children.Add(condenserOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Generator_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Generator.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Generator.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        GeneratorOverview generatorOverview = new GeneratorOverview(3);
            //        GraphGrid.Children.Add(generatorOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Transformer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Transformer.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Transformer.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        TransformerOverview transformerOverview = new TransformerOverview(3);
            //        GraphGrid.Children.Add(transformerOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Pulverizer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Pulverizer.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Pulverizer.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        PulverizerOverview pulverizerOverview = new PulverizerOverview(3);
            //        GraphGrid.Children.Add(pulverizerOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Coal_Supplier_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Coal_Supplier.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Coal_Supplier.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        CoalSupplierOverview coalSupplierOverview = new CoalSupplierOverview(3);
            //        GraphGrid.Children.Add(coalSupplierOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Conveyor_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Conveyor.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Conveyor.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        ConveyorOverview conveyorOverview = new ConveyorOverview(3);
            //        GraphGrid.Children.Add(conveyorOverview);
            //        component = false;
            //    }

        }

        private void Tree_Item_Transmission_Lines_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    if (!component && Tree_Item_Transmission_Lines.IsSelected)
            //    {
            //        component = true;
            //    }
            //    if (component && Tree_Item_Transmission_Lines.IsSelected)
            //    {
            //        LiveGraph.Visibility = Visibility.Hidden;
            //        HistoricalGraph.Visibility = Visibility.Hidden;
            //        GraphGrid.Children.Clear();
            //        TransmissionLinesOverview transmissionLinesOverview = new TransmissionLinesOverview(3);
            //        GraphGrid.Children.Add(transmissionLinesOverview);
            //        component = false;
            //    }

        }

        private void TreeViewItem__TempSensorPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    component = false;
            //    GraphGrid.Children.Clear();
            //    //Currently a placeholder until the backend is in the final iteration.
            //    LiveGraph.Visibility = Visibility.Visible;
            //    HistoricalGraph.Visibility = Visibility.Visible;
            //    LiveGraph.IsChecked = false;
            //    LiveGraph.IsChecked = true;
            //    Physical.IsSelected = true;
            //    Virtual.IsSelected = false;
        }

        private void TreeViewItem_TempSensorVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //    component = false;
            //    GraphGrid.Children.Clear();
            //    LiveGraph.Visibility = Visibility.Visible;
            //    HistoricalGraph.Visibility = Visibility.Visible;
            //    LiveGraph.IsChecked = false;
            //    LiveGraph.IsChecked = true;
            //    Physical.IsSelected = false;
            //    Virtual.IsSelected = true;
        }


    }
}
