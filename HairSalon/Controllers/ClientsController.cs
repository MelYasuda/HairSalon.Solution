using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
      [HttpGet("/stylists/{id}/new")]
      public ActionResult CreateForm(int id)
      {
        Stylist foundStylist = Stylist.Find(id);
        return View(foundStylist);
      }



    }
}
