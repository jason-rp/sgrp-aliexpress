using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.CrawlService.Interfaces;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class CrawlService : ICrawlService
    {
        private static readonly Random Random = new Random();
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CrawlService));

        public List<CategoryViewModel> GetData(List<InputUrlModel> urls)
        {
            _log.InfoFormat("Start GetData!!");
            var pid = 1;
            var result = new List<CategoryViewModel>();
            try
            {
                foreach (var url in urls)
                {

                    var executeNodeResult = Node("nodescript.js",
                        "\"" + -101 + "\"" + " \"" + GetLoginUrl(url) + "\"" + " \"" + url.Id + "\"" + " \"" +
                        url.IsCategory + "\"" + " \"" +
                        GetRandomMailPass(Random) + "\"", ref pid);
                    if (executeNodeResult.Count == 1)
                    {
                        if (url.IsCategory)
                        {
                            var rawData = JsonConvert.DeserializeObject<FirstPhaseDataModel>(executeNodeResult[0]);
                            _log.InfoFormat("vo 1: " + rawData.Email);
                            if (rawData.IsFirstPhase)
                            {
                                var urlDetails = rawData.FirstPhaseUrlModels;
                                var totalItemCount = 0;
                                var sendData = new List<FirstPhaseUrlModel>();
                                if(urlDetails == null) continue;
                                foreach (var urlDetail in urlDetails)
                                {
                                    _log.InfoFormat("vo 2: ");
                                    if (totalItemCount > 3600)
                                    {

                                        var categoryDataRaw = Node("nodescript.js",
                                            "\"" + -110 + "\"" + " \"" + urlDetail.Url + "\"" + " \"" + url.Id + "\"" +
                                            " \"" + url.IsCategory + "\"" + " \"" +
                                            GetRandomMailPass(Random) + "\"" + " \"" +
                                            GetLoginUrl(url) + "\"" + " \"" + JsonConvert.SerializeObject(sendData) + "\"",
                                            ref pid);
                                        
                                       _log.Info("qua luon 0 ::::" + categoryDataRaw.Count + '"' +  categoryDataRaw[0] + '"');
                                       _log.Info("qua luon 1 ::::" + categoryDataRaw.Count + '"' + categoryDataRaw[1] + '"');
                                       if (categoryDataRaw.Count == 3)
                                       {
                                           _log.Info("qua luon 2 ::::" + categoryDataRaw.Count + '"' + categoryDataRaw[2] + '"');
                                       }
                                        var saveData =
                                            JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoryDataRaw[1]);
                                        DataResolverContext.Init(saveData).ResolveData(url);

                                        totalItemCount = 0;
                                        sendData = new List<FirstPhaseUrlModel>();
                                    }
                                    else
                                    {
                                        sendData.Add(
                                            new FirstPhaseUrlModel
                                            {
                                                Min = urlDetail.Min,
                                                Max = urlDetail.Max,
                                                ResultCount = urlDetail.ResultCount,
                                                Url = "'" + urlDetail.Url + "'"
                                            }
                                        );
                                    }

                                    totalItemCount += urlDetail.ResultCount;
                                }
                            }

                        }
                        else
                        {
                            var saveData =
                                JsonConvert.DeserializeObject<List<CategoryViewModel>>(executeNodeResult[0]);
                            DataResolverContext.Init(saveData).ResolveData(url);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            


            return result;
        }


        public string GetLoginUrl(InputUrlModel model)
        {
            string result;
            if (model.IsCategory)
            {
                using (var client = new HttpClient())
                {
                    using (var response = client.GetAsync(model.FormattedUrl).Result)
                    {
                        using (var content = response.Content)
                        {
                            result = content.ReadAsStringAsync().Result;
                            var document = new HtmlDocument();
                            document.LoadHtml(result);
                            var collection = document.DocumentNode.SelectNodes("//script");
                            if (collection.Count != 1) return result;
                            var loginUrl = new Regex("location.href=\"(.*?)\"\\)").Match(collection[0].InnerHtml)
                                .Groups[1].Value.Trim() + "\")";
                            result = FilterUrl(loginUrl);
                        }
                    }
                }
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

                    __Proc_Start_Info = null;
                }
            }
            catch
            {

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
            result += "@gmail.com|" + "ayuz8555104" + GetRandomCharacter(text, rng) + GetRandomCharacter(text, rng);

            return result;
        }
    }
}