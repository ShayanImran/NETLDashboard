using NETLDashboard.UserControls;
using NETLDashboard.UserControls.ComponentOverviews;
using NETLDashboard__.NET_Framework_;
using System.Windows;


namespace NETLDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MachineLearningDashboard MLDash = new MachineLearningDashboard();
        bool component = false;
        public MainWindow()
        {

            InitializeComponent();
            MainGrid.Children.Add(MLDash);

        }
        /* This is a function used to prevent the parent tree item from running when you select the child. 
         This forces it to stay selected on the child object.*/
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

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_Furnace_FurnaceTempPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalTemp = new GraphSelectButtons("SensorData_FurnaceGetLastPhysicalTempValue", "SensorData_FurnaceGetPhysicalTempValuesByDate", "Temperature (P)");
            MainGrid.Children.Add(physicalTemp);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_Furnace_FurnaceGasVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualGas = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualGasValue", "SensorData_FurnaceGetVirtualGasValuesByDate", "Gas (V)");
            MainGrid.Children.Add(virtualGas);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_Furnace_FurnaceAirFlowVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualAirflow = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualAirFlowValue", "SensorData_FurnaceGetVirtualAirFlowValuesByDate", "Air Flow (V)");
            MainGrid.Children.Add(virtualAirflow);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_Furnace_FurnaceParticulateVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualParticulate = new GraphSelectButtons("SensorData_FurnaceGetLastVirtualParticulateValue", "SensorData_FurnaceGetVirtualParticulateValuesByDate", "Particulate (V)");
            MainGrid.Children.Add(virtualParticulate);

        }

        /* This is a function used to prevent the parent tree item from running when you select the child. 
         This forces it to stay selected on the child object.*/
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

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_BoilerPressurePhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalPressure = new GraphSelectButtons("SensorData_BoilerGetLastPhysicalPressureValue", "SensorData_BoilerGetPhysicalPressureValuesByDate", "Pressure (P)");
            MainGrid.Children.Add(physicalPressure);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_BoilerTempVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualTemp = new GraphSelectButtons("SensorData_BoilerGetLastVirtualTempValue", "SensorData_BoilerGetVirtualTempValuesByDate", "Temperature (V)");
            MainGrid.Children.Add(virtualTemp);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_BoilerPhVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualPH = new GraphSelectButtons("SensorData_BoilerGetLastVirtualPHValue", "SensorData_BoilerGetVirtualPhValuesByDate", "Water Ph (V)");
            MainGrid.Children.Add(virtualPH);

        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_BoilerWaterLevelVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualWaterLevel = new GraphSelectButtons("SensorData_BoilerGetLastVirtualWaterLevelValue", "SensorData_BoilerGetVirtualWaterLevelValuesByDate", "Water Level (V)");
            MainGrid.Children.Add(virtualWaterLevel);

        }

        /* This is a function used to prevent the parent tree item from running when you select the child. 
         This forces it to stay selected on the child object.*/
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

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_StackGasPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalGas = new GraphSelectButtons("SensorData_StackGetLastPhysicalGasValue", "SensorData_StackGetPhysicalGasValuesByDate", "Gas (P)");
            MainGrid.Children.Add(physicalGas);
        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_StackTempVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualTemp = new GraphSelectButtons("SensorData_StackGetLastVirtualTempValue", "SensorData_StackGetVirtualTempValuesByDate", "Temperature (V)");
            MainGrid.Children.Add(virtualTemp);
        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_StackParticulateVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualParticulate = new GraphSelectButtons("SensorData_StackGetLastVirtualParticulateValue", "SensorData_StackGetVirtualParticulateValuesByDate", "Particulate (V)");
            MainGrid.Children.Add(virtualParticulate);
        }

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_StackAirFlowVirtual_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons virtualAirFlow = new GraphSelectButtons("SensorData_StackGetLastVirtualAirFlowValue", "SensorData_StackGetVirtualAirFlowValuesByDate", "Air Flow (V)");
            MainGrid.Children.Add(virtualAirFlow);
        }

        /* This is a function used to prevent the parent tree item from running when you select the child. 
         This forces it to stay selected on the child object.*/
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

        /* This function takes the selected child, passes in the stored procedures, and a y-axis label to the user control that creates the graphs.*/
        private void TreeViewItem_TurbineVibrationPhys_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            component = false;
            MainGrid.Children.Clear();
            GraphSelectButtons physicalVibration = new GraphSelectButtons("SensorData_TurbineGetLastPhysicalVibrationValue", "SensorData_TurbineGetPhysicalVibrationValuesByDate", "Vibration (P)");
            MainGrid.Children.Add(physicalVibration);

        }

        /* This is a function used to prevent the parent tree item from running when you select the child. 
         This forces it to stay selected on the child object.*/
        private void Tree_Item_Machine_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!component && Tree_Item_Machine_Learning.IsSelected)
            {
                component = true;
            }
            if (component && Tree_Item_Machine_Learning.IsSelected)
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(MLDash);
                component = false;
            }

        }
        /* All the fucntions below this point are for controling the tree-view behavior. This is to prevent the parent from being
        selected when you click a child of the tree view. They're all slightly different but do the same thing with the main
        difference being the overview page that is generated. As you add more components just uncomment the code and it should
        work. */

        private void Tree_Item_Tubes_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Tubes.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Tubes.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    TubesOverview tubesOverview = new TubesOverview(3);
            //    GraphGrid.Children.Add(tubesOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Container_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Container.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Container.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    ContainerOverview containerOverview = new ContainerOverview(3);
            //    GraphGrid.Children.Add(containerOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Condenser_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Condenser.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Condenser.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    CondenserOverview condenserOverview = new CondenserOverview(3);
            //    GraphGrid.Children.Add(condenserOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Generator_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Generator.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Generator.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    GeneratorOverview generatorOverview = new GeneratorOverview(3);
            //    GraphGrid.Children.Add(generatorOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Transformer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Transformer.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Transformer.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    TransformerOverview transformerOverview = new TransformerOverview(3);
            //    GraphGrid.Children.Add(transformerOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Pulverizer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Pulverizer.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Pulverizer.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    PulverizerOverview pulverizerOverview = new PulverizerOverview(3);
            //    GraphGrid.Children.Add(pulverizerOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Coal_Supplier_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Coal_Supplier.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Coal_Supplier.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    CoalSupplierOverview coalSupplierOverview = new CoalSupplierOverview(3);
            //    GraphGrid.Children.Add(coalSupplierOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Conveyor_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Conveyor.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Conveyor.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    ConveyorOverview conveyorOverview = new ConveyorOverview(3);
            //    GraphGrid.Children.Add(conveyorOverview);
            //    component = false;
            //}

        }

        private void Tree_Item_Transmission_Lines_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!component && Tree_Item_Transmission_Lines.IsSelected)
            //{
            //    component = true;
            //}
            //if (component && Tree_Item_Transmission_Lines.IsSelected)
            //{
            //    LiveGraph.Visibility = Visibility.Hidden;
            //    HistoricalGraph.Visibility = Visibility.Hidden;
            //    GraphGrid.Children.Clear();
            //    TransmissionLinesOverview transmissionLinesOverview = new TransmissionLinesOverview(3);
            //    GraphGrid.Children.Add(transmissionLinesOverview);
            //    component = false;
            //}

        }

    }
}
