/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				userGateway (userGateway.cs)
* 
* Script Type:		C#
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    Contain the Create, Read, Update and Delete functions for the database.
*                    
* Notes:			This implementation uses visible sql strings as opposed to objects.
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace XamarinApp
{
    public class userGateway
    {

        SQLiteConnection conn = DependencyService.Get<ISql>().GetConnection();

        /// <summary>
        /// Get all users in database.
        /// Since 1.0.0
        /// </summary>
        /// <returns> List </returns>
        public List<User> getAll()
        {
            List<User> userList = new List<User>();
            string sqlString = "";

            try
            {
                sqlString = "SELECT * FROM User";
                userList = conn.Query<User>(sqlString);
                System.Diagnostics.Debug.WriteLine(userList.Count);
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return userList;
        }

        /// <summary>
        /// Retrieve a single user from the database
        /// Since 1.0.0
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User object</returns>
        public User get(string username)
        {
            User userdetails = null;
            string sqlString = "";

            try
            {
                sqlString = "SELECT * FROM User WHERE username=@username";
                
                var result = conn.Query<User>(sqlString, username);

                userdetails = result.ElementAt(0);

            }
            catch(SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return userdetails;
        }

        /// <summary>
        /// Insert a user into the database.
        /// Since 1.0.0
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean</returns>
        public bool insert(User user)
        {
            bool returnVal = false;
            string sqlString = "";
            int result = 0;
            try
            {
                sqlString = "INSERT INTO  User (username, password, firstname, surname, role, imageBase64, gps) VALUES (@username,@password,@firstname,@surname,@role,@imageBase64,@gps)";
                result = conn.Execute(sqlString, user.username, user.password, user.firstname, user.surname, user.role, user.imageBase64, user.gps);

                if (result == 1)
                {
                    returnVal = true;
                }
               
            }
            catch(SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return returnVal;
        }

        /// <summary>
        /// Update a user in the database.
        /// Since 1.0.0
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean</returns>
        public bool update(User user)
        {
            bool returnVal = false;
            string sqlString = "";
            int result = 0;

            try
            {
                sqlString = "UPDATE User SET password=@password WHERE username=@username";
                result = conn.Execute(sqlString, user.password, user.surname);
                System.Diagnostics.Debug.WriteLine(result);

                if (result == 1)
                {
                    returnVal = true;
                }
            }
            catch(SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return returnVal;
        }

        /// <summary>
        /// Delete a user from the database/
        /// Since 1.0.0
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean</returns>
        public bool delete(User user)
        {
            bool returnVal = false;
            string sqlString = "";
            int result = 0;

            try
            {
                sqlString = "DELETE FROM User WHERE username=@username";
                result = conn.Execute(sqlString, user.username);

                if(result == 1)
                {
                    returnVal = true;
                }
            }
            catch(SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return returnVal;
        }
    }
}
