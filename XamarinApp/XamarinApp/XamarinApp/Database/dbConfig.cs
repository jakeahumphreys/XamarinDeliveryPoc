/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				dbConfig (dbConfig.cs)
* 
* Script Type:		C# Script
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    Database configuration class.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
namespace XamarinApp.Database
{
    public class dbConfig
    {
        //name of database file, use .db3 or .sqlite3
        private string dbName = "localDatabase.db3";
        //determines the method in which the app will operate with the database
        //GATEWAY: Routes commands through the userGateway and uses plain sql.
        //OBJECT: Uses sqlite-pcl-net's object based approach.
        private string databaseMode = "GATEWAY";

        /// <summary>
        /// Return database name.
        /// </summary>
        /// <returns>
        /// {String} dbName
        /// </returns>
        public string getDatabaseName()
        {
            return dbName;
        }

        public string getDatabaseMode()
        {
            return databaseMode;
        }
    }
}
