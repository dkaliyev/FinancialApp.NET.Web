using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Filters;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;

namespace FinancialThing.Controllers
{
    public class RatiosBuilderController : Controller
    {
        private IRepository<Dictionary, Guid> _dictionaryServiceRepository;

        private IRepository<Ratio, Guid> _ratioServiceRepository;

        private IRepository<RatioValue, Guid> _ratioValueRepo;


        public RatiosBuilderController(IRepository<Dictionary, Guid> dictionaryServiceRepository, IRepository<Ratio, Guid> ratioServiceRepository,
            IRepository<RatioValue, Guid> ratioValueRepo)
        {
            _dictionaryServiceRepository = dictionaryServiceRepository;
            _ratioServiceRepository = ratioServiceRepository;
            _ratioValueRepo = ratioValueRepo;
        }
        //
        // GET: /Ratios/
        public async Task<ActionResult> Index()
        {
            var pages = new List<string>() { "FI", "BalanceSh", "CashFlow", "IncomeStatement" };
            var dics = await _dictionaryServiceRepository.GetQuery();
            var dictionaries = dics.Where(d => !pages.Contains(d.ParentCode)).AsEnumerable();
            var ratios = await _ratioServiceRepository.GetQuery();

            var vm = new RatioViewModel()
            {
                Ratios = ratios,
                Dictionary = dictionaries
            };

            return View(vm);
        }

        [AllowJsonGet]
        public async Task<JsonResult> GetRatios()
        {
            var ratios = await _ratioServiceRepository.GetQuery();
            return new JsonResult() { Data = new { _ratios = ratios } };
        }

        public async Task<JsonResult> GetDictionary()
        {
            var pages = new List<string>() { "FI", "BalanceSh", "CashFlow", "IncomeStatement" };
            var dics = await _dictionaryServiceRepository.GetQuery();
            var dictionaries = dics.Where(d => !pages.Contains(d.ParentCode)).ToList();
            return new JsonResult() { Data = new { _dics = dics } };
        }

        [HttpPost]
        public async Task<JsonResult> AddRatio(Ratio ratio)
        {
            var res = await _ratioServiceRepository.Add(ratio);
            var status = "fail";
            if (res != null)
                status = "success";
            return new JsonResult { Data = new { _status = status, _ratio = res } };
        }

        [HttpPost]
        public async Task<JsonResult> RemoveRatio(Ratio ratio)
        {
            await _ratioServiceRepository.Delete(ratio);
            return new JsonResult { Data = new { _status = "done" } };
        }

        [HttpPost]
        public async Task<JsonResult> BuildRatios()
        {
            await _ratioValueRepo.Add(null);
            return new JsonResult { Data = new { _status = "done" } };
        }
    }
}