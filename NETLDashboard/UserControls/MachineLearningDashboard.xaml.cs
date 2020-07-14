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
        List<DDLAlgorithm> algorithmList;
        List<MLPredictionInfo> builtModels;
        List<String> selectedAlgorithms;
        String algorithmsString = "";

        public MachineLearningDashboard()
        {
            InitializeComponent();
            algorithmList = new List<DDLAlgorithm>();
            builtModels = new List<MLPredictionInfo>();
            selectedAlgorithms = new List<String>();

            //BindDropDown();
            BindDropDownAlgo();
            BindDropDownModels();
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

        private void ComponentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindListBox();
        }


        private void BindDropDownAlgo()
        {
            fiu.getAlgorithmNames(algorithmList);
            AlgorithmsBox.ItemsSource = algorithmList;
        }

        private void BindDropDownModels()
        {
            if (builtModels.Count == 0)
            {
                fiu.getBuiltModels(builtModels);

                List<String> modelNames = new List<String>();
                for (int i = 0; i < builtModels.Count; i++)
                {
                    modelNames.Add(builtModels[i].ModelName);
                }
                PredictionsModelName.ItemsSource = modelNames;
            }
            else
            {
                builtModels.Clear();

                fiu.getBuiltModels(builtModels);

                List<String> modelNames = new List<String>();
                for (int i = 0; i < builtModels.Count; i++)
                {
                    modelNames.Add(builtModels[i].ModelName);
                }
                PredictionsModelName.ItemsSource = modelNames;
            }
        }

        private void BindPredictionAlgos(String currentModel)
        {
            List<MLPredictionInfo> temp = new List<MLPredictionInfo>();
            for(int i = 0; i < builtModels.Count; i++)
            {
                if(currentModel.Equals(builtModels[i].ModelName)) // Searches for the specific model name to pull the informoation from.
                {
                    String[] algos = builtModels[i].ModelAlgos.Split(','); //Splits the string thats stored in the coloumn of the db entry into separate algorithm names
                    for (int j = 0; j < algos.Length; j++)
                    {
                        temp.Add(new MLPredictionInfo()); //Populating the list with an empty object. Needs to be done this way to avoid binding errors.
                        temp[j].ModelAlgos = algos[j]; //Setting the empty object's ModelAlgo attribute to the algorithm name
                    }
                }

            }
            PredictionsAlgorithmsBox.ItemsSource = temp;//Passes the list of objects to the box.

        }

        // Makes a call to the database to retrieve all the machine learning algorithm names and adds to dropdown list
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


        private void PredictionsModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindPredictionAlgos(PredictionsModelName.SelectedValue.ToString());
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

    }

}
