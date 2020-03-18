using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.CrawlService.Interfaces;
using SGRP.Aliexpress.Helper;
using StackExchange.Redis;

namespace SGRP.Aliexpress.CrawlService
{
    class Program
    {
        private static ICrawlService _crawlService = new Services.CrawlService();
        static void Main(string[] args)
        {
            var connection = RedisConnectionFactory.GetConnection();

            //var data = _crawlService.GetData(new List<InputUrlModel>
            //{
            //    new InputUrlModel
            //    {
            //        Url = $"https://www.aliexpress.com/category/200002253/patches.html?trafficChannel=main&catName=patches&CatId=200002253&ltype=wholesale&SortType=default&page=1&isrefine=y"
            //    }

            //}).ToList();

            var data = new List<string>();
            _ = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        RedisMessageModel currents;
                         connection.GetSubscriber().Subscribe("redis::runNode", (c, v) =>
                        {
                            if (!string.IsNullOrEmpty(v))
                            {
                                if (v == "Cancel")
                                {
                                    foreach (var node in Process.GetProcessesByName("node"))
                                    {
                                        node.Kill();
                                    }
                                    //Environment.Exit(0);
                                }
                                else
                                {
                                    currents = JsonConvert.DeserializeObject<RedisMessageModel>(v);

                                    if (currents.IsRun)
                                    {
                                        var signalRKeyId = 1;
                                        foreach (var url in currents.Urls)
                                        {
                                            if (data.Count(n => n == url) < 1 || !data.Any())
                                            {
                                                var categoryViewModels = _crawlService.GetData(new List<InputUrlModel>
                                                {
                                                    new InputUrlModel
                                                    {
                                                        Url = url,
                                                        SignalRKeyId = signalRKeyId
                                                    }

                                                }).ToList();
                                                data.Add(url);
                                                signalRKeyId += 1;
                                            }
                                        }
                                    }
                                }
                                
                            }

                        });




                        Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        var t = ex;
                    }
                }
            });



            Console.WriteLine("\r\nPress any key to continue ...");
            Console.Read();
        }
    }
}
