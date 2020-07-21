using NETLDashboard__.NET_Framework_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<MLPredictionInfo> predictionAlgorithmCheckList;
        List<String> SelectedPredAlgorithms;
        String algorithmsString = "";
        bool isSystemLevelChecked;
        bool isComponentLevelChecked;
        bool isSensorLevelChecked;

        // Using this array to build the string for each components stored procedure
        string[] componentNamesList = { "Furnace", "Boiler", "Stack", "Turbine" };

        string[] sensorNamesList = { "Temperature", "Pressure", "pH", "Vibration", "AirFlow", "Gas", "Particulate","WaterLevel" };

        public MachineLearningDashboard()
        {
            try
            {
                InitializeComponent();
                systemLevelRadioButton.IsChecked = true;
                algorithmList = new List<DDLAlgorithm>();
                builtModels = new List<MLPredictionInfo>();
                selectedAlgorithms = new List<String>();
                SelectedPredAlgorithms = new List<String>();
                predictionAlgorithmCheckList = new List<MLPredictionInfo>();
                //BindDropDown();
                BindDropDownAlgo();
                BindDropDownModels();
            }
            catch(Exception e)
            {
                MessageBox.Show("You must be connected to the FIU network to use this applicaion.","FIU Connection Not Found");
                Application.Current.Shutdown();
            }
            
        }

        private void AllComponents_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            BindListBox();
        }

        

        private void ComponentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindListBox();
        }

        

        private void BindListBox()
        {
            selectedAlgorithms.Clear();
           
            algorithmsString = "";
            int i = 0;
            foreach (var algorithm in algorithmList)
            {
                if (algorithm.CheckedStatus == true)
                {
                    selectedAlgorithms.Add(algorithm.AlgorithmName);
                    if (i == algorithmList.Count)
                    {
                        algorithmsString = algorithmsString + algorithm.AlgorithmName;
                    }
                    else
                    {
                        algorithmsString = algorithmsString + algorithm.AlgorithmName + ",";
                    }
                }
            }
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
            predictionAlgorithmCheckList.Clear();
            for(int i = 0; i < builtModels.Count; i++)
            {
                if(currentModel.Equals(builtModels[i].ModelName)) // Searches for the specific model name to pull the informoation from.
                {
                    String[] algos = builtModels[i].ModelAlgos.Split(','); //Splits the string thats stored in the coloumn of the db entry into separate algorithm names
                    for (int j = 0; j < algos.Length; j++)
                    {
                        temp.Add(new MLPredictionInfo()); //Populating the list with an empty object. Needs to be done this way to avoid binding errors.
                        temp[j].ModelAlgos = algos[j]; //Setting the empty object's ModelAlgo attribute to the algorithm name
                        predictionAlgorithmCheckList.Add(temp[j]);
                    }
                }

            }
            PredictionsAlgorithmsBox.ItemsSource = temp;//Passes the list of objects to the box.

        }

        // Makes a call to the database to retrieve all the machine learning algorithm names and adds to dropdown list
        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
           
            if(isSystemLevelChecked == true)
            {
                // checks to see that the user has input all necessary fields
                if (String.IsNullOrWhiteSpace(ModelName.Text) || String.IsNullOrWhiteSpace(Description.Text) || String.IsNullOrWhiteSpace(algorithmsString))
                {
                    MessageBox.Show("Please make sure that all fields are filled out before clicking run", "Required field(s) empty");
                    return;
                }

                for (int i = 0; i < builtModels.Count; i++)
                {
                    if (ModelName.Text == builtModels[i].ModelName)
                    {
                        MessageBox.Show("This model name already exists in the database", "Model exists in database");
                        return;
                    }
                }
                // Removing the comma at the end of the string
                algorithmsString = algorithmsString.Remove(algorithmsString.Length - 1);
                fiu.InsertModelRun(ModelName.Text, Description.Text, "test", "System Level", algorithmsString); //remove modelType
                
                //Getting Model ID from the just created row in the table                                                                                               //Getting Model ID from the just created row in the table
                int modelID = (fiu.getModelId(ModelName.Text));
                for (int i = 0; i < componentNamesList.Length; i++)
                {
                    for (int j = 0; j < selectedAlgorithms.Count; j++)
                    {
                        String Procedure = "SensorModel_" + componentNamesList[i] + "_" + selectedAlgorithms[j];
                        fiu.runModels(Procedure, modelID); //Starts the model building
                    }
                }

                for (int i = 0; i < sensorNamesList.Length; i++)
                {
                    for (int j = 0; j < selectedAlgorithms.Count; j++)
                    {
                        String Procedure = "SensorModel_" + sensorNamesList[i] + "_" + selectedAlgorithms[j];
                        fiu.runModels(Procedure, modelID); //Starts the model building
                    }
                }

                BindDropDownModels();

            }
            if (isComponentLevelChecked == true)
            {
                // checks to see that the user has input all necessary fields
                if (String.IsNullOrWhiteSpace(ModelName.Text) || String.IsNullOrWhiteSpace(Description.Text) || !(ComponentBox.SelectedIndex > -1) || String.IsNullOrWhiteSpace(algorithmsString))
                {
                    MessageBox.Show("Please make sure that all fields are filled out before clicking run", "Required field(s) empty");
                    return;
                }

                for (int i = 0; i < builtModels.Count; i++)
                {
                    if (ModelName.Text == builtModels[i].ModelName)
                    {
                        MessageBox.Show("This model name already exists in the database", "Model exists in database");
                        return;
                    }
                }

                // Removing the comma at the end of the string
                algorithmsString = algorithmsString.Remove(algorithmsString.Length - 1);

                //Inserting created model into DB
                fiu.InsertModelRun(ModelName.Text, Description.Text, "test", ComponentBox.SelectedValue.ToString(), algorithmsString); //remove modelType

                //Getting Model ID from the just created row in the table
                int modelID = (fiu.getModelId(ModelName.Text));

                // run stored procedure for selected component, also take into account multiple procedures

                for (int i = 0; i < selectedAlgorithms.Count; i++)
                {
                    String Procedure = "SensorModel_" + ComponentBox.SelectedValue.ToString() + "_" + selectedAlgorithms[i];

                    fiu.runModels(Procedure, modelID); //Starts the model building
                }

                BindDropDownModels();
            }
            if (isSensorLevelChecked == true)
            {
                // checks to see that the user has input all necessary fields
                if (String.IsNullOrWhiteSpace(ModelName.Text) || String.IsNullOrWhiteSpace(Description.Text) || !(ComponentBox.SelectedIndex > -1) || String.IsNullOrWhiteSpace(algorithmsString))
                {
                    MessageBox.Show("Please make sure that all fields are filled out before clicking run", "Required field(s) empty");
                    return;
                }

                for (int i = 0; i < builtModels.Count; i++)
                {
                    if (ModelName.Text == builtModels[i].ModelName)
                    {
                        MessageBox.Show("This model name already exists in the database", "Model exists in database");
                        return;
                    }
                }
                // Removing the comma at the end of the string
                algorithmsString = algorithmsString.Remove(algorithmsString.Length - 1);

                //Inserting created model into DB
                fiu.InsertModelRun(ModelName.Text, Description.Text, "test", ComponentBox.SelectedValue.ToString() + " sensor", algorithmsString); //remove modelType

                //Getting Model ID from the just created row in the table
                int modelID = (fiu.getModelId(ModelName.Text));

                // run stored procedure for selected component, also take into account multiple procedures

                for (int i = 0; i < selectedAlgorithms.Count; i++)
                {
                    String Procedure = "SensorModel_" + ComponentBox.SelectedValue.ToString() + "_" + selectedAlgorithms[i];

                    fiu.runModels(Procedure, modelID); //Starts the model building
                }

                BindDropDownModels();
            }
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            ModelName.Clear();
            Description.Clear();
            
        }

        private void PredictionRunClicked(object sender, RoutedEventArgs e)
        {
            List<int> algoIdList = new List<int>();
            List<MLValidationResults> algoResults = new List<MLValidationResults>();
            List<MLResults> results = new List<MLResults>();
            MLIdAndAlgo dataVal = new MLIdAndAlgo();
            resultsGrid.Children.Clear();
            try
            {
                String ModelName = PredictionsModelName.SelectedValue.ToString();
                dataVal = fiu.getModelAlgoandID(ModelName);
            } catch(NullReferenceException ex)
            {
     
                MessageBox.Show("Please make sure that you have selected a model and its corresponding algorithms",ex.Message);
            }
            
            

            for (int i = 0; i < SelectedPredAlgorithms.Count; i++)
            {
                algoIdList.Add(fiu.getAlgorithmId(SelectedPredAlgorithms[i]));
                fiu.getValidationResults(algoResults, dataVal.ModelId, algoIdList[i]);

            }



            for (int i = 0; i < algoResults.Count; i++)
            {
                resultsGrid.RowDefinitions.Add(new RowDefinition());
                resultsGrid.RowDefinitions[i].Height = new GridLength(200);
                results.Add(new MLResults(algoResults[i].AlgorithmName, algoResults[i].ComponentName, "Accuracy: " + (100 * algoResults[i].SimilarityScore).ToString("#.00") + "%", algoResults[i].Result, algoResults[i].SimilarityScore));
                Console.WriteLine(algoResults[i].AlgorithmName);
                Console.WriteLine(algoResults[i].ComponentName);
                Console.WriteLine(algoResults[i].SimilarityScore);
                Console.WriteLine(algoResults[i].Result);
                resultsGrid.Children.Add(results[i]);
                Grid.SetRow(results[i], i);
            }
        }

       


        private void PredictionsModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindPredictionAlgos(PredictionsModelName.SelectedValue.ToString());
        }

        private void PredictCheckAlgos_Checked(object sender, RoutedEventArgs e)
        {
            SelectedPredAlgorithms.Clear();
            foreach (var pred in predictionAlgorithmCheckList)
            {
                if (pred.CheckedStatus == true)
                {
                 
                    SelectedPredAlgorithms.Add(pred.ModelAlgos);
                    
                }
            }

        }

        private void SystemLevel_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isSystemLevelChecked = true;
            isComponentLevelChecked = false;
            isSensorLevelChecked = false;
            ComponentBox.Visibility = Visibility.Hidden;
            selectCompLabel.Visibility = Visibility.Hidden;
        }

        private void ComponentLevel_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isComponentLevelChecked = true;
            isSensorLevelChecked = false;
            isSystemLevelChecked = false;
            ComponentBox.ItemsSource = componentNamesList;
            ComponentBox.Visibility = Visibility.Visible;
            selectCompLabel.Visibility = Visibility.Visible;
            selectCompLabel.Content = "Select Component:";
        }

        private void SensorLevel_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            isSensorLevelChecked = true;
            isSystemLevelChecked = false;
            isComponentLevelChecked = false;
            selectCompLabel.Visibility = Visibility.Visible;
            ComponentBox.Visibility = Visibility.Visible;
            ComponentBox.ItemsSource = sensorNamesList;
            selectCompLabel.Content = "Select Sensor:";
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

        public Boolean CheckedStatus{ get; set; }

    }
    // Class to hold an id and comma seperated algorithms 
    public class MLIdAndAlgo
    {
        public String ModelId { get; set; }

        public String ModelAlgos { get; set; }

    }

    public class MLValidationResults
    {
        public double SimilarityScore { get; set; }

        public String ComponentName { get; set; }

        public String Result { get; set; }

        public String AlgorithmName { get; set; }
    }

}
