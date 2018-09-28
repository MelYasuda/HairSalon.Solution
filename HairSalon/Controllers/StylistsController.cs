using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

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

        List<Speciality> stylistSpecialities = foundStylist.GetSpecialities();
        List<Speciality> allSpecialities = Speciality.GetAll();
        model.Add("stylistSpecialities", stylistSpecialities);
        model.Add("allSpecialities", allSpecialities);

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
        return RedirectToAction("Details", new { id = stylistId });
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

        [HttpGet("/clients/delete/{clientId}")]
        public ActionResult DeleteClient(int clientId)
        {
          Client foundClient = Client.Find(clientId);
          foundClient.Delete();
          return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{stylistId}/specialities/new")]
        public ActionResult AddSpecialities(int stylistId)
        {
          Stylist stylist = Stylist.Find(stylistId);
          Speciality specialities = Speciality.Find(Int32.Parse(Request.Form["speciality-id"]));
          stylist.AddSpecialities(specialities);
          return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{stylistId}/delete")]
        public ActionResult DeleteStylist(int stylistId)
        {
          Stylist foundStylist = Stylist.Find(stylistId);
          foundStylist.Delete();
          return RedirectToAction("Index");
        }

    }
}
