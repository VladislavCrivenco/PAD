using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PAD.Models;
using PAD.Repository;

namespace PAD.Controllers
{
    public class HomeController : Controller
    {

private IRepositoryBase repositoryBase;
        public HomeController(IRepositoryBase repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public IActionResult AddEmployee()
        {
            var model = new Employee();
            return View(model);
        }

        public IActionResult Edit(string id)
        {
            var model = repositoryBase.GetEmployee(id);
            return View(model);
        }
        public IActionResult Index()
        {
            var models = repositoryBase.GetEmployees();
            return View(models);
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

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
