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
                //Initailize the lists that will be used throughout the code, and set the default level to be sensor level.
                systemLevelRadioButton.IsChecked = true;
                algorithmList = new List<DDLAlgorithm>();
                builtModels = new List<MLPredictionInfo>();
                selectedAlgorithms = new List<String>();
                SelectedPredAlgorithms = new List<String>();
                predictionAlgorithmCheckList = new List<MLPredictionInfo>();

                //The following are used to initialize the drop down menus with their values dynamically.
                BindDropDownAlgo(); 
                BindDropDownModels();
            }
            catch(Exception e) //We couldn't figure out what exception type to make it since its used for checking if the user is on the correct network, so a generic one.
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

        
        /*
         This function is used to gather the input from the model building page, and store them in lists to be used in later functions.
         */
        private void BindListBox()
        {
            selectedAlgorithms.Clear(); //Clear the selected list
           
            algorithmsString = "";
            int i = 0;
            foreach (var algorithm in algorithmList)
            {
                if (algorithm.CheckedStatus == true)
                {
                    selectedAlgorithms.Add(algorithm.AlgorithmName);
                    if (i == algorithmList.Count)//This loop is used to make sure only the current selection is in the string.
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

        //Dynamically populates the drop down box with the correct values
        private void BindDropDownAlgo()
        {
            fiu.getAlgorithmNames(algorithmList);
            AlgorithmsBox.ItemsSource = algorithmList;
        }

        //Dynamically populates the drop down box with the correct values
        private void BindDropDownModels()
        {
            if (builtModels.Count == 0)// Basic switch to make sure the list gets populated correctly.
            {
                fiu.getBuiltModels(builtModels);

                List<String> modelNames = new List<String>();
                for (int i = 0; i < builtModels.Count; i++)
                {
                    modelNames.Add(builtModels[i].ModelName);
                }
                PredictionsModelName.ItemsSource = modelNames;
            }
            else //Clears the list since it was populated once already, reducing redundant values in the box.
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

        //Dynamically populates the drop down box with the correct values
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

        /* Stats the model building process, depending on the level overview, the function does something different. System level builds models for 
         the entire system, including the individual sensors and the components. The component level just builds models for the components, and the sensor level
        just builds models for the sensors.*/
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

                for (int i = 0; i < builtModels.Count; i++) //verifies if the model name exists in the database already. If it does, the user has to rename the model.
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
                        fiu.runModels(Procedure, modelID); //Starts the model building for the components
                    }
                }

                for (int i = 0; i < sensorNamesList.Length; i++)
                {
                    for (int j = 0; j < selectedAlgorithms.Count; j++)
                    {
                        String Procedure = "SensorModel_" + sensorNamesList[i] + "_" + selectedAlgorithms[j];
                        fiu.runModels(Procedure, modelID); //Starts the model building for the sensors
                    }
                }

                BindDropDownModels(); //Updates the built models drop down on the prediction section.

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

                BindDropDownModels(); //Updates the built models drop down on the prediction section.
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

                BindDropDownModels(); //Updates the built models drop down on the prediction section.
            }
        }

        //Resets the fields. 
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
            
            //This takes the selected algorithms from the predictions page and passes them into the database function to filter the results and pull the correct ones.
            for (int i = 0; i < SelectedPredAlgorithms.Count; i++)
            {
                algoIdList.Add(fiu.getAlgorithmId(SelectedPredAlgorithms[i]));
                fiu.getValidationResults(algoResults, dataVal.ModelId, algoIdList[i]);

            }

            //Takes the results from the previous database and plots them onto the machine learning page.
            for (int i = 0; i < algoResults.Count; i++)
            {
                resultsGrid.RowDefinitions.Add(new RowDefinition());
                resultsGrid.RowDefinitions[i].Height = new GridLength(200);
                results.Add(new MLResults(algoResults[i].AlgorithmName, algoResults[i].ComponentName, "Accuracy: " + (100 * algoResults[i].SimilarityScore).ToString("#.00") + "%", algoResults[i].Result, algoResults[i].SimilarityScore));
                resultsGrid.Children.Add(results[i]);
                Grid.SetRow(results[i], i);
            }
        }

        //Event handler for the model name drop down menu on the predictions side
        private void PredictionsModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindPredictionAlgos(PredictionsModelName.SelectedValue.ToString());
        }

        //Event handler for the algorithms drop down menu on the predictions side.
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

        //The next three functions are self explanitory. 
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

    //Used in the algorithm drop down list, this is to create an object of it with the ID and name
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
    //An object used for the Prediction section
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

    // Class to hold the values pulled from the database to be plotted
    public class MLValidationResults
    {
        public double SimilarityScore { get; set; }

        public String ComponentName { get; set; }

        public String Result { get; set; }

        public String AlgorithmName { get; set; }
    }

}
