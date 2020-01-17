/**********************************************************************************************************
* Copyright © FHL.
* All Rights Reserved.
* This is the confidential and proprietary information of FHL.
* The misuse of is strictly prohibited, in accordance with the terms of your agreement
* with FHL.
*
* Name:				MainPage (MainPage.xaml.cs)
* 
* Script Type:		XML
* 
* Version:			1.0.0 - 03/09/2019 - Initial Release - JAH.
*                   1.1.0 - 06/09/2019 - Added Camera functionality - JAH
*                   1.2.0 - 09/09/2019 - Added GPS Functionality - JAH
*                
* Author:           FHL 
*
* Purpose:		    Add users to a local SQLite database.
*                    
* Notes:			
* 
* Dependencies:		
***********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using XamarinApp;
using XamarinApp.Database;
using System.IO;
using System;
using System.Text;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using XamarinApp.Gateways;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public SQLiteConnection conn;
        public User user;
        public string location;
        public User newUser = new User();
        public bool allowGpsOperations = false;
        public userGateway gateway = new userGateway();
        public dbConfig dbConfig;

        string errorReason = "Failed to update database.";

        /// <summary>
        /// Initialise user interface and create table if none exists
        /// </summary>
        public MainPage()
        {
            try
            {
                InitializeComponent();
                conn = DependencyService.Get<ISql>().GetConnection();
                conn.CreateTable<User>(); //if table doesn't exist, create it.

                //Camera
                CameraButton.Clicked += CameraButton_Clicked;
                Plugin.Media.CrossMedia.Current.Initialize();

                //GPS

                GetLocation();

                if (location != null)
                {
                    allowGpsOperations = true;
                    System.Diagnostics.Debug.WriteLine("[GPS_SERVICE] > Started.");
                }

                //Initialise dbconfig
                dbConfig = new dbConfig();


            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

        /// <summary>
        /// Clear entry fields on user interface
        /// </summary>
        void clearTextFields()
        {
            username.Text = "";
            password.Text = "";
            firstname.Text = "";
            surname.Text = "";
        }
       
        /// <summary>
        /// Store each field value in a user object and insert object into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnUserAddButtonClicked(object sender, EventArgs e)
        {
           
            int i = 0;
            bool sqlResult = false;

            newUser.username = username.Text;
            newUser.password = password.Text;
            newUser.firstname = firstname.Text;
            newUser.surname = surname.Text;
            newUser.role = "defaultRole";

            if(CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationAvailable)
            {
                GetLocation();
                newUser.gps = location;
            }
            else
            {
                newUser.gps = "GPS SERVICE NOT AVAILABLE";
            }

            //Depending on the databaseMode variable, switch between object based sql or gateway/raw sql strings.

            try
            {
                switch(dbConfig.getDatabaseMode())
                {
                    case "GATEWAY":
                        System.Diagnostics.Debug.WriteLine("[APP] > Using GATEWAY mode");
                        switch(usernameCheck(newUser))
                        {
                            case false:
                                sqlResult = gateway.insert(newUser);
                                if (sqlResult)
                                {
                                    i = 1;
                                }
                                break;
                            case true:
                                errorReason = "A user with that username already exists.";
                                break;
                        }
                        break;
                    case "OBJECT":
                        System.Diagnostics.Debug.WriteLine("[APP] > Using OBJECT mode");
                        switch (usernameCheck(newUser))
                        {
                            case false:
                                i = conn.Insert(newUser);
                                clearTextFields();
                                break;
                            case true:
                                errorReason = "A user with that username already exists.";
                                break;
                        }
                        break;
                }     
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "Ok");
            }

            if (i == 1)
            {
                //insertion successful
                DisplayAlert("User Registration", "User: " + newUser.username + " registered successfully.", "Ok");
            }
            else
            {
                //insertion failed, append error reason.
                DisplayAlert("User Registration", "Unable to register user. Reason: " + errorReason, "Ok");
            }
            btnAddUser.IsEnabled = false;
        }


        /// <summary>
        /// Check if username is already present in database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        bool usernameCheck(User newUser)
        {
            List<string> usernames = (from x in conn.Table<User>() select x.username).ToList();

            if (usernames.Count > 0)
            {
                if (usernames.Contains(newUser.username))
                {
                    return true;
                }
                else
                {
                    //username already exists in the database.
                    return false;
                }
            }
            else
            {
                //no users exist in the system so continue with add, no existing user check nessecary
                return false;
            }
        }



        /// <summary>
        /// Navigate to display user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDisplayButtonClicked(object sender, EventArgs e)
        {

            App.Current.MainPage = new Display();
        }

        /// <summary>
        /// Drop existing table and create a new blank table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnDropTableButtonClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Are you sure you want to delete all users?", "Cancel", "Delete");
            switch (action)
            {
                case "Cancel":
                    break;
                case "Delete":
                    conn.DropTable<User>();
                    conn.CreateTable<User>();
                    await DisplayAlert("Information", "User table has been cleared.", "Ok");
                    break;
            }
        }

        /// <summary>
        /// Take photo and set imagesource on button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            if (Plugin.Media.CrossMedia.Current.IsCameraAvailable)
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {Directory = "images", Name = $"{DateTime.UtcNow}.jpg"});

                if (photo != null)
                {
                    PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    byte[] byteArray = GetImageStreamAsBytes(photo.GetStream());
                    newUser.imageBase64 = byteArray;
                    btnAddUser.IsEnabled = true;
                }      
            }
            else
            {
                await DisplayAlert("Device Error", "This device does not have a supported camera.", "Ok");
            } 
        }

        /// <summary>
        /// Convert photo stream into byte array for storage
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Retrieve current GPS values.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        public void OnGpsTestButtonClicked(Object e, EventArgs sender)
        {
            GetLocation();

            if(location == null)
            {
                DisplayAlert("Gps", "No GPS signal found.", "Ok");
            }
            else
            {
                DisplayAlert("Gps", location, "Ok");

            }
           
        }

        /// <summary>
        /// Get current GPS Position
        /// </summary>
        public async void GetLocation()
        {
            Position position = null;

            try
            {
                if (CrossGeolocator.Current.IsGeolocationAvailable)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 100;

                    position = await locator.GetPositionAsync();

                    if (position != null)
                    {
                        position = await locator.GetPositionAsync(TimeSpan.FromSeconds(0.5), null, true);
                        location = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                        position.Timestamp, position.Latitude, position.Longitude,
                        position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);
                        
                            
                        System.Diagnostics.Debug.WriteLine("[GPS_SERVICE > Location]: " + location);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[GPS_SERVICE]: Null Position");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[GPS_SERVICE]: Service not available.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
