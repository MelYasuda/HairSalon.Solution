using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class specialitiesController : Controller
  {
    [HttpGet("specialities")]
    public ActionResult Index()
    {
      List<Speciality> allSpecialities = Speciality.GetAll();
      return View(allSpecialities);
    }

    [HttpGet("/specialities/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/specialities")]
    public ActionResult Create(string description)
    {
      Speciality newSpeciality = new Speciality(description);
      newSpeciality.Save();
      List<Speciality> allSpecialities = Speciality.GetAll();
      return View("Index", allSpecialities);
    }

    [HttpGet("/specialities/{specialityId}")]
    public ActionResult Details(int specialityId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Speciality selectedSpeciality = Speciality.Find(specialityId);
      List<Stylist> specialityStylists = selectedSpeciality.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("selectedSpeciality", selectedSpeciality);
      model.Add("specialityStylists", specialityStylists);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpPost("/specialities/{specialityId}/stylists/new")]
    public ActionResult AddStylists(int specialityId)
    {
      Speciality speciality = Speciality.Find(specialityId);
      Stylist stylists = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
      speciality.AddStylists(stylists);
      return RedirectToAction("Index");
    }
  }
}
