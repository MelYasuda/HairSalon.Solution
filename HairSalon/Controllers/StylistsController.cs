using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
      [HttpGet("/stylists")]
      public ActionResult Index()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpGet("/stylists/new")]
      public ActionResult CreateForm()
      {
        return View();
      }

      [HttpPost("/stylists")]
      public ActionResult Create(string name)
      {
        Stylist newStylist = new Stylist(name);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View("Index", allStylists);
      }

      [HttpGet("/stylists/{id}")]
      public ActionResult Details(int id)
      {
        Stylist foundStylist = Stylist.Find(id);
        List<Client> stylistClients = foundStylist.GetClients();
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("stylist", foundStylist);
        model.Add("clients", stylistClients);
        return View(model);
      }

      [HttpPost("/clients")]
      public ActionResult Create(int stylistId, string clientName)
      {
        Client newClient = new Client(clientName, stylistId);
        newClient.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist foundStylist = Stylist.Find(stylistId);
        List<Client> stylistClients = foundStylist.GetClients();
        model.Add("clients", stylistClients);
        model.Add("stylist", foundStylist);
        return View("Details", model);
      }

      [HttpGet("/stylists/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
          Stylist foundStylist = Stylist.Find(id);
          return View(foundStylist);
        }

      [HttpPost("/stylists/{id}/update")]
        public ActionResult Update(int id, string editName)
        {
          Stylist foundStylist = Stylist.Find(id);
          foundStylist.Edit(editName);
          return RedirectToAction("Index");
        }
    }
}
