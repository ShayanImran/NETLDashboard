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

namespace NETLDashboard.UserControls.ComponentOverviews
{
    /// <summary>
    /// Interaction logic for TurbineOverview.xaml
    /// </summary>
    public partial class TurbineOverview : UserControl
    {
        private bool hasChild = false;

        public TurbineOverview(int numOfSensors)
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
                LiveGraph l1 = new LiveGraph("SensorData_TurbineGetLastPhysicalVibrationValue", "Vibration (P)");


                //Starts a thread for each graph that allows it to read the values from the database
                Task.Factory.StartNew(l1.Read);


                //Placing the graph on the screen in the viewable area
                viewableArea.Children.Add(l1);


                Grid.SetRow(l1, 0);
                Grid.SetColumn(l1, 0);

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
                ///Creation of our live graph from the user control
                LiveGraph l1 = new LiveGraph("SensorData_TurbineGetLastPhysicalVibrationValue", "Gas (P)");


                //Starts a thread for each graph that allows it to read the values from the database
                Task.Factory.StartNew(l1.Read);


                //Placing the graph on the screen in the viewable area
                viewableArea.Children.Add(l1);


                Grid.SetRow(l1, 0);
                Grid.SetColumn(l1, 0);
            }

        }

        private void HistoricalGraphs_Checked(object sender, RoutedEventArgs e)
        {
            viewableArea.RowDefinitions.Clear();
            viewableArea.ColumnDefinitions.Clear();
            viewableArea.Children.Clear();

            string[] procedureArray ={
                "SensorData_TurbineGetPhysicalVibrationValuesByDate"
            };
            string[] labelArray ={
                "Vibration (P)"
            };

            SelectDates graphs = new SelectDates(1, procedureArray, labelArray);
            viewableArea.Children.Add(graphs);

        }
    }
}
