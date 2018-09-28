using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
      [HttpGet("/clients")]
      public ActionResult Index()
      {
        List<Client> allClients = Client.GetAll();
        return View(allClients);
      }

      [HttpGet("/stylists/{id}/new")]
      public ActionResult CreateForm(int id)
      {
        Stylist foundStylist = Stylist.Find(id);
        return View(foundStylist);
      }

      [HttpGet("/clients/delete")]
      public ActionResult DeleteAllClient()
      {
        Client.DeleteAll();
        return RedirectToAction("Index");
      }
    }
}
