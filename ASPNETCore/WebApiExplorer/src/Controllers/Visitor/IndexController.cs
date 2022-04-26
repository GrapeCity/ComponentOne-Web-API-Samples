using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiExplorer.Controllers.Visitor
{
  public partial class VisitorController : Controller
  {
    Dictionary<string, string> Resources = new Dictionary<string, string>()
    {
      { "time_just_now", Localization.Visitor.Time_Just_Now},
      { "time_minutes_ago", Localization.Visitor.Time_Minutes_Ago},
      { "time_hours_ago", Localization.Visitor.Time_Hours_Ago},
      { "time_days_ago", Localization.Visitor.Time_Days_Ago},
      { "time_months_ago", Localization.Visitor.Time_Months_Ago},
      { "time_years_ago", Localization.Visitor.Time_Years_Ago},
      { "txt_much_more", Localization.Visitor.Txt_Much_More},
      { "welcome_message", Localization.Visitor.Welcome_Message},
      { "browsing_time", Localization.Visitor.Template_Browsing_Time},
      { "device_info", Localization.Visitor.Template_Device_Info},
      { "ip_address", Localization.Visitor.Template_Ip_Address},
      { "landed_time", Localization.Visitor.Template_Landed_Time},
      { "last_visit", Localization.Visitor.Template_Last_Visit},
      { "location_info", Localization.Visitor.Template_Location_Info},
      { "visitor_shown", Localization.Visitor.Template_Visitor_Shown},
      { "visits_time", Localization.Visitor.Template_Visits_Time},
      { "no_location_found", Localization.Visitor.No_Location_Found},
    };
 
    public IActionResult Index()
    {
      ViewBag.Resources = this.Resources;
      return View();
    }
  }
}