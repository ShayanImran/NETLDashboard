﻿using NETLDashboard__.NET_Framework_;
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
        List<String> selectedAlgorithms;
        Db fiu = new Db();
        String algorithmsString = "";

        public MachineLearningDashboard()
        {
            InitializeComponent();
            componentList = new List<DDLComponent>();
            algorithmList = new List<DDLAlgorithm>();
            selectedAlgorithms = new List<String>();
            //AddComponentsToList();
            populateAlgorithmBox();
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
            selectedAlgorithms.Clear();
            SelectionList.Items.Add(ModelName.Text);
            SelectionList.Items.Add(ComponentBox.SelectedValue.ToString());
            algorithmsString = "";
            int i = 0;
            foreach (var algorithm in algorithmList)
            {
                if (algorithm.CheckedStatus == true)
                {
                    SelectionList.Items.Add(algorithm.AlgorithmName);
                    selectedAlgorithms.Add(algorithm.AlgorithmName);
                    if(i == algorithmList.Count)
                    {
                        algorithmsString = algorithmsString + algorithm.AlgorithmName ;
                    }
                    else
                    {
                        algorithmsString = algorithmsString + algorithm.AlgorithmName + ",";
                    }
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

        // Makes a call to the database to retrieve all the machine learning algorithm names and adds to dropdown list
        private void populateAlgorithmBox()
        {
            fiu.getAlgorithmNames(algorithmList);
        }

        private void AlgorithmBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlgorithmsBox.ItemsSource = algorithmList.Where(x => x.AlgorithmName.StartsWith(AlgorithmsBox.Text.Trim()));
        }

        private void AlgorithmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            fiu.InsertModelRun(ModelName.Text, Description.Text, "test", ComponentBox.SelectedValue.ToString(), algorithmsString); //remove modelType
            SelectionList.Items.Add("Your entry has been created");
            // run stored procedure for selected component, also take into account multiple procedures

            for(int x = 0; x < selectedAlgorithms.Count; x++)
            {
                String Procedure = "SensorModel_" + ComponentBox.SelectedValue.ToString() + "_" + selectedAlgorithms[x];
                MessageBox.Show(Procedure);
                //fiu.runModels(Procedure); //Starts the model building
            }
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            ModelName.Clear();
            Description.Clear();
            
        }

        private void PredictionRunClicked(object sender, RoutedEventArgs e)
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
