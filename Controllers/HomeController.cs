using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalrSqlDependency1.Models;
using SignalrSqlDependency1.Repository;
using Newtonsoft.Json;

namespace SignalrSqlDependency1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILenhDatRepository _repository;

        public HomeController(ILenhDatRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetLenhDats(){
            return Ok(_repository.GetAllLenhDats());
        }

        [HttpPost]
        public IActionResult PostLenhDat([FromBody]LenhDat lenhDatData)
        {
            string expectResult = "Thêm thành công";
            string result = _repository.AddLenhDat(lenhDatData);
            if (result.Equals(expectResult)){
                return Ok(expectResult);
            }
            else{
                return BadRequest(result);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
