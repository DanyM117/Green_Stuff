using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Green_Stuff.Controllers
{
    public class SalesController : Controller
    {
        private readonly string cadenaSQL;
        private readonly DbLabpwebContext _DBContext;
        public SalesController(IConfiguration config, DbLabpwebContext context)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
            _DBContext = context;
        }
        // GET: SalesController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult SalesSummary()
        {
            DateTime StartDate = DateTime.Now;
            StartDate = StartDate.AddDays(-5);
            List<SalesVM> list = (from sales in _DBContext.Sales
                                  where sales.CreationDate.Date >= StartDate.Date
                                  group sales by sales.CreationDate.Date into grupo
                                  select new SalesVM
                                  {
                                      fecha = grupo.Key.ToString("dd/MM/yyyy"),
                                      cantidad = grupo.Count()
                                  }).ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        public IActionResult SaleDetailSummary()
        {
            List<Sale_DetailVM> list = (from salesdetail in _DBContext.SaleDetails
                                        join warehouse in _DBContext.Warehouses
                                        on salesdetail.Iditem equals warehouse.IdItem
                                        group salesdetail by warehouse.Name into grouped
                                        select new Sale_DetailVM
                                        {
                                            producto = grouped.Key,
                                            cantidad = grouped.Sum(g => g.Quantity)
                                        }).OrderByDescending(vm => vm.cantidad).Take(4).ToList();

            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet]
        public JsonResult ListaSales()
        {
            List<Sale> list = new List<Sale>();
            using (var connection = new SqlConnection(cadenaSQL))
            {
                connection.Open();
                var cmd = new SqlCommand("sp_list_Sales", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sale
                        {
                            IdSale = Convert.ToInt32(reader["ID_Sale"]),
                            Iduser = 0,
                            Username = reader["Username"].ToString(),
                            Idcard = 0,
                            P_Card = reader["P_Card"].ToString(),
                            TotalAmmount = Convert.ToDecimal(reader["TotalAmmount"]),
                            Cdate = reader["CreationDate"].ToString(),
                            TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"])
                        });
                    }
                }
            }
            return Json(new { data = list });
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
