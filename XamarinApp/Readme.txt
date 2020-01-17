Xamarin implementation of GRS Mobile app:

-IOS (Unable to test due to mac remote host block)
-UWP (Tested, Working)
-ANDROID (Tested,Working)

Local Database Solution: SQLite

//Version 1.1.0
-Added Camera Functionality
Camera takes a photo using the native device's camera, converts the image into a byte array and stores that array in the local database.

//Version 1.2.0
-Added GPS Functionality
Each new user created will store a GPS value of the location they were created.

//Version 1.3.0
-Added gateway class and a new dbconfig entry.
Application can be swapped from gateway to object operation by changing the 'databaseMode' string:
GATEWAY = gateway operation mode
OBJECT = sqlite-pcl database operation mode.
