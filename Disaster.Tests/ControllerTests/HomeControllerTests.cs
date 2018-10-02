using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Disaster.Controllers;
using Disaster.Models;

namespace Disaster.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
