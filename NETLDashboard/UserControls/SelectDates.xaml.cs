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
    /// Interaction logic for SelectDates.xaml
    /// </summary>
    public partial class SelectDates : UserControl
    {
        private HistoricalGraph[] graphNameArray;
       
        public SelectDates(int numberOfGraphs, String[] procedureArray, String[] labelArray)
        {
           graphNameArray = new HistoricalGraph[numberOfGraphs];
            

            //historicalViewArea.ColumnDefinitions.Add(new ColumnDefinition());
            //historicalViewArea.ColumnDefinitions.Add(new ColumnDefinition());
            //historicalViewArea.ColumnDefinitions[0].Width = new GridLength(25, GridUnitType.Star);
            //historicalViewArea.ColumnDefinitions[1].Width = new GridLength(25, GridUnitType.Star);

            InitializeComponent();

            for (int i = 0; i < 2; i++)
            {
                historicalViewArea.ColumnDefinitions.Add(new ColumnDefinition());
                historicalViewArea.ColumnDefinitions[i].Width = new GridLength(50,GridUnitType.Star);
            }

            for (int i = 0; i < numberOfGraphs / 2; i++)
            {
                historicalViewArea.RowDefinitions.Add(new RowDefinition());
                historicalViewArea.RowDefinitions[i].Height = new GridLength(500);
            }

            int k = 0;
            for (int i = 0; i < numberOfGraphs / 2 ; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    graphNameArray[k] = new HistoricalGraph(procedureArray[k], labelArray[k]);
                    historicalViewArea.Children.Add(graphNameArray[k]);
                    Grid.SetRow(graphNameArray[k], i);
                    Grid.SetColumn(graphNameArray[k], j);
                    k++;
                }
            }

            

           


        }
        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.

            //X.MinValue = double.NaN;
            //X.MaxValue = double.NaN;
            //Y.MinValue = double.NaN;
            //Y.MaxValue = double.NaN;
        }

        private void Selectdates(object sender, RoutedEventArgs e)
        {
            ////Create DatePicker selection window, then redraw the entire graph
            //if (SeriesCollection.Count != 0)
            //{
            //    SeriesCollection.Clear();
            //}
            //startDate = Start.SelectedDate.Value.Date.ToString("yyyyMMdd");
            //endDate = End.SelectedDate.Value.Date.ToString("yyyyMMdd");
            //SeriesCollection = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = GetData(startDate,endDate),
            //        StrokeThickness = 0,
            //        PointGeometrySize = 3
            //    }
            //};

            //ZoomingMode = ZoomingOptions.X;

            //XFormatter = val => new DateTime((long)val).ToString("dd MMM");
            //YFormatter = val => val.ToString("G");

            //DataContext = this;
        }
    } 
}
