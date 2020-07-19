using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.CartesianChart.ZoomingAndPanning;

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for GraphSelectButtons.xaml
    /// </summary>
    public partial class GraphSelectButtons : UserControl
    {
        private String LiveGraphProcedure;
        private String HistoricalGraphProcedure;
        private String yLabel;
        private bool hasChild = false;

        public GraphSelectButtons(String LiveGraphProcedure, String HistoricalGraphProcedure, String yLabel)
        {
            
            this.LiveGraphProcedure = LiveGraphProcedure;
            this.HistoricalGraphProcedure = HistoricalGraphProcedure;
            this.yLabel = yLabel;

            InitializeComponent();
            LiveG.IsChecked = true;
            hasChild = true; //Just used once in the LiveGraph_Checked function.
            
        }

        /* This function has a switch statement inside becuase it is the default value. If it was not implemented, the application
         would crash on the first time a user selects one of the components to view the sensor overview.*/
        private void LiveGraph_Checked(object sender, RoutedEventArgs e)
        {
            if(!hasChild)
            {
                LiveGraph liveGraph = new LiveGraph(LiveGraphProcedure, yLabel); //Creates the instance and passes in the procedure and axis label
                viewableArea.Children.Add(liveGraph);
                Task.Factory.StartNew(liveGraph.Read); //This starts the multi-threading 
                Grid.SetRow(liveGraph, 1);
            }
            if(hasChild)
            {
                viewableArea.Children.Clear();
                LiveGraph liveGraph = new LiveGraph(LiveGraphProcedure, yLabel);
                viewableArea.Children.Add(liveGraph);
                Task.Factory.StartNew(liveGraph.Read);
                Grid.SetRow(liveGraph, 1);
            }
            
        }

        private void HistoricalGraph_Checked(object sender, RoutedEventArgs e)
        {
            viewableArea.Children.Clear();
            ZoomingAndPanning historicalGraph = new ZoomingAndPanning(HistoricalGraphProcedure);//Passes in the stored procedure
            viewableArea.Children.Add(historicalGraph);//adds it to the grid
            Grid.SetRow(historicalGraph, 1); //Sets it in the correct spot on the grid.
        }
    }
}
