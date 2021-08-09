ASP.NET Core Web API Sample.
-------------------------------------------------------------------
The WebAPI sample demonstrates how to make a webapi server to provide all services that ComponentOne Web API products supports.

The sample is a service application which provides all these services:
-Excel: generate/import/export etc.
-Barcode: generate barcode.
-Image: export flexchart/gauge to image.
-Report: generate a report from given report defintion file and datasource, export the report to other formats etc.
-DataEngine: analyze the raw data to show the aggregate result, show detailed row data etc.
-Pdf: load a pdf from given path, export the pdf to other formats etc.

<product>Data Engine Services for ASP.NET Web API;ASP.NET</product>
<product>PDF Services for ASP.NET Web API;ASP.NET</product>
<product>Report Services for ASP.NET Web API;ASP.NET</product>
<product>Excel Services for ASP.NET Web API;ASP.NET</product>
<product>Image Services for ASP.NET Web API;ASP.NET</product>
<product>Barcode Services for ASP.NET Web API;ASP.NET</product>

For cloud service storage, this sample demo for DropBox only. Other services (Azure, AWS, GoogleDrive, OneDrive) will be shown in CloudFileExplorer sampler. 
For DropBox, please note that :

DropBox : DropBox/C1WebApi/test.xlsx

step 1: login to your DropBox acount.

step 2: create an App and generate Access Token for this app.

step 3: use Access Token  created at step 2 to put into DropBoxStorageAccessToken key value in the Web.config.

step 4: open WebApiExplorer project and navigate to Storage api section.

step 5: change "C1WebApi" to your apps name.

step 6: change "test.xlsx" to your file name you want to post.
NOTE: if using List Api, need change "test1" to your sub folder path. 

Note for FlexReport using SQLite database:
Currently, SQLite has this issue: https://stackoverflow.com/questions/13028069/unable-to-load-dll-sqlite-interop-dll/60176344.
Then SQLite 1.0.113 x86 package is requied for the sample.
