using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult HomeAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ChartData()
        {
            var keys = new string[] { "Rất không hài lòng", "Chưa hài lòng", "Bình thường", "Hài lòng", "Rất hài lòng" };
            var httpClient = new HttpClient();
            var chartData = await httpClient.GetStringAsync("https://thongkevludoctor.herokuapp.com/api/statistic");
            var data = JsonConvert.DeserializeObject<Dictionary<int, int>>(chartData).ToDictionary(e => keys[e.Key - 1], e => e.Value);
            foreach (var key in keys)
            {
                if (!data.ContainsKey(key))
                    data.Add(key, 0);
            }
            return Json(new { dbchart = data.Select(e => new { e.Key, Count = e.Value }), code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}