using AllPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Diagnostics;

namespace AllPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlConnection _conn;
        private readonly W1Context _w1Context;
        private readonly DBManagerModel _model;
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly CacheModel _cacheModel;

        public HomeController(SqlConnection conn, W1Context w1Context, ILogger<HomeController> logger, IMemoryCache memoryCache, CacheModel cacheModel, IConnectionMultiplexer connectionMultiplexer)
        {
            _conn = conn;
            _w1Context = w1Context;
            _model = new DBManagerModel();
            _logger = logger;
            _memoryCache = memoryCache;
            _cacheModel = cacheModel;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Run HomeController / index");
            List<Work> lstWork;
            if (!_memoryCache.TryGetValue("lstWork", out List<Work>? _))
            {
                List<Work>? cacheValue = _model.GetWork(this._conn);
                _cacheModel.SetMemoryCache(_memoryCache, "lstWork", cacheValue);
            }

            lstWork = _memoryCache.Get<List<Work>>("lstWork") ?? new List<Work>();
            ViewBag.TimeUTC = _cacheModel.GetOrSetTimeCache(_connectionMultiplexer, "cachedTimeUTC");
            ViewBag.TimeTW = _cacheModel.GetOrSetTimeCache(_connectionMultiplexer, "cachedTimeTW");

            this._conn.Close();
            return View(lstWork);
        }
        public ActionResult Create()
        {
            return View(new Work());
        }

        [HttpPost]
        public ActionResult Create(Work work)
        {

            _model.InsertWork(_conn, work);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Work work = _w1Context.Works.Where(x => x.Id == id).SingleOrDefault() ?? new Work();
            return View(work);
        }
        [HttpPost]
        public ActionResult Edit(Work work)
        {
            _model.UpdateWork(_conn, work);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Work work = _w1Context.Works.Find(id) ?? new Work();

            return View(work);
        }
        [HttpPost]
        public ActionResult Delete(Work work)
        {
            _model.DeleteWork(_conn, work);
            return RedirectToAction("Index");
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

        public ActionResult ToHome()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ToWorks()
        {
            return RedirectToAction("Index", "Works");
        }
        public ActionResult ToCompanies()
        {
            return RedirectToAction("Index", "Companies");
        }
    }
}