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
        List<MLPredictionInfo> predictionAlgorithmCheckList;
        List<String> SelectedPredAlgorithms;
        String algorithmsString = "";

        public MachineLearningDashboard()
        {
            InitializeComponent();
            algorithmList = new List<DDLAlgorithm>();
            builtModels = new List<MLPredictionInfo>();
            selectedAlgorithms = new List<String>();
            SelectedPredAlgorithms = new List<String>();
            predictionAlgorithmCheckList = new List<MLPredictionInfo>();
            //BindDropDown();
            BindDropDownAlgo();
            BindDropDownModels();

            for(int i = 0; i<7; i++)
            {
                resultsGrid.RowDefinitions.Add(new RowDefinition());
                resultsGrid.RowDefinitions[i].Height = new GridLength(150);
            }

           
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
            // Removing the comma at the end of the string
            algorithmsString = algorithmsString.Remove(algorithmsString.Length - 1);

            //Inserting created model into DB
            fiu.InsertModelRun(ModelName.Text, Description.Text, "test", ComponentBox.SelectedValue.ToString(), algorithmsString); //remove modelType
            SelectionList.Items.Add("Your entry has been created");

            //Getting Model ID from the just created row in the table
           int modelID = (fiu.getModelId(ModelName.Text));

            // run stored procedure for selected component, also take into account multiple procedures

            for(int i = 0; i < selectedAlgorithms.Count; i++)
            {
                String Procedure = "SensorModel_" + ComponentBox.SelectedValue.ToString() + "_" + selectedAlgorithms[i];
                MessageBox.Show(Procedure);
                fiu.runModels(Procedure, modelID); //Starts the model building
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
           String ModelName = PredictionsModelName.SelectedValue.ToString();
           MLIdAndAlgo dataVal = new MLIdAndAlgo();
           dataVal = fiu.getModelAlgoandID(ModelName);
            
        for (int i =0; i<SelectedPredAlgorithms.Count; i++)
            {
                Console.WriteLine(SelectedPredAlgorithms[i]);
            }



















            MLResults ml1 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 20.54);
            MLResults ml2 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 99.9);
            MLResults ml3 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 45.2);
            MLResults ml4 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 57.2);
            MLResults ml5 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 69.69);
            MLResults ml6 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 13.37);
            MLResults ml7 = new MLResults("OneClassSVM", "Furnace", "Accuracy 50%", "4.564", 2.54);

            resultsGrid.Children.Add(ml1);
            resultsGrid.Children.Add(ml2);
            resultsGrid.Children.Add(ml3);
            resultsGrid.Children.Add(ml4);
            resultsGrid.Children.Add(ml5);
            resultsGrid.Children.Add(ml6);
            resultsGrid.Children.Add(ml7);

            Grid.SetRow(ml1, 0);
            Grid.SetRow(ml2, 1);
            Grid.SetRow(ml3, 2);
            Grid.SetRow(ml4, 3);
            Grid.SetRow(ml5, 4);
            Grid.SetRow(ml6, 5);
            Grid.SetRow(ml7, 6);


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

}
