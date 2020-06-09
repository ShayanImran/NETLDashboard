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
    /// Interaction logic for StackOverview.xaml
    /// </summary>
    public partial class StackOverview : UserControl
    {
        public StackOverview(int numOfSensors)
        {

            InitializeComponent();

            for (int i = 0; i < numOfSensors; i++)
            {
                ComponentGrid.RowDefinitions.Add(new RowDefinition());
                ComponentGrid.RowDefinitions[i].Height = new GridLength(400);
            }

            //Creation of our live graph from the user control
            LiveGraph l1 = new LiveGraph();
            LiveGraph l2 = new LiveGraph();                                     // Current plan is to hard code a parameter that can allow for some diversity among the graphs
            LiveGraph l3 = new LiveGraph();

            //Starts a thread for each graph that allows it to read the values from the database
            Task.Factory.StartNew(l1.Read);
            Task.Factory.StartNew(l2.Read);
            Task.Factory.StartNew(l3.Read);

            //Placing the graph on the screen in the viewable area
            ComponentGrid.Children.Add(l1);
            ComponentGrid.Children.Add(l2);
            ComponentGrid.Children.Add(l3);

            Grid.SetRow(l1, 0);
            Grid.SetRow(l2, 1);
            Grid.SetRow(l3, 2);

        }
    }
}
