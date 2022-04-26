using Owin;
using Microsoft.Owin;
using System.Data.SqlClient;
using System.Configuration;

[assembly: OwinStartup(typeof(SalesAnalysis.Startup))]

namespace SalesAnalysis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                app.UseDataEngineProviders()
                    .AddDataEngine("Orders",
                        new SqlCommand(
                                    "SELECT Orders.Amount, Orders.Province, Orders.Region, Orders.ShipMode, "
                                    + "Orders.ShippingCost, Orders.OrderPriority, Orders.OrderQuantity,"
                                    + "Orders.Discount, Customers.Name as CustomerName, Employees.Name as EmployeeName "
                                    + "From Orders Left join Customers on Orders.CustomerId = Customers.Id "
                                    + "Left join Employees on Orders.EmployeeId = Employees.Id"), 
                        connection)
                    .AddDataEngine("Invoices",
                        new SqlCommand("SELECT Invoices.Amount, Customers.Name as CustomerName, Employees.Name as EmployeeName "
                                    + "From Invoices "
                                    + "Left join Customers on Invoices.CustomerId = Customers.Id "
                                    + "Left join Employees on Invoices.EmployeeId = Employees.Id"),
                        connection)
                    .AddDataEngine("Products",
                        new SqlCommand("select Name,Price,Category,SubCategory,QuantityInStock,QuantityInOrder from Products"),
                        connection)
                    .AddDataEngine("Expenses",
                        new SqlCommand(
                                    "SELECT Expenses.Amount, Expenses.Type, Expenses.Status, Employees.Name as EmployeeName "
                                    + "From Expenses Left join Employees on Expenses.EmployeeId = Employees.Id"),
                        connection);
            }
        }
    }
}