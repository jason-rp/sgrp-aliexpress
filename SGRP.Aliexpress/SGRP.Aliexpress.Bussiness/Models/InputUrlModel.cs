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
                        var storeId = new Regex("/store/([0-9]+)").Match(Url)
                            .Groups[1].Value.Trim();
                        int.TryParse(storeId, out result);
                        IsCategory = false;
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

        public bool IsCategory { get; private set; } = true;
    }
}
