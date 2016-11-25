using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace WebNetTools.Controllers
{
    public class ToolsController : Controller
    {
        public class PingResult
        {
            public string IP;
            public string TTL;
            public string Status;
        }

        public IActionResult Ping()
        {
            return View();
        }

        public IActionResult DoPing(string host)
        {
            try
            {
                var ping = new Ping();
                List<PingReply> attempts = new List<PingReply>();
                for (int i = 0; i < 4; i++)
                    attempts.Add(ping.Send(host));

                return Json(new { result = attempts.Select(a=>new PingResult
                    {
                        IP = a.Address?.ToString(),
                        TTL = a.Options?.Ttl.ToString(),
                        Status = a.Status.ToString()
                    }).ToArray() });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
