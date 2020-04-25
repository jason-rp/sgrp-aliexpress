using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.CrawlService.Interfaces;
using SGRP.Aliexpress.Helper;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class CrawlService : ICrawlService
    {
        private static readonly Random Random = new Random();
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CrawlService));

        public async Task GetData(List<int> browsers, int browserNumber, List<InputUrlModel> urls)
        {
            var pid = 1;
            try
            {
                foreach (var url in urls)
                {

                    var executeNodeResult = Node("nodescript.js",
                        "\"" + -101 + "\"" + " \"" + GetLoginUrl(url) + "\"" + " \"" + url.Id + "\"" + " \"" +
                        url.IsCategory + "\"" + " \""  + browserNumber + "\""  +" \"" +
                        GetRandomMailPass(Random) + "\"", ref pid);
                    if (executeNodeResult.Count == 1)
                    {
                        if (url.IsCategory)
                        {
                            var rawData = JsonConvert.DeserializeObject<FirstPhaseDataModel>(executeNodeResult[0]);
                            if (rawData.IsFirstPhase)
                            {
                                var urlDetails = rawData.FirstPhaseUrlModels;

                                if(urlDetails == null) continue;
                                foreach (var urlDetail in urlDetails)
                                {
                                    var categoryDataRaw = Node("nodescript.js",
                                        "\"" + -110 + "\"" + " \"" + urlDetail.Url + "\"" + " \"" + url.Id + "\"" +
                                        " \"" + url.IsCategory + "\"" + " \"" + browserNumber + "\"" + " \"" +
                                        GetRandomMailPass(Random) + "\"" + " \"" +
                                        GetLoginUrl(url) + "\"" + " \"" + JsonConvert.SerializeObject(new List<FirstPhaseUrlModel>
                                        {
                                            new FirstPhaseUrlModel
                                            {
                                                Min = urlDetail.Min,
                                                Max = urlDetail.Max,
                                                ResultCount = urlDetail.ResultCount,
                                                Url = "'" + urlDetail.Url.Replace("&page=1&isrefine=y","") + "'"
                                            }
                                        }) + "\"",
                                        ref pid);

                                    try
                                    {
                                        if (categoryDataRaw.Count == 2)
                                        {
                                            var saveData =
                                                JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoryDataRaw[1]);
                                            var resolver = new DataResolverService(saveData);

                                            await resolver.ResolveData(url);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("ERROR saveData:::",ex);
                                    }
                                }
                            }

                        }
                        else
                        {
                            try
                            {
                                var saveData =
                                    JsonConvert.DeserializeObject<List<CategoryViewModel>>(executeNodeResult[0]);
                                var resolver = new DataResolverService(saveData);

                                await resolver.ResolveData(url);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("ERROR saveData:::", ex);
                            }

                        }
                    }

                }

                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            RedisConnectionFactory.GetConnection().GetDatabase().StringSet("redis::isSubmit", "false");
            browsers.Add(browserNumber);
        }


        public string GetLoginUrl(InputUrlModel model)
        {
            string result;
            if (model.IsCategory)
            {
                //using (var client = new HttpClient())
                //{
                //    using (var response = client.GetAsync(model.FormattedUrl).Result)
                //    {
                //        using (var content = response.Content)
                //        {
                //            result = content.ReadAsStringAsync().Result;
                //            var document = new HtmlDocument();
                //            document.LoadHtml(result);
                //            var collection = document.DocumentNode.SelectNodes("//script");
                //            if (collection.Count != 1) return result;
                //            var loginUrl = new Regex("location.href=\"(.*?)\"\\)").Match(collection[0].InnerHtml)
                //                .Groups[1].Value.Trim() + "\")";
                //            result = FilterUrl(loginUrl);
                //        }
                //    }
                //}
                result = $"https://login.aliexpress.com";
            }
            else
            {
                result = $"https://login.aliexpress.com/?from=sm&return_url=https://www.aliexpress.com/store/all-wholesale-products/{model.Id}.html?scene=allproducts";
            }


            return result;
        }

        private static string FilterUrl(string url)
        {
            if (url.Contains("location.href="))
            {
                url = new Regex("location.href=\"(.*)\".*?$").Match(url).Groups[1].Value.Trim();
            }

            if (url.Contains("encodeURIComponent"))
            {
                var match = new Regex("(.*?)\".*?encodeURIComponent.*?\"(.*)$").Match(url);
                url = match.Groups[1].Value.Trim() + "" + Uri.EscapeDataString(match.Groups[2].Value.Trim());
            }

            return url;
        }

        public static List<string> Node(string fileName, string command, ref int pId, string dir = "Node",
            int timeout = 0)
        {
            var isDebug = false;

            var result = new List<string>();
            try
            {
                using (var process = new Process())
                {
                    ProcessStartInfo __Proc_Start_Info = new ProcessStartInfo();

                    __Proc_Start_Info.FileName = "node.exe";
                    __Proc_Start_Info.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dir);
                    __Proc_Start_Info.Arguments =
                        (command.Trim().Length > 0) ? fileName + " " + command : fileName;
                    if (!isDebug)
                    {
                        __Proc_Start_Info.RedirectStandardOutput = true;
                        __Proc_Start_Info.UseShellExecute = false;
                        __Proc_Start_Info.CreateNoWindow = true;
                    }

                    __Proc_Start_Info.Verb = "runas";
                    //__Proc_Start_Info.StandardOutputEncoding = Encoding.UTF8;

                    process.StartInfo = __Proc_Start_Info;
                    if (!isDebug)
                    {
                        process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
                        {
                            lock (result)
                            {
                                if (args.Data != null)
                                {
                                    result.Add(args.Data);
                                }
                            }
                        };
                    }

                    process.Start();
                    if (!isDebug)
                    {
                        process.BeginOutputReadLine();
                    }

                    pId = process.Id;


                    if (timeout > 0)
                    {
                        process.WaitForExit(timeout);
                    }
                    else
                    {
                        process.WaitForExit();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error("Node error:",ex);
            }

            return result;
        }

        private static char GetRandomCharacter(string text, Random rng)
        {
            var index = rng.Next(text.Length);
            return text[index];
        }

        private static string GetRandomMailPass(Random rng)
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = "oiyufads";
            for (var i = 0; i < 6; i++)
            {
                result += GetRandomCharacter(text, rng);
            }
            result += rng.Next(0, 9) + rng.Next(1, 9) + GetRandomCharacter(text, rng);
            result += "@gmail.com|" + "ayuz06121587" + GetRandomCharacter(text, rng) + GetRandomCharacter(text, rng);

            return result;
        }
    }
}