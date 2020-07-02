﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Wpf.CartesianChart.ZoomingAndPanning;

namespace NETLDashboard
{
    /// <summary>
    /// Interaction logic for ComponentDashboard.xaml
    /// </summary>
    
    public partial class FurnaceOverview : UserControl
    {
        private String LiveGraphProcedure;
        private String HistoricalGraphProcedure;
        private String yLabel;
        private bool hasChild = false;

        public FurnaceOverview()
        {
           
            InitializeComponent();

            LiveG.IsChecked = true;
            hasChild = true;
        }
        private void LiveGraphs_Checked(object sender, RoutedEventArgs e)
        {
            if (!hasChild)
            {
                for (int i = 0; i < 2; i++)
                {
                    viewableArea.RowDefinitions.Add(new RowDefinition());
                    viewableArea.ColumnDefinitions.Add(new ColumnDefinition());
                    viewableArea.RowDefinitions[i].Height = new GridLength(25, GridUnitType.Star);
                    viewableArea.ColumnDefinitions[i].Width = new GridLength(25, GridUnitType.Star);
                }

                //Creation of our live graph from the user control
                LiveGraph l1 = new LiveGraph("SensorData_FurnaceGetLastPhysicalTempValue", "Temperature (P)");
                LiveGraph l2 = new LiveGraph("SensorData_FurnaceGetLastVirtualGasValue", "Gas (V)");
                LiveGraph l3 = new LiveGraph("SensorData_FurnaceGetLastVirtualAirFlowValue", "Air Flow (V)"); // same as gas for now 
                LiveGraph l4 = new LiveGraph("SensorData_FurnaceGetLastVirtualParticulateValue", "Particulate (V)");

                //Starts a thread for each graph that allows it to read the values from the database
                Task.Factory.StartNew(l1.Read);
                Task.Factory.StartNew(l2.Read);
                Task.Factory.StartNew(l3.Read);
                Task.Factory.StartNew(l4.Read);

                //Placing the graph on the screen in the viewable area
                viewableArea.Children.Add(l1);
                viewableArea.Children.Add(l2);
                viewableArea.Children.Add(l3);
                viewableArea.Children.Add(l4);

                Grid.SetRow(l1, 0);
                Grid.SetColumn(l1, 0);

                Grid.SetRow(l2, 0);
                Grid.SetColumn(l2, 1);

                Grid.SetRow(l3, 1);
                Grid.SetColumn(l3, 0);

                Grid.SetRow(l4, 1);
                Grid.SetColumn(l4, 1);
            }
            if (hasChild)
            {

                viewableArea.RowDefinitions.Clear();
                viewableArea.ColumnDefinitions.Clear();
                viewableArea.Children.Clear();

                for (int i = 0; i < 2; i++)
                {
                    viewableArea.RowDefinitions.Add(new RowDefinition());
                    viewableArea.ColumnDefinitions.Add(new ColumnDefinition());
                    viewableArea.RowDefinitions[i].Height = new GridLength(25, GridUnitType.Star);
                    viewableArea.ColumnDefinitions[i].Width = new GridLength(25, GridUnitType.Star);
                }
                //Creation of our live graph from the user control
                LiveGraph l1 = new LiveGraph("SensorData_FurnaceGetLastPhysicalTempValue", "Temperature (P)");
                LiveGraph l2 = new LiveGraph("SensorData_FurnaceGetLastVirtualGasValue", "Gas (V)");
                LiveGraph l3 = new LiveGraph("SensorData_FurnaceGetLastVirtualAirFlowValue", "Air Flow (V)"); // same as gas for now 
                LiveGraph l4 = new LiveGraph("SensorData_FurnaceGetLastVirtualParticulateValue", "Particulate (V)");

                //Starts a thread for each graph that allows it to read the values from the database
                Task.Factory.StartNew(l1.Read);
                Task.Factory.StartNew(l2.Read);
                Task.Factory.StartNew(l3.Read);
                Task.Factory.StartNew(l4.Read);

                //Placing the graph on the screen in the viewable area
                viewableArea.Children.Add(l1);
                viewableArea.Children.Add(l2);
                viewableArea.Children.Add(l3);
                viewableArea.Children.Add(l4);

                Grid.SetRow(l1, 0);
                Grid.SetColumn(l1, 0);

                Grid.SetRow(l2, 0);
                Grid.SetColumn(l2, 1);

                Grid.SetRow(l3, 1);
                Grid.SetColumn(l3, 0);

                Grid.SetRow(l4, 1);
                Grid.SetColumn(l4, 1);
            }

        }

        private void HistoricalGraphs_Checked(object sender, RoutedEventArgs e)
        {
            viewableArea.RowDefinitions.Clear();
            viewableArea.ColumnDefinitions.Clear();
            viewableArea.Children.Clear();

            viewableArea.RowDefinitions.Add(new RowDefinition());
            viewableArea.RowDefinitions[0].Height = new GridLength(100);

            for (int i = 1; i <= 2; i++)
            {
                viewableArea.RowDefinitions.Add(new RowDefinition());
                viewableArea.ColumnDefinitions.Add(new ColumnDefinition());
                viewableArea.RowDefinitions[i].Height = new GridLength(25, GridUnitType.Star);
                viewableArea.ColumnDefinitions[i-1].Width = new GridLength(25, GridUnitType.Star);
            }

            SelectDates rec = new SelectDates();
            viewableArea.Children.Add(rec);
            Grid.SetRow(rec, 0);
            Grid.SetColumnSpan(rec, 2);

            LiveGraph l1 = new LiveGraph("SensorData_FurnaceGetLastPhysicalTempValue", "Temperature (P)");
            LiveGraph l2 = new LiveGraph("SensorData_FurnaceGetLastVirtualGasValue", "Gas (V)");
            LiveGraph l3 = new LiveGraph("SensorData_FurnaceGetLastVirtualAirFlowValue", "Air Flow (V)"); // same as gas for now 
            LiveGraph l4 = new LiveGraph("SensorData_FurnaceGetLastVirtualParticulateValue", "Particulate (V)");

            //Starts a thread for each graph that allows it to read the values from the database
            Task.Factory.StartNew(l1.Read);
            Task.Factory.StartNew(l2.Read);
            Task.Factory.StartNew(l3.Read);
            Task.Factory.StartNew(l4.Read);

            //Placing the graph on the screen in the viewable area
            viewableArea.Children.Add(l1);
            viewableArea.Children.Add(l2);
            viewableArea.Children.Add(l3);
            viewableArea.Children.Add(l4);

            Grid.SetRow(l1, 1);
            Grid.SetColumn(l1, 0);

            Grid.SetRow(l2, 1);
            Grid.SetColumn(l2, 1);

            Grid.SetRow(l3, 2);
            Grid.SetColumn(l3, 0);

            Grid.SetRow(l4, 2);
            Grid.SetColumn(l4, 1);


            /*
            HistoricalGraph historicalGraph = new HistoricalGraph(HistoricalGraphProcedure);
            viewableArea.Children.Add(historicalGraph);
            Grid.SetRow(historicalGraph, 1); */
        }
    }
}
