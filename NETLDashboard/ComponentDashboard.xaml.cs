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

namespace NETLDashboard
{
    /// <summary>
    /// Interaction logic for ComponentDashboard.xaml
    /// </summary>
    public partial class ComponentDashboard : UserControl
    {
        public ComponentDashboard(int numOfSensors)
        {
           
            InitializeComponent();

            for (int i = 0; i < numOfSensors; i++)
            {
               ComponentGrid.RowDefinitions.Add(new RowDefinition());
               ComponentGrid.RowDefinitions[i].Height = new GridLength(400);
               LiveGraph l1 = new LiveGraph();
               ComponentGrid.Children.Add(l1);
               Grid.SetRow(l1, i);
                
               
            }
               
                
        }

    }
}
