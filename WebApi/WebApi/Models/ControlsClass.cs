﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApi.Models
{
    //ControlsClass-A static class to get list of control's categories, groups and controls
    public static class ControlsClass
    {
        private static string PagesFileInServer = new C1FileInfo
        {
            Directories = new List<string> { "Content" },
            Name = "ControlPages.xml"
        }.GetFilePath();

        private static List<ControlCat> _ControlCatList;
        private static IDictionary<string, ControlPage> _pageDic;
        private static List<ControlGroup> _controlGroups;
        private static List<ControlPage> _pages;
        private static List<ControlPageGroup> _pageGroups;
        private static readonly object _locker = new object();

        public static void ResetCatList()
        {
            if (_pages != null)
            {
                return;
            }

            lock (_locker)
            {
                if (_pages != null)
                {
                    return;
                }

                var root = XElement.Load(PagesFileInServer);
                _ControlCatList = new List<ControlCat>();

                _controlGroups = new List<ControlGroup>();
                _pages = new List<ControlPage>();
                _pageGroups = new List<ControlPageGroup>();
                int catId = 0;
                foreach (var controlCat in root.Elements("ControlCat"))
                {
                    catId++;
                    var catName = controlCat.Attribute("name").Value;
                    var catIcon = controlCat.Attribute("Icon").Value;
                    var functionGroups = new List<FunctionGroup>();
                    foreach (var functionGroup in controlCat.Elements("FunctionGroup"))
                    {
                        var functionGroupName = functionGroup.Attribute("name").Value;
                        var functionGroupNameJa = functionGroup.Attribute("name.ja") == null ? null : functionGroup.Attribute("name.ja").Value;
                        var controlGroups = new List<ControlGroup>();
                        foreach (var controlGroup in functionGroup.Elements("ControlGroup"))
                        {
                            var groupId = controlGroup.Attribute("id").Value;
                            var groupName = controlGroup.Attribute("name").Value;
                            var pageGroups = new List<ControlPageGroup>();
                            foreach (var pageElement in controlGroup.Elements("Control"))
                            {
                                var currentControl = pageElement.Attribute("name").Value;
                                var docElement = pageElement.Attribute("documentation");
                                var documentation = docElement == null ? null : docElement.Value;
                                var pages = GetControlPagesFromXEle(pageElement, currentControl);
                                _pages.AddRange(pages);
                                pageGroups.Add(new ControlPageGroup
                                {
                                    ControlName = currentControl,
                                    Documentation = documentation,
                                    Pages = pages
                                });
                                _pageGroups.Add(new ControlPageGroup
                                {
                                    ControlName = currentControl,
                                    Documentation = documentation,
                                    Pages = pages
                                });
                            }
                            _controlGroups.Add(new ControlGroup
                            {
                                GroupId = groupId,
                                GroupName = groupName,
                                controls = pageGroups

                            });
                            controlGroups.Add(new ControlGroup
                            {
                                GroupId = groupId,
                                GroupName = groupName,
                                controls = pageGroups
                            });
                        }
                        var functions = new List<Function>();
                        foreach (var function in functionGroup.Elements("Function"))
                        {
                            var functionName = function.Attribute("name").Value;
                            var functionMethod = function.Attribute("method").Value;
                            var functionUrl = function.Attribute("url").Value;
                            var functionDescription = function.Attribute("description").Value;
                            var functionDescriptionJa = function.Attribute("description.ja") == null ? null : function.Attribute("description.ja").Value;
                            var responseSchema = function.Attribute("responseschema").Value;
                            var functionUsage = function.Attribute("usage").Value;
                            var statuses = new List<FunctionStatus>();
                            foreach (var status in function.Elements("Status"))
                            {
                                var statusName = status.Attribute("name").Value;
                                var statusDescription = status.Attribute("description").Value;
                                statuses.Add(new FunctionStatus
                                {
                                    StatusName = statusName,
                                    StatusDescription = statusDescription
                                });
                            }
                            functions.Add(new Function
                            {
                                FunctionName = functionName,
                                FunctionMethod = functionMethod,
                                FunctionUrl = functionUrl,
                                FunctionDescriptionEn = functionDescription,
                                FunctionDescriptionJp = functionDescriptionJa,
                                ResponseSchema = responseSchema,
                                FunctionUsage = functionUsage,
                                GroupName= functionGroupName,
                                Statuses = statuses
                            });
                        }
                        functionGroups.Add(new FunctionGroup 
                        {
                            FunctionGroupNameEn = functionGroupName,
                            FunctionGroupNameJp = functionGroupNameJa,
                            ControlGroups = controlGroups,
                            Functions = functions
                        });
                    }
                    _ControlCatList.Add(new ControlCat
                    {
                        CatId = catId,
                        CatName = catName,
                        Icon = catIcon,
                        FunctionGroups = functionGroups
                    });
                }
            }
        }

        public static IEnumerable<ControlCat> ControlCatList
        {
            get
            {
                ResetCatList();
                //var Temp = ControlPages.ControlGroups;
                return _ControlCatList;
            }
        }

        private static List<ControlPage> GetControlPagesFromXEle(XElement controlElement, string controlName)
        {
            var pages = controlElement.Elements("action").Select(e =>
            {
                ControlPage page = new ControlPage
                {
                    Text = e.Attribute("text").Value,
                    Name = e.Attribute("name").Value,
                    ControlName = controlName
                };
                if (e.Element("subactions") != null)
                {
                    List<ControlPage> subPages = GetControlPagesFromXEle(e.Element("subactions"), controlName);
                    if (subPages.Count() > 0)
                    {
                        page.SubPages = subPages;
                    }
                }

                return page;
            }).ToList();
            return pages;
        }

        public static string GetPageText(string controllerName, string actionName)
        {
            EnsurePageDic();
            var key = CreatePageKey(controllerName, actionName);
            return _pageDic.ContainsKey(key) ? _pageDic[key].Text : "OverView";
        }

        private static void EnsurePageDic()
        {
            if (_pageDic != null)
            {
                return;
            }

            _pageDic = new Dictionary<string, ControlPage>(StringComparer.OrdinalIgnoreCase);
            foreach (var page in Pages)
            {
                _pageDic.Add(CreatePageKey(page.ControlName, page.Name), page);
            }
        }

        private static string CreatePageKey(string controllerName, string actionName)
        {
            return string.Format("{0}-{1}", controllerName, actionName);
        }

        public static IEnumerable<ControlPage> Pages
        {
            get
            {
                ResetCatList();
                return _pages;
            }
        }

        public static ControlPageGroup GetControlPageGroup(string controlName)
        {
            EnsurePageDic();
            foreach (var pageGroup in _pageGroups)
            {
                if (pageGroup.ControlName.ToLower() == controlName.ToLower())
                    return pageGroup;
            }
            return null;
        }
    }

    public class ControlCat
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Icon { get; set; }
        public IEnumerable<FunctionGroup> FunctionGroups { get; set; }
    }

    public class FunctionGroup
    {
        public IEnumerable<ControlGroup> ControlGroups { get; set; }
        public IEnumerable<Function> Functions { get; set; }

        internal string FunctionGroupNameEn { get; set; }
        internal string FunctionGroupNameJp { get; set; }
        [JsonProperty("FunctionDescription")]
        public string FunctionGroupName
        {
            get
            {
                return PageTemplate.IsJpCulture ? FunctionGroupNameJp ?? FunctionGroupNameEn : FunctionGroupNameEn;
            }
        }
    }

    public class Function
    {
        public string FunctionName { get; set; }
        public string FunctionMethod { get; set; }
        public string FunctionUrl { get; set; }
        internal string FunctionDescriptionEn { get; set; }
        internal string FunctionDescriptionJp { get; set; }

        public string FunctionDescription
        {
            get
            {
                return PageTemplate.IsJpCulture ? FunctionDescriptionJp ?? FunctionDescriptionEn : FunctionDescriptionEn;
            }
        }
        public string FunctionUsage { get; set; }
        public string ResponseSchema { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<FunctionStatus> Statuses { get; set; }
    }

    public class FunctionStatus
    {
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    }

    public class ControlGroup
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<ControlPageGroup> controls { get; set; }
    }

    public class ControlPageGroup
    {
        private const string DocumentationRoot = "https://developer.mescius.com/componentone/docs/webapi/online-webapi/overview.html";
        private string _documentation;

        public string Documentation
        {
            get
            {
                if (string.IsNullOrEmpty(_documentation))
                {
                    return DocumentationRoot;
                }

                return _documentation;
            }
            set { _documentation = value; }
        }

        public IEnumerable<ControlPage> Pages { get; set; }
        public string ControlName { get; set; }
        public int GetSelectedPageIndex(string actionName)
        {
            int sIndex = 0, index = 0;
            foreach (var page in Pages)
            {
                if (page.Name.ToLower() == actionName.ToLower())
                    return index;
                index++;
            }
            return sIndex;
        }
    }
    
    public class ControlPage
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string ControlName { get; set; }
        public List<ControlPage> SubPages { get; set; }
    }
}