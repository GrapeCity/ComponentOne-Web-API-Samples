using Microsoft.AspNetCore.Mvc;
using C1.Web.Api.Excel;

namespace WebApi.Controllers
{

  /// <summary>
  /// Define a new controller which extends from <see cref="ExcelController"/>.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class C1ExcelController : ExcelController
  {

  }
}