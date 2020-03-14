using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SGRP.Aliexpress.Bussiness.Models;
using SGRP.Aliexpress.Bussiness.ViewModel;
using SGRP.Aliexpress.Data;
using SGRP.Aliexpress.Data.Entities;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class DataResolverService
    {
        private readonly List<CategoryViewModel> _viewModel;
        public DataResolverService(List<CategoryViewModel> rawData)
        {
            _viewModel = rawData;
        }

        public void ResolveData(long catId)
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            {
                var products = new List<Product>();
                foreach (var viewModel in _viewModel)
                {
                    var ratingDesc = 0M;
                    var ratingComm = 0M;
                    var ratingShip = 0M;
                    var shippingCompany = "";
                    var shippingFee = 0M;
                    var shipping1 = "";
                    var shipping2 = "";
                    var shipping3 = "";
                    var onTimeDelivery = "";

                    if (!string.IsNullOrEmpty(viewModel.StoreRatingMultiple))
                    {
                        var storeRatingMultiple = new Regex("location.href=\"(.*)\".*?$").Match(viewModel.StoreRatingMultiple).Groups[1].Value.Trim();
                        var ratingDetail = JsonConvert.DeserializeObject<StoreRatingDetailModel>(storeRatingMultiple);
                        if (ratingDetail != null)
                        {
                            ratingDesc = ratingDetail.StoreRatingDesc.Score;
                            ratingComm = ratingDetail.StoreRatingSeller.Score;
                            ratingShip = ratingDetail.StoreRatingShipping.Score;
                        }
                    }

                    if (!string.IsNullOrEmpty(viewModel.ShippingContent))
                    {
                        var shippingData = JsonConvert.DeserializeObject<ShippingContentModel>(viewModel.ShippingContent);
                        if (shippingData != null)
                        {
                            var ePack = shippingData.ShippingBody.ShippingFreightResult.Where(n =>
                                n.Company == "ePacket").ToList();
                            if (ePack.Any())
                            {
                                shippingCompany = ePack.First().Company;
                                shippingFee = ePack.First().ShippingFreightAmount.Value;
                                onTimeDelivery = ePack.First().Time;
                            }

                            var sorts = shippingData.ShippingBody.ShippingFreightResult
                                .Select(n => n.ShippingFreightAmount).OrderBy(n => n.Value).ToList();
                            var count = 1;

                            foreach (var sort in sorts)
                            {
                                if (count <= 3)
                                {
                                    var current = shippingData.ShippingBody.ShippingFreightResult.First(n => n.ShippingFreightAmount.Value == sort.Value);
                                    var shippingStr = current.Company + "|" + current.ShippingFreightAmount.Value;
                                    switch (count)
                                    {
                                        case 1:
                                            shipping1 = shippingStr;
                                            break;
                                        case 2:
                                            shipping2 = shippingStr;
                                            break;
                                        case 3:
                                            shipping3 = shippingStr;
                                            break;
                                    }
                                }

                                count += 1;
                            }

                        }
                    }

                    var product = new Product
                    {
                        ProductName = viewModel.ProductName,
                        ProductId = viewModel.ProductId,
                        ProductKeyId = viewModel.ProductId.ToString(),
                        IsParent = true,
                        Description = viewModel.Description,
                        ItemLot = viewModel.ItemLot,
                        BrandName = viewModel.BrandName,
                        StockNumber = viewModel.StockNumber,
                        CategoryId = catId,
                        CategoryName = viewModel.CategoryName,
                        StoreId = viewModel.StoreId,
                        StoreName = viewModel.StoreName,
                        StoreYear = viewModel.StoreYear,
                        StoreRatingPercent = viewModel.StoreRating,
                        StoreRatingDescribed = ratingDesc,
                        StoreRatingCommunication = ratingComm,
                        StoreRatingShippingSpeed = ratingShip,
                        RatingNumber = viewModel.RatingNumber,
                        RatingPercent = Convert.ToDecimal(viewModel.RatingPercentNumber),
                        StoreRatingTotal = viewModel.StoreRatingTotal,
                        OrderNumber = viewModel.OrderNumber,
                        ProcessingTime = string.Empty,
                        ShippingCompany = shippingCompany,
                        ShippingFee = shippingFee,
                        Shipping1ST = shipping1,
                        Shipping2ND = shipping2,
                        Shipping3RD = shipping3,
                        OnTimeDelivery = onTimeDelivery,
                        Specification10 = viewModel.SpecificationHtml,
                        Bullet2ND = viewModel.Specification1,
                        Bullet3RD = viewModel.Specification2,
                        Bullet4TH = viewModel.Specification3,
                        Bullet5TH = viewModel.Specification4,
                        ParentChild = "Parent"

                    };

                    if (!string.IsNullOrEmpty(viewModel.ImagePatchList))
                    {
                        var imgs = viewModel.ImagePatchList.Split("|").ToList();

                        if (imgs.Any())
                        {
                            for (var i = 0; i < imgs.Count; i++)
                            {
                                if (i == 0)
                                {
                                    product.Image1ST = imgs[i];
                                }
                                else if (i == 1)
                                {
                                    product.Image2ND = imgs[i];
                                }
                                else if (i == 2)
                                {
                                    product.Image3RD = imgs[i];
                                }
                                else if (i == 3)
                                {
                                    product.Image4TH = imgs[i];
                                }
                                else if (i == 4)
                                {
                                    product.Image5TH = imgs[i];
                                }
                                else if (i == 5)
                                {
                                    product.Image6TH = imgs[i];
                                }
                                else if (i == 6)
                                {
                                    product.Image7TH = imgs[i];
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(viewModel.SkuPriceList))
                    {
                        products.Add(product);

                        var productChild = product;
                        productChild.IsParent = null;
                        var prices = JsonConvert.DeserializeObject<List<SkuPriceList>>(viewModel.SkuPriceList);
                        if (!string.IsNullOrEmpty(viewModel.ProductSKUPropertyList))
                        {
                            productChild.ParentChild = "Child";

                            var skuProps =
                                JsonConvert.DeserializeObject<List<SkuPropertyListModel>>(viewModel.ProductSKUPropertyList);
                            if (skuProps.Any() && prices.Any())
                            {
                                foreach (var price in prices)
                                {
                                    var ids = price.skuPropIds.Split(",");
                                    var childName = " - (";
                                    var bullet1 = "NOTE - You are Choosing: \"";
                                    var variationTheme = "";
                                    var variationColor = "";
                                    var variationSize = "";
                                    var variationPlus1 = "";
                                    var variationPlus2 = "";
                                    var shippingFrom = "";

                                    foreach (var id in ids)
                                    {
                                        var currentProp = skuProps.Where(n =>
                                                n.SkuPropertyValues.Select(i => i.propertyValueId.ToString()).Contains(id))
                                            .ToList();

                                        if (currentProp.Any())
                                        {
                                            if (childName == " - ( ")
                                            {
                                                childName += currentProp.First().SkuPropertyName + ": " +
                                                             currentProp.First().SkuPropertyValues.First(n => n.propertyValueId.ToString() == id)
                                                                 .propertyValueDisplayName;
                                            }
                                            else
                                            {
                                                childName += ", " + currentProp.First().SkuPropertyValues.First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }

                                            if (bullet1 == "NOTE - You are Choosing: \"")
                                            {
                                                bullet1 += currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else
                                            {
                                                bullet1 += ", " + currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }

                                            if (currentProp.Any(n => n.SkuPropertyName.Contains("Color")))
                                            {
                                                variationTheme += "Color";
                                                variationColor = currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else if (currentProp.Any(n => n.SkuPropertyName.Contains("Size")))
                                            {
                                                variationTheme += " SizeName";
                                                variationSize = currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else if(!string.IsNullOrEmpty(variationColor) && !string.IsNullOrEmpty(variationSize))
                                            {
                                                variationPlus1 = currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else if(!string.IsNullOrEmpty(variationColor) && !string.IsNullOrEmpty(variationSize) && !string.IsNullOrEmpty(variationPlus1))
                                            {
                                                variationPlus2 = currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else if(currentProp.Any(n => n.SkuPropertyName.Contains("Ships From")))
                                            {
                                                shippingFrom += currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName + " , ";
                                            }
                                        }

                                    }



                                    bullet1 += " \", Price for this selection only not for all.";
                                    childName += " )";

                                    var shipping1StValue = !string.IsNullOrEmpty(productChild.Shipping1ST)
                                        ? Convert.ToDecimal((productChild.Shipping1ST.Split("|"))[1])
                                        : 0;

                                    productChild.TotalPrice = productChild.ShippingFee > 0
                                        ? Convert.ToDecimal(Convert.ToDouble(price.skuVal.actSkuCalPrice) + 0.8) +
                                          productChild.ShippingFee
                                        : shipping1StValue +
                                          Convert.ToDecimal(Convert.ToDouble(price.skuVal.actSkuCalPrice) + 0.8);


                                    productChild.ProductName = productChild.ProductName + childName;
                                    productChild.BuyingPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice);
                                    productChild.Bullet1ST = bullet1;
                                    productChild.ParentSku = productChild.ProductId;
                                    productChild.RelationshipType = "Variation";
                                    productChild.VariationTheme = variationTheme;

                                    productChild.VariationColor = variationColor;
                                    productChild.VariationSize = variationSize;
                                    productChild.VariationPlus1ST = variationPlus1;
                                    productChild.VariationPlus2ND = variationPlus2;

                                    //add to db

                                     childName = " - (";
                                     bullet1 = "NOTE - You are Choosing: \"";
                                     variationTheme = "";
                                     variationColor = "";
                                     variationSize = "";
                                     variationPlus1 = "";
                                     variationPlus2 = "";
                                     shippingFrom = "";
                                    products.Add(productChild);
                                }

                            }
                        }
                        else
                        {
                            //single product
                            foreach (var price in prices)
                            {
                                var shipping1StValue = !string.IsNullOrEmpty(productChild.Shipping1ST)
                                    ? Convert.ToDecimal((productChild.Shipping1ST.Split("|"))[1])
                                    : 0;

                                productChild.TotalPrice = productChild.ShippingFee > 0
                                    ? Convert.ToDecimal(Convert.ToDouble(price.skuVal.actSkuCalPrice) + 0.8) +
                                      productChild.ShippingFee
                                    : shipping1StValue +
                                      Convert.ToDecimal(Convert.ToDouble(price.skuVal.actSkuCalPrice) + 0.8);


                                productChild.BuyingPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice);

                                products.Add(productChild);

                            }

                        }


                    }


                }

                //foreach (var product in products)
                //{
                    
                //}
                context.AddRange(products);
                //context.SaveChanges();
            }
        }

    }
}
