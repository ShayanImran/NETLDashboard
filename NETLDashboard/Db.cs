using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NETLDashboard.UserControls;


namespace NETLDashboard__.NET_Framework_
{
    class Db
    {
        readonly SqlConnection connection; //Variable that will hold the open connection to the database.

        //The constructor that initiates the connection to the database.
        public Db()
        {
            string connectionString = File.ReadAllText("..\\..\\Database_Connection_String.txt"); //Database information is stored in seperate file for security and is loaded upon start
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
        }

        // Gets the stored procedure as an input parameter and pulls the appropriate value,  used for the live graphs of sensor data.
        public double getLastVirtualEntry(String procedureName)
        {

            double lastValue = 0.0;
            SqlCommand command = new SqlCommand(procedureName, connection);

            connection.Open(); //Opens the connection to the database.
            using (SqlDataReader reader = command.ExecuteReader()) //Limits the scope of the command to just read while it's in this block.
            {
                reader.Read();
                lastValue = double.Parse(reader[0].ToString()); //If a double value is detected in a string, it gets converted to be a double.
            }

            connection.Close(); //Closes the connection to the database.
            return lastValue;
        }

        /* The point of this function below is to try and get the datetime value and 
            point TOGETHER in ONE list and then plot it to the historical graph, which is different from the getHistoricalData function which just
            gets all the stored values of one sensor and stores it as a double value. DateTimePoint is a data type defined in LiveCharts. */
        public List<DateTimePoint> getHistoricalDataPoints(String procedureName, String start, String end)
        {
            List<DateTimePoint> data = new List<DateTimePoint>();
            SqlCommand command = new SqlCommand(procedureName, connection); //Reads all the column data from the SensorData table
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@startDate", start));//The beginning value of the date range, linked to the variable in the stored procedure.
            command.Parameters.Add(new SqlParameter("@endDate", end));//The end date of the date range, linked to the variable in the stored procedure.
            connection.Open();// Opens the connection
            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read())//Reads all the data that was stored within the specified date range.
                {
                    data.Add(new DateTimePoint(Convert.ToDateTime(reader[1]), double.Parse(reader[0].ToString())));//Gets the first sensor value and inserted on time at the iterator of the reader.
                }
            }

