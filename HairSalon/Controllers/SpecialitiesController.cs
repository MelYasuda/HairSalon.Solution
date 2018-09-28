using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

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
  }
}
