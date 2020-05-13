using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETLDashboard__.NET_Framework_
{
    class Db
    {

        readonly SqlConnection connection; //Variable that will hold the open connection to the database.
        //The constructor that initiates the connection to the database.
        public Db()
        {
            //Server information goes here
        }

        //The print function is just used for console debugging, it prints out temperature value in each row of the database
        public void print()
        {
            SqlDataAdapter sdlDA = new SqlDataAdapter("select * from SensorData", connection);
            DataTable dtbl = new DataTable();
            sdlDA.Fill(dtbl);
            foreach (DataRow row in dtbl.Rows)
            {
                Trace.WriteLine(row["SensorValue"]);
            }
        }
        //getDBArray is used for non-live updates to the database. It gets all the values and stores it in an array.
        public float[] getDBArray()
        {

            SqlDataAdapter sdlDA = new SqlDataAdapter("select * from SensorData", connection);
            DataTable dtbl = new DataTable();
            sdlDA.Fill(dtbl);
            float[] DBArray = new float[dtbl.Rows.Count];
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                DBArray[i] = float.Parse(dtbl.Rows[i]["SensorValue"].ToString());
                Trace.WriteLine(DBArray[i]);
            }

            return DBArray;
        }
        //getLastEntry reads the last line in the database, and formats it so the temperature sensors value is all that remains.
        public double getLastEntry()
        {
            double lastValue = 0.0;
            SqlCommand command = new SqlCommand("SELECT TOP 1 SensorValue From SensorData ORDER BY InsertedOn desc;", connection);
            connection.Open(); //Opens the connection to the database.
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                lastValue = double.Parse(reader[0].ToString());
            }

            connection.Close(); //Closes the connection to the database.
            return lastValue;
        }
    }

}
