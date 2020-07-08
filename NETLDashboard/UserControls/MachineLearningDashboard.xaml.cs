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

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for MachineLearningDashboard.xaml
    /// </summary>
    public partial class MachineLearningDashboard : UserControl
    {
        List<DDLComponent> componentList;
        List<DDLAlgorithm> algorithmList;
        public MachineLearningDashboard()
        {
            InitializeComponent();
            componentList = new List<DDLComponent>();
            algorithmList = new List<DDLAlgorithm>();
            //AddComponentsToList();
            AddAlgorithmsToList();
            //BindDropDown();
            BindDropDownAlgo();
        }

        private void BindDropDown()
        {
            ComponentBox.ItemsSource = componentList;
        }

        private void AllComponents_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            BindListBox();
        }

        private void BindListBox()
        {
            SelectionList.Items.Clear();
            SelectionList.Items.Add(ModelName.Text);
            //SelectionList.Items.Add(ComponentBox.SelectedValue.ToString());
            foreach (var algorithm in algorithmList)
            {
                if (algorithm.CheckedStatus == true)
                {
                    SelectionList.Items.Add(algorithm.AlgorithmName);
                }
            }
        }
        private void Component_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComponentBox.ItemsSource = componentList.Where(x => x.ComponentName.StartsWith(ComponentBox.Text.Trim()));
        }

        private void ComponentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindListBox();
        }

        private void AddComponentsToList()
        {
            DDLComponent thing = new DDLComponent();
            thing.ComponentName = "Furnace";
            thing.ComponentId = 2;
            componentList.Add(thing);
            thing = new DDLComponent();
            thing.ComponentName = "Boiler";
            thing.ComponentId = 1;
            componentList.Add(thing);
            thing = new DDLComponent();
            thing.ComponentName = "Stack";
            thing.ComponentId = 4;
            componentList.Add(thing);
            thing = new DDLComponent();
            thing.ComponentName = "Turbine";
            thing.ComponentId = 3;
            componentList.Add(thing);

        }

        private void BindDropDownAlgo()
        {
            AlgorithmsBox.ItemsSource = algorithmList;
        }

        private void AddAlgorithmsToList()
        {
            DDLAlgorithm thing = new DDLAlgorithm();
            thing.AlgorithmName = "Test Algorithm 1";
            thing.AlgorithmId = 1;
            algorithmList.Add(thing);
            thing = new DDLAlgorithm();
            thing.AlgorithmName = "Test Algorithm 1";
            thing.AlgorithmId = 2;
            algorithmList.Add(thing);
            thing = new DDLAlgorithm();
            thing.AlgorithmName = "Test Algorithm 1";
            thing.AlgorithmId = 3;
            algorithmList.Add(thing);
            thing = new DDLAlgorithm();
            thing.AlgorithmName = "Test Algorithm 1";
            thing.AlgorithmId = 4;
            algorithmList.Add(thing);

        }

        private void AlgorithmBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlgorithmsBox.ItemsSource = algorithmList.Where(x => x.AlgorithmName.StartsWith(AlgorithmsBox.Text.Trim()));
        }

        private void AlgorithmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
    
    public class DDLComponent
    {
        public int ComponentId
        {
            get;
            set;
        }
        public string ComponentName
        {
            get;
            set;
        }
        public Boolean CheckedStatus
        {
            get;
            set;
        }
    }

    public class DDLAlgorithm
    {
        public int AlgorithmId
        {
            get;
            set;
        }
        public string AlgorithmName
        {
            get;
            set;
        }
        public Boolean CheckedStatus
        {
            get;
            set;
        }
    }
}
