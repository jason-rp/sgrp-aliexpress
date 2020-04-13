using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Data;
using SGRP.Aliexpress.Helper;
using SGRP.Aliexpress.Web.Hubs;
using SGRP.Aliexpress.Web.Interfaces;
using SGRP.Aliexpress.Web.Models;
using StackExchange.Redis;

namespace SGRP.Aliexpress.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly AliexpressHub _aliexpressHub;
        //private ApplicationDbContext _context = new DesignTimeDbContextFactory().CreateDbContext()

        public HomeController(ApplicationDbContext context, AliexpressHub aliexpressHub)
        {
            _context = context;
            _aliexpressHub = aliexpressHub;
        }

        //[Route("/Home/Index", Name = "AddCat")]
        //public ActionResult RedisCategory(RedisCategoryUrlModel model)
        //{
        //    if (model.Urls.Any())
        //    {
        //        var data = new RedisMessageModel
        //        {
        //            IsRun = true,
        //            Urls = new List<string>()
        //        };

        //        model.Urls.ForEach(n => data.Urls.Add(n));

        //        RedisConnectionFactory.GetConnection().GetSubscriber().Publish("redis::runNode", JsonConvert.SerializeObject(data));
        //    }

        //    return RedirectToAction("Index");

        //}


        public void Cancel()
        {
            RedisConnectionFactory.GetConnection().GetSubscriber().Publish("redis::runNode", "Cancel");
        }

        public void RedisCategory(RedisCategoryUrlModel model)
        {
            if (model.Urls.Any())
            {
                var data = new RedisMessageModel
                {
                    IsRun = true,
                    Urls = new List<string>()
                };

                model.Urls.ForEach(n => data.Urls.Add(n));

                RedisConnectionFactory.GetConnection().GetSubscriber().Publish("redis::runNode", JsonConvert.SerializeObject(data));
            }

        }


        public async Task<IActionResult> Index()
        {
            _ = Task.Factory.StartNew(async () =>
              {
                  while (true)
                  {
                      try
                      {
                          await _aliexpressHub.UpdateTotalCounter();
                          Thread.Sleep(2000);
                      }
                      catch (Exception ex)
                      {
                          var t = ex;
                      }
                  }
              });

            return View();
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
