using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelExportWithLargeData.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipCountry { get; set; }
        public DateTime ShippedDate { get; set; }
        public static IQueryable<Order> All { get { return _all.Value; } }

        private static Lazy<IQueryable<Order>> _all = new Lazy<IQueryable<Order>>(GetData);
        public static IQueryable<Order> GetData()
        {
            var productNames = new[] { "PlayStation 4", "XBOX ONE", "Wii U", "PlayStation Vita", "PlayStation 3", "XBOX 360", "PlayStation Portable", "3 DS",
                "Dream Cast", "Nintendo 64", "Wii", "PlayStation 2", "PlayStation 1", "XBOX" };
            var countries = new string[] { "US", "Germany", "UK", "Japan", "Italy", "Greece" };
            var list = new List<Order>();
            for (int i = 0, length = 200000; i < length; i++)
            {
                var orderDate = new DateTime(2017, new Random(i).Next(1, 12), new Random(i).Next(1, 28));
                list.Add(new Order
                {
                    Id = i,
                    ProductName = productNames[i % productNames.Length],
                    Price = new Random(i).Next(0, 10000) / 100f,
                    Discount = new Random(i).Next(0, 100) / 100f,
                    OrderDate = orderDate,
                    ShipCountry = countries[i % countries.Length],
                    ShippedDate = orderDate.AddDays(30)
                });
            }

            return list.AsQueryable();
        }
    }
}