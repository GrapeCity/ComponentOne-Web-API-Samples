using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiExplorer.Controllers.Visitor
{
  public partial class VisitorController : Controller
  {
    Dictionary<string, string> VariablesToDisplay = new Dictionary<string, string>()
    {
      { "visitorId", Localization.Visitor.Txt_Title_VisitorId},
      { "ip", Localization.Visitor.Txt_Title_Ip},
      { "locale", Localization.Visitor.Txt_Title_Locale},
      { "browser", Localization.Visitor.Txt_Title_Browser},
      { "os", Localization.Visitor.Txt_Title_Os},
      { "device", Localization.Visitor.Txt_Title_Device},
      { "geo", Localization.Visitor.Txt_Title_Geo},
      { "session", Localization.Visitor.Txt_Title_Session},
      { "firstSession", Localization.Visitor.Txt_Title_First_Session}
    };

    public IActionResult AvailableVariables()
    {
      ViewBag.VariablesToDisplay = this.VariablesToDisplay;
      return View();
    }
  }
}