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
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    UWP SQLite implementation script.
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
using XamarinApp.UWP;
using Windows.Storage;
using System.Runtime.CompilerServices;

[assembly: Dependency(typeof(SqliteWindows))]
namespace XamarinApp.UWP
{
    public class SqliteWindows : ISql
    {
        /// <summary>
        /// Attempt to connect to local database
        /// </summary>
        /// <returns>
        /// {SQLiteConnection} connection
        /// </returns>
        public SQLiteConnection GetConnection()
        {
            string dbase = "";
            string path = "";
            SQLiteConnection connection = null;
            dbConfig dbConfig;

            try
            {
                dbConfig = new dbConfig(); //initialise database config class
                dbase = dbConfig.getDatabaseName(); //get database file name
                path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbase);
                connection = new SQLiteConnection(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return connection;
        }
    }
}