            connection.Close(); // closes the connection to the database
            return data;
        }

        //getAlgorithmNames is a function used for the machine learning page(s). It populates the drop-down menus on the page.
        public void getAlgorithmNames( List<DDLAlgorithm> algoList) //DDLAlgorith is a custom class created specifically for the drop-dowm menus.
        {
            SqlCommand command = new SqlCommand("Algorithm_GetAlgorithmNames", connection); //Reads all the column data from the SensorData table
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();// Opens the connection

            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read())//Loops until there are no more algorithms found.
                {
                    DDLAlgorithm thing = new DDLAlgorithm();
                    thing.AlgorithmName = reader[1].ToString();//reader[1] is the second column in the database entry
                    thing.AlgorithmId = int.Parse(reader[0].ToString());//reader[0] is the first column in the database entry.
                    algoList.Add(thing);//Adds the completed object to a list.
                }
            }
            connection.Close(); // closes the connection to the database
        }
        /* The InsertModelRun takes the information entered on the model building page, and inserts it into the database's 
         * ModelMaster table.*/
        public void InsertModelRun(String modelName, String description, String modelType, String component, String algorithms)
        {
            SqlCommand command = new SqlCommand("ModelMaster_InsertModel", connection); //Sets up the connection for the stored procedure
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@modelName", modelName));//Links the value to the stored procedure variable
            command.Parameters.Add(new SqlParameter("@description", description));//Links the value to the stored procedure variable
            command.Parameters.Add(new SqlParameter("@modelType", modelType));//Links the value to the stored procedure variable
            command.Parameters.Add(new SqlParameter("@component", component));//Links the value to the stored procedure variable
            command.Parameters.Add(new SqlParameter("@algorithms", algorithms));//Links the value to the stored procedure variable
            connection.Open();// Opens the connection
            command.ExecuteNonQuery(); //This is used because pulling data from the database is not required, since this is an insert.
            Console.WriteLine("Entry Created");
            
            connection.Close(); // closes the connection to the database
        }

        //runModels is used to run the selected algorithms the user wants a model of.
        public void runModels(String Procedure, int ModelId)
        {
            SqlCommand command = new SqlCommand(Procedure, connection); //Opens a connection to the db with the stored procedure for a specific algorithm
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ModelID", ModelId));//passes in the modelId to the stored proedure
            connection.Open();// Opens the connection
            command.CommandTimeout = 200; // Allows the model to run for 200 seconds before it throws an error.
            command.ExecuteNonQuery();//Starts the machine learning procedure with the sql command, then closes it once the scope ends.
          
            connection.Close(); // closes the connection to the database
        }

        //getModelId is used to get the modelId after it was inserted into the database.
        public int getModelId(String ModelName)
        {
            int temp = 0;
            SqlCommand command = new SqlCommand("ModelMaster_GetID", connection); //Returns the entire ModelMaster table
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ModelName", ModelName));// Specifies what row to select data from.
            connection.Open();// Opens the connection

            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read())
                {
                    temp = int.Parse(reader[0].ToString()); //returns the ModelId for the ModelName
                }
            }

            connection.Close(); // closes the connection to the database
            return temp;
        }
        // Function that retrieves the modelID and the Alogrithms used for the model
        public MLIdAndAlgo getModelAlgoandID(String ModelName)
        {
            MLIdAndAlgo Mlobj = new MLIdAndAlgo();
            SqlCommand command = new SqlCommand("ModelMaster_GetID", connection); //Reads all the column data from the SensorData table
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ModelName", ModelName));
            connection.Open();// Opens the connection

            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read())
                {
                    Mlobj.ModelId = reader[0].ToString(); //Reads in the ModelId from the row
                    Mlobj.ModelAlgos = reader[1].ToString(); //Gets the algorithm(s) string from table
                }
            }

            connection.Close(); // closes the connection to the database
            return Mlobj;
        }
        //getBuiltModels is used to populate the drop down list of built models on the predictions page.
        public void getBuiltModels(List<MLPredictionInfo> modelList)
        {
            SqlCommand command = new SqlCommand("ModelMaster_GetModels", connection); //Reads all the column data from the ModelMaster table
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();// Opens the connection

            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read()) //loops until there are no more models found.
                {
                    MLPredictionInfo thing = new MLPredictionInfo(); //The object to hold all the values from the table.
                    thing.ModelName = reader[0].ToString();
                    thing.Description = reader[1].ToString();
                    thing.ModelComponent = reader[2].ToString();
                    thing.ModelAlgos = reader[3].ToString();
                    
                    modelList.Add(thing);//Adds the object to a list to populate the dropdown menu.
                }
            }
            connection.Close(); // closes the connection to the database
        }

        //getAlgorithmId is used to get the id of the algorithm on the Algorithms table.
        public int getAlgorithmId(String algoName)
        {
            int id = 0;
            SqlCommand command = new SqlCommand("Algorithm_GetAlgorithmId", connection); //Reads all the column data from the Algorithm table
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AlgorithmName", algoName)); //Sets the stored procedure's variable to the passed in value.
            connection.Open();// Opens the connection
            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString()); //Returns the algorithm id based on the name passed in.
                }
            }
            connection.Close(); // closes the connection to the database
            return id;
        }

        //getValidationResults is used to plot the selected model from the prediction page.
        public void getValidationResults(List<MLValidationResults> data, String modelID, int algoID)
        {
            
            SqlCommand command = new SqlCommand("ModelValidation_GetMachineLearningResults", connection); //Reads all the column data from the ModelValidation table
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ModelID", int.Parse(modelID))); //Sets the procedure variable to the passed in parameter.
            command.Parameters.Add(new SqlParameter("@AlgorithmID", algoID)); //Sets the procedure variable to the passed in parameter.
            connection.Open();// Opens the connection
            using (SqlDataReader reader = command.ExecuteReader())//Starts the reading process with the sql command, then closes it once the scope ends.
            {
                while (reader.Read()) //Reads all the rows that match the ModelId and AlgorithmId
                {
                    MLValidationResults temp = new MLValidationResults();

                    if (String.IsNullOrWhiteSpace(reader[1].ToString()))
                    {
                        temp.ComponentName = reader[4].ToString();
                    }
                    else
                    {
                        temp.ComponentName = reader[1].ToString();
                    }

                    temp.SimilarityScore = double.Parse(reader[0].ToString());

                   
                    temp.AlgorithmName = reader[2].ToString();
                    temp.Result = reader[3].ToString();
                    data.Add(temp);//Adds it to a list to populate dynamically in window.

                }
            }
            connection.Close(); // closes the connection to the database
        }



    }

}
