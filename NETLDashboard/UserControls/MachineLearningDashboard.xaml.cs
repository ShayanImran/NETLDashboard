using NETLDashboard__.NET_Framework_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NETLDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for MachineLearningDashboard.xaml
    /// </summary>
    public partial class MachineLearningDashboard : UserControl
    {
        Db fiu = new Db();
        List<DDLComponent> componentList;
        List<DDLAlgorithm> algorithmList;
        List<MLPredictionInfo> builtModels;
        List<String> selectedAlgorithms;
        String algorithmsString = "";

        public MachineLearningDashboard()
        {
            InitializeComponent();
            componentList = new List<DDLComponent>();
            algorithmList = new List<DDLAlgorithm>();
            builtModels = new List<MLPredictionInfo>();
            selectedAlgorithms = new List<String>();

            //BindDropDown();
            BindDropDownAlgo();
            BindDropDownModels();
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
            SelectionList.Items.Add(ModelName.Text); //FIXME crash when selecting algorithm without selecting component
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
            fiu.getAlgorithmNames(algorithmList);
            AlgorithmsBox.ItemsSource = algorithmList;
        }

        private void BindDropDownModels()
        {
            fiu.getBuiltModels(builtModels);

            List<String> modelNames = new List<String>();
            for(int i = 0; i < builtModels.Count; i++)
            {
                modelNames.Add(builtModels[i].ModelName);
            }
            PredictionsModelName.ItemsSource = modelNames; 
        }

        private void BindPredictionAlgos(String currentModel)
        {
            String[] temp = new String[3];
            for(int i = 0; i < builtModels.Count; i++)
            {
                if(currentModel.Equals(builtModels[i].ModelName))
                {
                    //MessageBox.Show(builtModels[i].ModelName.ToString());
                    String[] algos = builtModels[i].ModelAlgos.Split(',');
                    
                    for(int j = 0; j < algos.Length; j++)
                    {
                        MLPredictionInfo val = builtModels[i];
                        val.ModelAlgos = algos[j];
                        temp[j] = val.ModelAlgos;
                       // MessageBox.Show(temp[j].ToString());
                        //PredictionsAlgorithmsBox.Items.Insert(j, tempObject[j].ModelAlgos.ToString());
                    }
                }

            }
            PredictionsAlgorithmsBox.ItemsSource = temp;

        }

        // Makes a call to the database to retrieve all the machine learning algorithm names and adds to dropdown list

        private void AlgorithmBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlgorithmsBox.ItemsSource = algorithmList.Where(x => x.AlgorithmName.StartsWith(AlgorithmsBox.Text.Trim()));
        }

        private void AlgorithmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            // Removing the comma at the end of the string
            algorithmsString = algorithmsString.Remove(algorithmsString.Length - 1);

            fiu.InsertModelRun(ModelName.Text, Description.Text, "test", ComponentBox.SelectedValue.ToString(), algorithmsString); //remove modelType
            SelectionList.Items.Add("Your entry has been created");
            // run stored procedure for selected component, also take into account multiple procedures

            for(int i = 0; i < selectedAlgorithms.Count; i++)
            {
                
                String Procedure = "SensorModel_" + ComponentBox.SelectedValue.ToString() + "_" + selectedAlgorithms[i];
                MessageBox.Show(Procedure);
                //fiu.runModels(Procedure); //Starts the model building
            }

            BindDropDownModels();
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            ModelName.Clear();
            Description.Clear();
            
        }

        private void PredictionRunClicked(object sender, RoutedEventArgs e)
        {

        }

        private void checkModels_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PredictionsModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindPredictionAlgos(PredictionsModelName.SelectedValue.ToString());
        }

        private void PredictionsAlgorithmsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PredictionsAlgorithmsBox_TextChanged(object sender, TextChangedEventArgs e)
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

    public class MLPredictionInfo
    {
        public String ModelName { get; set; }

        public String Description { get; set; }

        public String ModelComponent { get; set; }

        public String ModelAlgos { get; set; }

        public Boolean CheckedStatus { get; set; }


    }

}
