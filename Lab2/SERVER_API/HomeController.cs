using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PAD.Models;
using PAD.Repository;
using PAD;

namespace PAD
{
    public class HomeController : Controller
    {
        public IRepositoryBase _repository;

        public HomeController(IRepositoryBase repository)
        {
            _repository = repository;
        }

        [Route("api/all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repository.GetAuthors();
            return Json(new {data = list});
        }

    }
}