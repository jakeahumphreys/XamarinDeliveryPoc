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
* Purpose:		    SQLite getConnection interace.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using SQLite;

namespace XamarinApp
{
    //shared SQLiteConnection
    public interface ISql
    {
        SQLiteConnection GetConnection();
    }
}
