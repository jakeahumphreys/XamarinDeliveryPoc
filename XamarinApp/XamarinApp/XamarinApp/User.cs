/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				User (User.cs)
* 
* Script Type:	    C# Script
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    Create a user object for use in table creation and data transfer.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using SQLite;
using Xamarin.Forms;

namespace XamarinApp
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string role { get; set; } //support for role based system. Currently hardcoded.
        public byte[] imageBase64 { get; set; }
        public string gps { get; set; }


        /// <summary>
        /// Override class toString method to print user details in one string
        /// </summary>
        /// <returns>
        /// {String} message
        /// </returns>
        public override string ToString()
        {
            string message = "[Username]: " + username + "\n"
                            + "[Password]: " + password + "\n"
                            + "[Firstname]: " + firstname + "\n"
                            + "[Surname]: " + surname + "\n"
                            + "[Role]: " + role + "\n"
                            + "[Base64 Image]: " + imageBase64 + "\n"
                            + "[GPS String]: " + gps.ToString();
                            

            return message;
        }
    }

    
}
