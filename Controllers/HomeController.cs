using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.Models;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Countries> QueryCountries;
        private List<Stores> QueryStores;
        private List<Customers> QueryCustomers;
        private Customers QueryCustomer;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var dbContext = new DBClass())
            {
             QueryCountries =   dbContext.Countries.ToList();
             
            }
            ViewBag.vCountries = QueryCountries;
            
           
            return View();
        }


        public IActionResult GetSTores(string CountryCode)
        {
            int vCountryCode = int.Parse(CountryCode);
            using (var dbContext = new DBClass())
            {
                QueryStores = dbContext.Stores.Where(x=>x.CountryID== vCountryCode).ToList();
            }
           
            return Json(QueryStores);
        }
        
            public IActionResult CustomerList()
        {
            using (var dbContext = new DBClass())
            {
                QueryCustomers = dbContext.Customers.ToList();
                QueryStores = dbContext.Stores.ToList();
            }
            ViewBag.vCustomers = QueryCustomers;
            ViewBag.vStores = QueryStores;
            return View();
        }
        public IActionResult GetCustomers(string StoreCode)
        {
            int vStoreCode = int.Parse(StoreCode);
            using (var dbContext = new DBClass())
            {
                QueryCustomers = dbContext.Customers.Where(x => x.StoreID == vStoreCode).ToList();
            }

            return Json(QueryCustomers);
        }

        public IActionResult EditCustomers(string CustomerID)
        {
            bool result = false;
            int vCustomerID = int.Parse(CustomerID);
            using (var dbContext = new DBClass())
            {
                 QueryCustomer = dbContext.Customers.Where(x => x.ID == vCustomerID).FirstOrDefault();
                
            }

            return Json(QueryCustomer);
        }
        public IActionResult SaveCustomers(Customers customers)
        {
            bool result = false;
            int vCustomerID = customers.ID;
            using (var dbContext = new DBClass())
            {
                var QueryCustomer = dbContext.Customers.Where(x => x.ID == vCustomerID).FirstOrDefault();
                if (QueryCustomer != null)
                {
                    QueryCustomer.Active = customers.Active;
                    QueryCustomer.FirstName = customers.FirstName;
                    QueryCustomer.LastName = customers.LastName;
                    QueryCustomer.MoviesRented = customers.MoviesRented;
                    QueryCustomer.StoreID = customers.StoreID;


                    dbContext.SaveChanges();

                    result = true;
                }
                else
                {
                    var MaxID = dbContext.Customers.Select(x => x.ID).Max();                    Customers NewCustomer = new Customers();
                    NewCustomer.ID = MaxID + 1;
                    NewCustomer.Active = customers.Active;
                    NewCustomer.FirstName = customers.FirstName;
                    NewCustomer.LastName = customers.LastName;
                    NewCustomer.MoviesRented = customers.MoviesRented;
                    NewCustomer.StoreID = customers.StoreID;
                    dbContext.Customers.Add(NewCustomer);
                    dbContext.SaveChanges();
                    result = true;
                }
                }
            return Json(result);
            }
        public IActionResult DeleteCustomers(string CustomerID)
        {
            bool result = false;
            int vCustomerID = int.Parse(CustomerID);
            using (var dbContext = new DBClass())
            {
              var  QueryCustomer = dbContext.Customers.Where(x => x.ID == vCustomerID).FirstOrDefault();
                if (QueryCustomer != null) {
                    dbContext.Customers.Remove(QueryCustomer);
                    dbContext.SaveChanges();
                    result = true;
                }
            }

            return Json(result);
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
