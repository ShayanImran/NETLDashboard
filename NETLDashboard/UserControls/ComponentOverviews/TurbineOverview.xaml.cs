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
        public TurbineOverview(int numOfSensors)
        {
            InitializeComponent();

            for (int i = 0; i < numOfSensors; i++)
            {
                ComponentGrid.RowDefinitions.Add(new RowDefinition());
                ComponentGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ComponentGrid.RowDefinitions[i].Height = new GridLength(400);
                ComponentGrid.ColumnDefinitions[i].Width = new GridLength(700);
            }

            //Creation of our live graph from the user control
            LiveGraph l1 = new LiveGraph("SensorData_TurbineGetLastPhysicalVibrationValue", "Vibration (P)");
            

            //Starts a thread for each graph that allows it to read the values from the database
            Task.Factory.StartNew(l1.Read);
            

            //Placing the graph on the screen in the viewable area
            ComponentGrid.Children.Add(l1);
           
            Grid.SetRow(l1, 0);
            Grid.SetColumn(l1, 0);


        }
    }
}
