using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_TKBookCalendarController : Controller
    {
        public ActionResult BookCalendar()
        {
            return View();
        }

        // GET: Admin/TKBookCalendar
        public async Task<JsonResult> ChartData()
        {
            var httpClient = new HttpClient();
            var chartData = await httpClient.GetStringAsync("https://bacsivanlang-statistic.herokuapp.com/api/statistic?startDate=01%2F2022&endDate=12%2F2022");
            var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(chartData);
            return Json(new { dbchart = data.Select(e => new { e.Key, Count = e.Value }), code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}