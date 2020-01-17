/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				SqliteIOS (SqliteIOS.cs)
* 
* Script Type:		C# Script
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    IOS SQLite implementation script.
*                   
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using XamarinApp.Database;
using XamarinApp.iOS;
using SQLite;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SqliteIOS))]
namespace XamarinApp.iOS
{
    public class SqliteIOS : ISql
    {
        /// <summary>
        /// Attempt to connect to local database
        /// </summary>
        /// <returns>SQLiteConnection</returns>
        public SQLiteConnection GetConnection()
        {
            string dbase = "";
            string path = "";
            string documentsPath = "";
            SQLiteConnection connection = null;
            dbConfig dbConfig;

            try
            {
                dbConfig = new dbConfig(); //initialise database config class

                dbase = dbConfig.getDatabaseName();
                documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
               
                path = Path.Combine(documentsPath, dbase);
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