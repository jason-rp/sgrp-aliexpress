using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using log4net;
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
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            var connection = RedisConnectionFactory.GetConnection();

            _log.InfoFormat("Start Program!!");
            var trackedTasks = new List<Task>();
            var numhberOfThreads = 2;


            var browsers = Enumerable.Range(1, numhberOfThreads).ToList();
            var data = new List<string>();
            //_ = Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            RedisMessageModel currents;
            //            connection.GetSubscriber().Subscribe("redis::runNode", (c, v) =>
            //            {
            //                if (!string.IsNullOrEmpty(v))
            //                {
            //                    if (v == "Cancel")
            //                    {
            //                        foreach (var node in Process.GetProcessesByName("node"))
            //                        {
            //                            node.Kill();
            //                        }
            //                    }
            //                    else
            //                    {
            //                        currents = JsonConvert.DeserializeObject<RedisMessageModel>(v);
            //                        if (currents.IsRun)
            //                        {
            //                            var signalRKeyId = 1;
            //                            var semaphore = new SemaphoreSlim(numhberOfThreads);
            //                            foreach (var url in currents.Urls)
            //                            {
            //                                semaphore.Wait();

            //                                var _ = Task.Factory.StartNew(() =>
            //                                {
            //                                    try
            //                                    {
            //                                        var rd = _random.Next(0, numhberOfThreads);
            //                                        browserNumber = browsers[rd];
            //                                        browsers.Remove(browserNumber);

            //                                        _crawlService.GetData(browsers,browserNumber,new List<InputUrlModel>
            //                                                    {
            //                                                        new InputUrlModel
            //                                                        {
            //                                                            Url = url,
            //                                                            SignalRKeyId = signalRKeyId
            //                                                        }

            //                                                    });

            //                                        data.Add(url);
            //                                        signalRKeyId += 1;
            //                                    }
            //                                    catch (Exception ex)
            //                                    {
            //                                        _log.Error(ex);
            //                                    }
            //                                    finally
            //                                    {
            //                                        semaphore.Release();
            //                                    }
            //                                });
            //                                trackedTasks.Add(_);
            //                            }
            //                            Task.WaitAll(trackedTasks.ToArray());
            //                            //Parallel.ForEach(currents.Urls,
            //                            //    new ParallelOptions {MaxDegreeOfParallelism = 5},
            //                            //    (url, state) =>
            //                            //    {
            //                            //        if (data.Count(n => n == url) < 1 || !data.Any())
            //                            //        {
            //                            //            try
            //                            //            {
            //                            //                _crawlService.GetData(new List<InputUrlModel>
            //                            //                {
            //                            //                    new InputUrlModel
            //                            //                    {
            //                            //                        Url = url,
            //                            //                        SignalRKeyId = signalRKeyId
            //                            //                    }

            //                            //                });

            //                            //                data.Add(url);
            //                            //                signalRKeyId += 1;
            //                            //            }
            //                            //            catch (Exception ex)
            //                            //            {
            //                            //                _log.Error(ex);
            //                            //            }

            //                            //        }
            //                            //    });
            //                            //foreach (var url in currents.Urls)
            //                            //{
            //                            //    if (data.Count(n => n == url) < 1 || !data.Any())
            //                            //    {
            //                            //        try
            //                            //        {
            //                            //            _crawlService.GetData(new List<InputUrlModel>
            //                            //            {
            //                            //                new InputUrlModel
            //                            //                {
            //                            //                    Url = url,
            //                            //                    SignalRKeyId = signalRKeyId
            //                            //                }

            //                            //            });

            //                            //            data.Add(url);
            //                            //            signalRKeyId += 1;
            //                            //        }
            //                            //        catch (Exception ex)
            //                            //        {
            //                            //            _log.Error(ex);
            //                            //        }

            //                            //    }


            //                            //}

            //                        }
            //                    }

            //                }

            //            });




            //            Thread.Sleep(2000);
            //        }
            //        catch (Exception ex)
            //        {
            //            _log.Error(ex);
            //        }
            //    }
            //});

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
                    }
                    else
                    {
                        currents = JsonConvert.DeserializeObject<RedisMessageModel>(v);
                        if (currents.IsRun)
                        {
                            var signalRKeyId = 1;
                            var semaphore = new SemaphoreSlim(numhberOfThreads);
                            foreach (var url in currents.Urls)
                            {
                                semaphore.Wait();

                                var browserNumber = browsers[_random.Next(0, browsers.Count)];
                                browsers.Remove(browserNumber);

                                var _ = Task.Factory.StartNew(() =>
                                {
                                    try
                                    {
                                        _crawlService.GetData(browsers, browserNumber, new List<InputUrlModel>
                                        {
                                            new InputUrlModel
                                            {
                                                Url = url,
                                                SignalRKeyId = signalRKeyId
                                            }

                                        });

                                        data.Add(url);
                                        signalRKeyId += 1;
                                    }
                                    catch (Exception ex)
                                    {
                                        _log.Error(ex);
                                    }
                                    finally
                                    {
                                        semaphore.Release();
                                    }
                                });

                                trackedTasks.Add(_);
                            }
                            Task.WaitAll(trackedTasks.ToArray());

                        }
                    }

                }

            });

            Console.WriteLine("\r\nPress any key to continue ...");
            Console.Read();
        }
    }
}
