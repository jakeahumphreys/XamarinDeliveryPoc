/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				Display (Display.xaml.cs)
* 
* Script Type:		XML
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                
* Author:           FHL 
*
* Purpose:		    Display users in database.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using XamarinApp.Database;
using System.Collections.Generic;
using XamarinApp.Gateways;

namespace XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Display : ContentPage
    {
        public SQLiteConnection conn;
        public User user;
        public User loggedInUser = null;
        public dbConfig dbConfig;
        public userGateway gateway;

        /// <summary>
        /// Initialise user interface and create table if none exists
        /// </summary>
        public Display()
        {
            InitializeComponent();
            conn = DependencyService.Get<ISql>().GetConnection(); //choose relevent SQLite class
            conn.CreateTable<User>(); //create table if none exists
            dbConfig = new dbConfig();
            gateway = new userGateway();
            DisplayDetails();
        }

        /// <summary>
        /// Retrieve all users from the database and display in a listview.
        /// </summary>
        private void DisplayDetails()
        {
            List<User> userList;
            switch(dbConfig.getDatabaseMode())
            {
                case "GATEWAY":
                    System.Diagnostics.Debug.WriteLine("[APP] > Using GATEWAY mode");
                    userList = gateway.getAll();
                    myListView.ItemsSource = userList;
                    break;
                case "OBJECT":
                    System.Diagnostics.Debug.WriteLine("[APP] > Using OBJECT mode");
                    var details = (from x in conn.Table<User>() select x).ToList();
                    myListView.ItemsSource = details;
                    break;
            }
        }

        /// <summary>
        /// Get the user object of the selected item from the list and pass to the EditUser page.
        /// Display selected user's toString()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            User selectedUser = (User)myListView.SelectedItem;
            //DisplayAlert("SelectedItem", selectedUser.ToString(), "Ok");
            App.Current.MainPage = new EditUser(selectedUser);
        }

        /// <summary>
        /// Navigate to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void onBackButtonPressed(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}