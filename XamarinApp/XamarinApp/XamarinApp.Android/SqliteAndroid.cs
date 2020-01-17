/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				SqliteAndroid (SqliteAndroid.cs)
* 
* Script Type:		C# Script
* 
* Version:			1.0.0 - 03/09/2019 - Initial Version - JAH.
*                
* Author:           FHL 
*
* Purpose:		    Android SQLite implementation script.
*                   
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using XamarinApp.Database;
using XamarinApp.Droid;


[assembly: Dependency(typeof(SqliteAndroid))]
namespace XamarinApp.Droid
{
    public class SqliteAndroid : ISql
    {
        /// <summary>
        /// Attmept to connect to a local database
        /// </summary>
        /// <returns>
        /// SQLiteConnection
        /// </returns>
        public SQLiteConnection GetConnection()
        {
            string dbase = "";
            string dbpath = "";
            string path = "";
            SQLiteConnection connection = null;
            dbConfig dbConfig;
      
            try
            {
                dbConfig = new dbConfig(); //initialise database config class
                dbase = dbConfig.getDatabaseName(); //get database name from database config class
                dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(dbpath, dbase); 
                connection = new SQLiteConnection(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return connection;
        }
    }
}