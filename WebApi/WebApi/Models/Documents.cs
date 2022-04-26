using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WebApi.Models
{
    public class Documents
    {
        internal static string PATH_SLASH = "/";

        private static Documents _ssrsReports = new Documents(new C1FileInfo
        {
            Directories = new List<string> { "Content" },
            Name = "SsrsReportInfos.xml"
        }, "c1ssrs", DocumentItem.DocumentKindSsrsReport);
        private static Documents _pdfs = new Documents(new C1FileInfo
        {
            Directories = new List<string> { "Content" },
            Name = "PdfInfos.xml"
        }, "PdfsRoot", DocumentItem.DocumentKindPdf);
        private static List<DocumentItem> _allItems;

        private readonly string _xmlFilePath;
        private readonly string _rootKey;
        private readonly string _itemKind;
        private List<DocumentItem> _items;
        private DocumentItem _selectedItem;

        private readonly object _locker = new object();

        public Documents(C1FileInfo xmlFilePath, string rootKey, string itemKind)
        {
            _xmlFilePath = xmlFilePath.GetFilePath();
            _rootKey = rootKey;
            _itemKind = itemKind;
        }

        public static Documents SsrsReports
        {
            get { return _ssrsReports; }
        }

        public static Documents Pdfs
        {
            get { return _pdfs; }
        }

        public static IEnumerable<DocumentItem> AllItems
        {
            get
            {
                if (_allItems == null)
                {
                    var pdfs = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "PdfFiles", TitleEn = "PDF Files" };
                    pdfs.Children.AddRange(_pdfs.Items);
                    var ssrsReports = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "SsrsReports", TitleEn = "SSRS Reports" };
                    ssrsReports.Children.AddRange(_ssrsReports.Items);
                    var flexReports = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "FlexReports", TitleEn = "FlexReports" };
                    flexReports.Children.AddRange(ReportFiles.Reports);

                    _allItems = new List<DocumentItem> { flexReports, ssrsReports, pdfs };
                }

                return _allItems;
            }
        }

        public IEnumerable<DocumentItem> Items
        {
            get
            {
                EnsureInit();
                return _items;
            }
        }

        public string SelectedDocumentPath
        {
            get
            {
                EnsureInit();
                return _selectedItem.FullPath;
            }
        }

        public string SelectedDocumentName
        {
            get
            {
                EnsureInit();
                return _selectedItem.Name;
            }
        }

        public string SelectedDocumentTitle
        {
            get
            {
                EnsureInit();
                return _selectedItem.Title;
            }
        }

        private void EnsureInit()
        {
            if (_items == null)
            {
                lock (_locker)
                {
                    if (_items == null)
                    {
                        InitItems();
                    }
                }
            }
        }

        private void InitItems()
        {
            var items = new List<DocumentItem>();
            var docInfos = XElement.Load(_xmlFilePath);
            var selectedCategoryName = docInfos.Element("SelectedDocument").Element("CategoryName").Value;
            var selectedDocumentName = docInfos.Element("SelectedDocument").Element("DocumentName").Value;

            foreach (var category in docInfos.Elements("Category"))
            {
                var categoryName = category.Attribute("Name").Value;
                var categoryTitle = category.Attribute("Title").Value;
                var categoryTitleJa = category.Attribute("Title.ja").Value;

                var folder = new DocumentItem() { Name = categoryName, TitleEn = categoryTitle, TitleJp = categoryTitleJa, Kind = DocumentItem.DocumentKindFolder };
                items.Add(folder);

                foreach (var doc in category.Elements("Document"))
                {
                    var docName = doc.Element("DocumentName").Value;
                    var docPath = doc.Element("DocumentPath").Value;
                    var docTitle = doc.Element("DocumentTitle").Value;
                    var docTitleJa = doc.Element("DocumentTitle.ja") == null ? null : doc.Element("DocumentTitle.ja").Value;

                    var item = new DocumentItem() { Kind = _itemKind, Name = docName, TitleEn = docTitle, TitleJp = docTitleJa, FullPath = _rootKey + PATH_SLASH + docPath };
                    folder.Children.Add(item);

                    if (selectedCategoryName == categoryName && selectedDocumentName == docName)
                    {
                        _selectedItem = item;
                    }
                }
            }

            _items = items.Count == 1 ? items[0].Children : items;
        }
    }

    public class DocumentItem
    {
        private const string SLASH = "/";

        internal const string DocumentKindFolder = "Folder";
        internal const string DocumentKindPdf = "Pdf";
        internal const string DocumentKindFlexReport = "FlexReport";
        internal const string DocumentKindSsrsReport = "SsrsReport";

        private List<DocumentItem> _children;
        private string _fullPath;

        public string Kind { get; set; }
        internal string TitleEn { get; set; }
        internal string TitleJp { get; set; }
        public string Title
        {
            get
            {
                return PageTemplate.IsJpCulture ? TitleJp ?? TitleEn : TitleEn;
            }
        }
        public string Name { get; set; }
        public string FullPath
        {
            get
            {
                return string.IsNullOrEmpty(_fullPath) ? Name : _fullPath;
            }
            set
            {
                _fullPath = value;
            }
        }
        public List<DocumentItem> Children
        {
            get
            {
                return _children ?? (_children = new List<DocumentItem>());
            }
            set { _children = value; }
        }
    }

    public static class ReportFiles
    {
        private const string REPORTSROOT = "ReportsRoot";
        private const string SLASH = "/";

        private static List<DocumentItem> _reportFiles;
        private static DocumentItem _selectedItem;
        private static readonly object _locker = new object();

        private static void EnsureInit()
        {
            lock (_locker)
            {
                if (_reportFiles != null)
                {
                    return;
                }
                _reportFiles = new List<DocumentItem>();
                var reportInfos = XElement.Load(new C1FileInfo
                {
                    Directories = new List<string>
                    {
                        "Content"
                    },
                    Name = "ReportInfos.xml"
                }.GetFilePath());
                var selectedReportCategoryName = reportInfos.Element("SelectedReport").Element("CategoryName").Value;
                var selectedReportName = reportInfos.Element("SelectedReport").Element("ReportName").Value;

                foreach (var category in reportInfos.Elements("Category"))
                {
                    var categoryName = category.Attribute("Name").Value;
                    var categoryText = category.Attribute("Text").Value;
                    var categoryTextJa = category.Attribute("Text.ja").Value;
                    var categoryImage = category.Attribute("Image").Value;

                    var folder = new DocumentItem() { Kind = DocumentItem.DocumentKindFolder, Name = categoryName, TitleEn = categoryText, TitleJp = categoryTextJa };

                    foreach (var report in category.Elements("Report"))
                    {
                        var reportName = report.Element("ReportName").Value;
                        var fileName = report.Element("FileName").Value;
                        var reportTitle = report.Element("ReportTitle").Value;
                        var reportTitleJa = report.Element("ReportTitle.ja") == null ? null : report.Element("ReportTitle.ja").Value;
                        var imageName = report.Element("ImageName").Value;

                        var file = new DocumentItem() { Kind = DocumentItem.DocumentKindFlexReport, Name = reportName, TitleEn = reportTitle, TitleJp = reportTitleJa, FullPath = REPORTSROOT + Documents.PATH_SLASH + categoryName + Documents.PATH_SLASH + fileName };
                        folder.Children.Add(file);

                        if (selectedReportCategoryName == categoryName && selectedReportName == reportName)
                        {
                            _selectedItem = file;
                        }
                    }
                    folder.Children.Sort(new Comparison<DocumentItem>((r1, r2) => r1.Title.CompareTo(r2.Title)));
                    _reportFiles.Add(folder);
                }
                _reportFiles.Sort(new Comparison<DocumentItem>((r1, r2) => r1.Title.CompareTo(r2.Title)));
            }
        }

        public static IEnumerable<DocumentItem> Reports
        {
            get
            {
                EnsureInit();
                return _reportFiles;
            }
        }

        public static string SelectedReportPath
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.FullPath;
            }
        }

        public static string SelectedReportName
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.Name;
            }
        }

        public static string SelectedReportTitle
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.Title;
            }
        }
    }
}