using System.Text.RegularExpressions;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class InputUrlModel
    {
        public long Id
        {
            get
            {
                int result;
                if (string.IsNullOrEmpty(Url))
                {
                    result = -1;
                }
                else
                {
                    var categoryId = new Regex("/category/([0-9]+)").Match(Url)
                        .Groups[1].Value.Trim();
                    if (string.IsNullOrEmpty(categoryId))
                    {
                        var storeId = new Regex("/store/all-wholesale-products/([0-9]+)").Match(Url)
                            .Groups[1].Value.Trim();
                        int.TryParse(storeId, out result);
                           
                    }
                    else
                    {
                        int.TryParse(categoryId, out result);
                            
                    }
                }

                return result;
            }
        }

        public string Url { get; set; }

        public string FormattedUrl {
            get
            {
                if (new Regex("/category/([0-9]+)").IsMatch(Url))
                {
                 return $"https://www.aliexpress.com/category/{Id}/patches.html?trafficChannel=main&catName=patches&CatId={Id}&ltype=wholesale&SortType=default&page=1&isrefine=y";
                 
                }
                else
                {
                    return $"https://www.aliexpress.com/store/all-wholesale-products/{Id}.html?scene=allproducts";
                }
            }
        }

        public bool IsCategory => new Regex("/category/([0-9]+)").IsMatch(Url);

        public int SignalRKeyId { get; set; }
    }
}
