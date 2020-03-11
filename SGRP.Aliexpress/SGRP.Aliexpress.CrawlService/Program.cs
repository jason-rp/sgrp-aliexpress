using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.CrawlService.Interfaces;
using SGRP.Aliexpress.Data;

namespace SGRP.Aliexpress.CrawlService
{
    class Program
    {
        private static ICrawlService _crawlService = new Services.CrawlService();
        static void Main(string[] args)
        {
            //using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            //{
            //    var releases = context.Users.Take(5);

            //}
            var data = _crawlService.GetData(new List<InputUrlModel>
            {
                new InputUrlModel
                {
                    Url = $"https://www.aliexpress.com/category/100003415/patches.html?trafficChannel=main&catName=patches&CatId=100003415&ltype=wholesale&SortType=default&page=1&isrefine=y"
                }
                
            }).ToList();



            Console.WriteLine("\r\nPress any key to continue ...");
            Console.Read();
        }
    }
}
