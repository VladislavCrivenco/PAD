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
    public class EmployeeController : ControllerBase
    {

        private IRepositoryBase repositoryBase;
        public EmployeeController(IRepositoryBase repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        [HttpDelete]
[Route("api/[controller]/Delete")]
        public IActionResult Delete(string Id){

            repositoryBase.DeleteEmployee(Id);
            return new  RedirectToActionResult("index", "home", new {});
        }

        [HttpPost]
        [Route("api/[controller]/Add")]
        public IActionResult Add(Employee employee){
            repositoryBase.AddEmployee(employee);
            return new  RedirectToActionResult("index", "home", new {});
        }

        [HttpPost]
        [Route("api/[controller]/put")]
        public IActionResult Put(Employee employee){
            repositoryBase.EditEmployee(employee);
            return new  RedirectToActionResult("index", "home", new {});
        }
    }
}
