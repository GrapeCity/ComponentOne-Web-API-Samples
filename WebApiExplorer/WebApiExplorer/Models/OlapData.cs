using C1.Web.Mvc.Olap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiExplorer.Models
{
    public class OlapData
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static IList<OlapData> GetDataSets()
        {
            var results = new List<OlapData>();
            results.Add(new OlapData
            {
                Name = "DataEngine(100,000 items)",
                Value = "complex10"
            });
            results.Add(new OlapData
            {
                Name = "DataEngine(500,000 items)",
                Value = "complex50"
            });
            results.Add(new OlapData
            {
                Name = "DataEngine(1,000,000 items)",
                Value = "complex100"
            });
            results.Add(new OlapData
            {
                Name = "DataSet(100,000 items)",
                Value = "dataset10"
            });
            results.Add(new OlapData
            {
                Name = "DataSet(500,000 items)",
                Value = "dataset50"
            });
            results.Add(new OlapData
            {
                Name = "DataSet(1,000,000 items)",
                Value = "dataset100"
            });
            results.Add(new OlapData
            {
                Name = "SSAS(Adventure Works)",
                Value = "cube"
            });
            return results;
        }

        public static IEnumerable<string> GetShowTotals()
        {
            return Enum.GetValues(typeof(ShowTotals)).OfType<ShowTotals>().Select(item => item.ToString());
        }
    }
}