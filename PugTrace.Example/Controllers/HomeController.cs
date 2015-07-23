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
            catch (Exception exception)
            {
                Source.TraceWebException(5000, HttpContext, exception);
                Source.Flush();
            }
            return RedirectToAction("Index");
        }

        public ActionResult LogLong()
        {
            Source.TraceInformation("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin imperdiet, risus non lobortis egestas, nibh sapien fringilla dui, sed dictum diam turpis et nisl. Sed elit lacus, tincidunt at tincidunt sed, mollis et eros. Vivamus ac diam lorem. Curabitur placerat interdum augue. Proin sit amet risus eu magna aliquam volutpat non in turpis. Phasellus porta eu lorem vitae condimentum. Suspendisse efficitur bibendum justo non fringilla. Sed et ornare ipsum. Nullam at turpis sed eros pellentesque consectetur. Duis ullamcorper porta dignissim.");
            Source.Flush();
            return RedirectToAction("Index");
        }
    }
}