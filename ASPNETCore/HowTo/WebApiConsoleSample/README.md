## Net Core Console Sample
#### [Download as zip](https://downgit.github.io/#/home?url=https://github.com/GrapeCity/ComponentOne-Web-API-Samples/tree/master/ASPNETCore/HowTo/WebApiConsoleSample)
____
#### Net Core application that demonstrate how to use ComponentOne WebApi Core services.
____
This sample including:


* WebApi Excel Service
* Import/Export Excel To FlexGrid and FlexGrid to Excel
* Generate Excel
* Split & Merge
* Find & Replace
* Create/Delete rows/columns


* WebApi Barcode Service
* Generate Barcode


* WebApi DataEngine Service
* Analyze Data and get result.

# How to config service url

C1WebApi Service Url can be configured in assets/config.json file.

## How to run the sample.

This sample was made as console application, so you can use command line or add these code as library in your application. The steps below is to run the sample as console application with dotnet cli.

Steps:
    1. dotnet restore
    2. dotnet run.

# How to configure test

The sample made with 3 main apis of C1WebApi including Excel, BarCode and DataEngine. 
You can choose which API to test by comment/uncomment at Main function. Each Controller has built-in function that demonstrate how to use C1WebApiService. Please comment/uncomment function at Run() method to play around.




            


