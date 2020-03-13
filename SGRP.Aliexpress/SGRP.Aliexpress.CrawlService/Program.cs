using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using Newtonsoft.Json;
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
            var str =
                "{\"desc\":{\"score\":\"4.5\",\"ratings\":\"33\",\"percent\":\"-2.47\"},\"seller\":{\"score\":\"4.5\",\"ratings\":\"33\",\"percent\":\"-3.34\"},\"shipping\":{\"score\":\"4.4\",\"ratings\":\"33\",\"percent\":\"-2.98\"}}";
            var t1 = JsonConvert.DeserializeObject(str);
            var ratingDetails = JsonConvert.DeserializeObject<StoreRatingDetailModel>(str);
            //var ratingDetails1 = JsonConvert.DeserializeObject<List<StoreRatingDetailModel>>(t1);

            var rp = "{\"body\":{\"features\":{},\"freightResult\":[{\"bizShowMind\":{\"layout\":[]},\"commitDay\":\"60\",\"company\":\"China Post Registered Air Mail\",\"currency\":\"USD\",\"discount\":100,\"displayType\":\"deliveryTime\",\"features\":{},\"freightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $0.00\",\"value\":0},\"fullMailLine\":false,\"hbaService\":false,\"i18nMap\":{},\"id\":0,\"ltDisplayModel\":{\"deliveryDisplayModel\":{\"deliveryTime\":\"Estimated Delivery Time:21-39 Days\",\"fromToInfo\":{\"medusaText\":\"To ${toCountry} via ${freightDisplayName}\",\"placeHolderMap\":{\"freightDisplayName\":\"China Post Registered Air Mail\",\"fromCountry\":\"\",\"toIcon\":\"https://ae01.alicdn.com/kf/Hee93bbbc00b64a7b9bd29ddbcc64e9543.png\",\"fromIcon\":\"\",\"toCountry\":\"Vietnam\"}}},\"highLight\":[],\"serviceInfo\":[],\"shippingFeeInfoModel\":{\"shippingFee\":{\"medusaText\":\"Free Shipping\"}}},\"name\":\"FreightItemModule\",\"notification\":\"\",\"sendGoodsCountry\":\"CN\",\"sendGoodsCountryFullName\":\"China\",\"serviceName\":\"CPAM\",\"standardFreightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $7.31\",\"value\":7.31},\"time\":\"21-39\",\"tracking\":false},{\"bizShowMind\":{\"layout\":[]},\"commitDay\":\"60\",\"company\":\"AliExpress Standard Shipping\",\"currency\":\"USD\",\"discount\":89,\"displayType\":\"deliveryTime\",\"features\":{},\"freightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $1.05\",\"value\":1.05},\"fullMailLine\":false,\"hbaService\":false,\"i18nMap\":{},\"id\":0,\"ltDisplayModel\":{\"deliveryDisplayModel\":{\"deliveryTime\":\"Estimated Delivery Time:18-36 Days\",\"fromToInfo\":{\"medusaText\":\"To ${toCountry} via ${freightDisplayName}\",\"placeHolderMap\":{\"freightDisplayName\":\"AliExpress Standard Shipping\",\"fromCountry\":\"\",\"toIcon\":\"https://ae01.alicdn.com/kf/Hee93bbbc00b64a7b9bd29ddbcc64e9543.png\",\"fromIcon\":\"\",\"toCountry\":\"Vietnam\"}}},\"highLight\":[],\"serviceInfo\":[],\"shippingFeeInfoModel\":{\"shippingFee\":{\"medusaText\":\"Shipping: ${shippingFee}\",\"placeHolderMap\":{\"shippingFee\":\"US $1.05\"}}}},\"name\":\"FreightItemModule\",\"sendGoodsCountry\":\"CN\",\"sendGoodsCountryFullName\":\"China\",\"serviceName\":\"CAINIAO_STANDARD\",\"standardFreightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $9.88\",\"value\":9.88},\"time\":\"18-36\",\"tracking\":false},{\"bizShowMind\":{\"layout\":[]},\"commitDay\":\"35\",\"company\":\"ePacket\",\"currency\":\"USD\",\"discount\":0,\"displayType\":\"deliveryTime\",\"features\":{},\"freightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $7.03\",\"value\":7.03},\"fullMailLine\":false,\"hbaService\":false,\"i18nMap\":{},\"id\":0,\"ltDisplayModel\":{\"deliveryDisplayModel\":{\"deliveryTime\":\"Estimated Delivery Time:20-35 Days\",\"fromToInfo\":{\"medusaText\":\"To ${toCountry} via ${freightDisplayName}\",\"placeHolderMap\":{\"freightDisplayName\":\"ePacket\",\"fromCountry\":\"\",\"toIcon\":\"https://ae01.alicdn.com/kf/Hee93bbbc00b64a7b9bd29ddbcc64e9543.png\",\"fromIcon\":\"\",\"toCountry\":\"Vietnam\"}}},\"highLight\":[],\"serviceInfo\":[],\"shippingFeeInfoModel\":{\"shippingFee\":{\"medusaText\":\"Shipping: ${shippingFee}\",\"placeHolderMap\":{\"shippingFee\":\"US $7.03\"}}}},\"name\":\"FreightItemModule\",\"sendGoodsCountry\":\"CN\",\"sendGoodsCountryFullName\":\"China\",\"serviceName\":\"EMS_ZX_ZX_US\",\"standardFreightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $7.03\",\"value\":7.03},\"time\":\"20-35\",\"tracking\":true},{\"bizShowMind\":{\"layout\":[]},\"commitDay\":\"30\",\"company\":\"DHL\",\"currency\":\"USD\",\"discount\":63,\"displayType\":\"deliveryTime\",\"features\":{},\"freightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $21.05\",\"value\":21.05},\"fullMailLine\":false,\"hbaService\":false,\"i18nMap\":{},\"id\":0,\"ltDisplayModel\":{\"deliveryDisplayModel\":{\"deliveryTime\":\"Estimated Delivery Time:8-17 Days\",\"fromToInfo\":{\"medusaText\":\"To ${toCountry} via ${freightDisplayName}\",\"placeHolderMap\":{\"freightDisplayName\":\"DHL\",\"fromCountry\":\"\",\"toIcon\":\"https://ae01.alicdn.com/kf/Hee93bbbc00b64a7b9bd29ddbcc64e9543.png\",\"fromIcon\":\"\",\"toCountry\":\"Vietnam\"}}},\"highLight\":[],\"serviceInfo\":[],\"shippingFeeInfoModel\":{\"shippingFee\":{\"medusaText\":\"Shipping: ${shippingFee}\",\"placeHolderMap\":{\"shippingFee\":\"US $21.05\"}}}},\"name\":\"FreightItemModule\",\"sendGoodsCountry\":\"CN\",\"sendGoodsCountryFullName\":\"China\",\"serviceName\":\"DHL\",\"standardFreightAmount\":{\"currency\":\"USD\",\"formatedAmount\":\"US $56.23\",\"value\":56.23},\"time\":\"8-17\",\"tracking\":true}],\"i18nMap\":{},\"name\":\"LogisticsFreightResp\"},\"code\":200,\"cost\":55,\"success\":true}";
            var r1 = JsonConvert.DeserializeObject<ShippingContentModel>(rp);


            //var data = _crawlService.GetData(new List<InputUrlModel>
            //{
            //    new InputUrlModel
            //    {
            //        Url = $"https://www.aliexpress.com/category/200002253/patches.html?trafficChannel=main&catName=patches&CatId=200002253&ltype=wholesale&SortType=default&page=1&isrefine=y"
            //    }

            //}).ToList();



            Console.WriteLine("\r\nPress any key to continue ...");
            Console.Read();
        }
    }
}
