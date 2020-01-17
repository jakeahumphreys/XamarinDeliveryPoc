/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				EditUser (EditUser.xaml.cs)
* 
* Script Type:		XML
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                   1.1.0 - 06/09/2019 - Added Image display - JAH
*                   1.2.0 - 09/09/2019 - Added GPS Field for gps functionality - JAH
*                   1.2.1 - 11/09/2019 - Added 'View on map' button functionality - JAH
*                
* Author:           FHL 
*
* Purpose:		    View the details of the selected user.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using System.Collections.Generic;

namespace XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        /// <summary>
        /// Set entry field text to fields from the user object parameter.
        /// </summary>
        /// <param name="editUser"></param>
        public EditUser(User editUser)
        {
            InitializeComponent();

            //disable editing of text fields
            username.IsEnabled = false;
            password.IsEnabled = false;
            firstname.IsEnabled = false;
            surname.IsEnabled = false;

            //set text fields to contents of user object
            username.Text = editUser.username;
            password.Text = editUser.password;
            firstname.Text = editUser.firstname;
            surname.Text = editUser.surname;
            gps.Text = editUser.gps;

            PhotoImage.Source = ImageSource.FromStream(() => new MemoryStream(editUser.imageBase64));
        }

        /// <summary>
        /// 1.0.0
        /// navigate to display page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnBackButtonClicked(Object sender, EventArgs e)
        {
            App.Current.MainPage = new Display();
        }

        /// <summary>
        /// 1.2.1
        /// Navigate from current location to GPS location from user's gps field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnMapButtonClicked(object sender, EventArgs e)
        {
            NavigationType navigationType = NavigationType.Default;
            var gpsList = new List<string>(gps.Text.Split(','));
            var success = await CrossExternalMaps.Current.NavigateTo("", (Convert.ToDouble(gpsList[1])), (Convert.ToDouble(gpsList[2])),navigationType);
        }
    }

}
