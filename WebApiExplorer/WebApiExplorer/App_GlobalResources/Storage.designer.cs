//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Storage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Storage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Storage", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete.
        /// </summary>
        internal static string StorageUploadDELETE_DELETE {
            get {
                return ResourceManager.GetString("StorageUploadDELETE_DELETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to List.
        /// </summary>
        internal static string StorageUploadLIST_LIST {
            get {
                return ResourceManager.GetString("StorageUploadLIST_LIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Description.
        /// </summary>
        internal static string StorageUploadPOST_Description {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to file.
        /// </summary>
        internal static string StorageUploadPOST_File {
            get {
                return ResourceManager.GetString("StorageUploadPOST_File", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The upload file. (for demo, please choose a .xlsx file).
        /// </summary>
        internal static string StorageUploadPOST_FileText {
            get {
                return ResourceManager.GetString("StorageUploadPOST_FileText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter.
        /// </summary>
        internal static string StorageUploadPOST_Parameter {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Parameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameters.
        /// </summary>
        internal static string StorageUploadPOST_Parameters {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Parameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to path.
        /// </summary>
        internal static string StorageUploadPOST_Path {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Path", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file path in storage..
        /// </summary>
        internal static string StorageUploadPOST_PathText {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - AWS: AWS/test.xlsx&lt;/br&gt;
        ///
        ///step 1: you need prepare value for following 3 keys in the Web.config.&lt;/br&gt;
        ///
        ///AccessTocken&lt;/br&gt;
        ///SecretKey&lt;/br&gt;
        ///BucketName&lt;/br&gt;
        ///step 2: open WebApiExplorer project and navigate to Storage api section.&lt;/br&gt;
        ///
        ///step 3: change &quot;test.xlsx&quot; to your file name you want to post.&lt;/br&gt;
        ///NOTE: if using List Api, need change &quot;test1&quot; to your sub folder path..
        /// </summary>
        internal static string StorageUploadPOST_PathTextAWS {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathTextAWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Azure : The demo uses the file path as &quot;Azure/demostoragepdfcontainer/test.xlsx&quot;&lt;/br&gt;
        ///
        ///You need to use your account to update the Azure connection string in the sample &lt;/br&gt;by following these steps:&lt;/br&gt;
        ///
        ///step 1: login to your Azure account.&lt;/br&gt;
        ///
        ///step 2: create a container and generate connection string for this container.&lt;/br&gt;
        ///
        ///step 3: use the connection string created in step 2 to put into &lt;/br&gt;AzureStorageConnectionString key value in the Web.config.&lt;/br&gt;
        ///
        ///step 4: open WebApiExplorer project [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StorageUploadPOST_PathTextAzure {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathTextAzure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DropBox : DropBox/C1WebApi/test.xlsx&lt;/br&gt;
        ///
        ///step 1: login to your DropBox acount.&lt;/br&gt;
        ///
        ///step 2: create an App and generate Access Token for this app.&lt;/br&gt;
        ///
        ///step 3: use Access Token  created at step 2 to put into DropBoxStorageAccessToken&lt;/br&gt; key value in the Web.config.&lt;/br&gt;
        ///
        ///step 4: open WebApiExplorer project and navigate to Storage api section.&lt;/br&gt;
        ///
        ///step 5: change &quot;C1WebApi&quot; to your apps name.&lt;/br&gt;
        ///
        ///step 6: change &quot;test.xlsx&quot; to your file name you want to post.&lt;/br&gt;
        ///NOTE: if using List Api [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StorageUploadPOST_PathTextDropBox {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathTextDropBox", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GoogleDrive: GoogleDrive/WebAPI/test.xlsx&lt;/br&gt;
        ///
        ///step 1: login to your GoogleDrive acount.&lt;/br&gt;
        ///
        ///step 2: create an App and generate credentials.json file for this app.&lt;/br&gt;
        ///
        ///step 3: usecredentials.json  created at step 2 to put into WebApi folder.&lt;/br&gt;
        ///
        ///step 4: open WebApiExplorer project and navigate to Storage api section.&lt;/br&gt;
        ///
        ///step 5: change &quot;WebAPI&quot; to your apps name.&lt;/br&gt;
        ///
        ///step 6: change &quot;test.xlsx&quot; to your file name you want to post.&lt;/br&gt;
        ///NOTE: if using List Api, need change &quot;test1&quot; to yo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StorageUploadPOST_PathTextGoogleDrive {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathTextGoogleDrive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OneDrive : OneDrive/C1WebApi/test.xlsx&lt;/br&gt;
        ///
        ///step 1: login to your OneDrive acount.&lt;/br&gt;
        ///
        ///step 2: browse this link for access token&lt;/br&gt; &quot;https://login.live.com/oauth20_authorize.srf?client_id=000000004C16A865&amp;scope=onedrive.readwrite&amp;response_type=token&quot;.&lt;/br&gt;
        ///
        ///step 3: use Access Token  created at step 2 to put into OneDriveAccessToken&lt;/br&gt; key value in the Web.config.&lt;/br&gt;
        ///
        ///step 4: open WebApiExplorer project and navigate to Storage api section.&lt;/br&gt;
        ///
        ///step 5: change &quot;C1WebApi&quot; to your apps name. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StorageUploadPOST_PathTextOneDrive {
            get {
                return ResourceManager.GetString("StorageUploadPOST_PathTextOneDrive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request Url.
        /// </summary>
        internal static string StorageUploadPOST_RequestUrl {
            get {
                return ResourceManager.GetString("StorageUploadPOST_RequestUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Response Data.
        /// </summary>
        internal static string StorageUploadPOST_ResponseData {
            get {
                return ResourceManager.GetString("StorageUploadPOST_ResponseData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upload.
        /// </summary>
        internal static string StorageUploadPOST_Upload {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Upload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value.
        /// </summary>
        internal static string StorageUploadPOST_Value {
            get {
                return ResourceManager.GetString("StorageUploadPOST_Value", resourceCulture);
            }
        }
    }
}
