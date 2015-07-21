using PugTrace.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace PugTrace.Example.Controllers
{
    public class HomeController : Controller
    {
        private static TraceSource Source { get; set; }

        public HomeController()
        {
            Source = new TraceSource("Application");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Log()
        {
            Source.TraceInformation("Hello world!");
            Source.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult LogException()
        {
            try
            {
                int divider = 0;
                int result = 5 / divider;
            }
            catch (Exception ex)
            {
                var data = new List<object>();
                data.Add(new RequestData(HttpContext));
                data.Add(new ExceptionData(ex));
                Source.TraceData(TraceEventType.Error, 5000, data.ToArray());
                Source.Flush();
            }
            return RedirectToAction("Index");
        }
    }
}