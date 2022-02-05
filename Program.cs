using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStore.Models;

namespace MovieStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbName = "MovieStores.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            using (var dbContext = new DBClass())
            {
               
                dbContext.Database.EnsureCreated();

                if (!dbContext.Stores.Any())
                {
                    dbContext.Stores.AddRange(new Stores[]
                        {
                             new Stores{ ID=1, CountryID=1, location="LAGOS",AvailableMovies=1500 },
                             new Stores{ ID=2, CountryID=1, location="PORT-HACOURT",AvailableMovies=500 },
                             new Stores{ ID=3, CountryID=1, location="IBADAN",AvailableMovies=3500 },
                             new Stores{ ID=4, CountryID=1, location="BENIN",AvailableMovies=5000 }
                        });
                    dbContext.SaveChanges();
                }
               
                if (!dbContext.Countries.Any())
                {
                    dbContext.Countries.AddRange(new Countries[]
                        {
                             new Countries{ ID=1, Name="Nigeria"},
                             new Countries{ ID=2,Name="Ghana"},
                             new Countries{ ID=3, Name="South-Africa" },
                             new Countries{ ID=4, Name="Togo"}
                        });
                    dbContext.SaveChanges();
                }
                if (!dbContext.Customers.Any())
                {
                    dbContext.Customers.AddRange(new Customers[]
                        {
                             new Customers{ ID=1, FirstName="Peter", LastName="Parker",Active =true,MoviesRented=5 ,StoreID=3},
                             new Customers{ ID=2,FirstName="Clark", LastName="Kent",Active =true,MoviesRented=15 ,StoreID=1 },
                             new Customers{ ID=3, FirstName="Berry", LastName="Allen",Active =true,MoviesRented=150 ,StoreID=1},
                             new Customers{ ID=4, FirstName="Luke", LastName="Cage",Active =true,MoviesRented=15 ,StoreID=12}
                        });
                    dbContext.SaveChanges();
                }
            }
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
