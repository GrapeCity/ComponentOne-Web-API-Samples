AspNetCore WebApiCore Sample.
-------------------------------------------------------------------
The WebApiCore sample demonstrates how to make a webapi server to provide all services that ComponentOne WebApi products supports.

The sample is a service application which provides all these services:
- Excel: generate/import/export etc.
- Barcode: generate barcode.
- DataEngine: analyze the raw data to show the aggregate result, show detailed row data etc.
- Pdf: load a pdf from given path, export the pdf to other formats etc.
- Report: generate a report from given report definition file and datasource, export the report to other formats etc.
- Visitor: Gathering visitor information like IP address, geolocation, language,...

In the sample, we used visitor api which require a local database included ip2location database.
Please add a new database to your local sql server and provide a valid connection string inside appsettings.json file.
You can find the database and setup guide from https://lite.ip2location.com/database/ip-country-region-city-latitude-longitude-zipcode-timezone
- database in use: db11
- server type: sql
- ip version: ipv4

For cloud service storage, this sample demo for DropBox only. Other services (Azure, AWS, GoogleDrive, OneDrive) will be shown in CloudFileExplorer sample. 
For DropBox, please note that :

DropBox : DropBox/C1WebApi/test.xlsx

step 1: login to your DropBox account.

step 2: create an App and generate Access Token for this app.

step 3: use Access Token  created at step 2 to put into DropBoxStorageAccessToken key value in the Web.config.

step 4: open WebApiExplorer project and navigate to Storage api section.

step 5: change "C1WebApi" to your apps name.

step 6: change "test.xlsx" to your file name you want to post.
NOTE: if using List Api, need change "test1" to your sub folder path. 


