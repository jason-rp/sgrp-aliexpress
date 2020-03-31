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
using SGRP.Aliexpress.Helper;

namespace SGRP.Aliexpress.CrawlService.Services
{
    public class DataResolverService
    {
        private readonly List<CategoryViewModel> _viewModel;

        public DataResolverService(List<CategoryViewModel> rawData)
        {
            _viewModel = rawData;
        }

        public void ResolveData(InputUrlModel inputUrlModel)
        {

            var products = new List<Product>();
            var category = new CategoryPath();
            foreach (var viewModel in _viewModel)
            {
                var ratingDesc = 0M;
                var ratingComm = 0M;
                var ratingShip = 0M;
                var shippingCompany = "";
                decimal? shippingFee = 0M;
                var shipping1 = "";
                var shipping2 = "";
                var shipping3 = "";
                var onTimeDelivery = 0;
                var subTotalPrice = 0M;
                var variationTheme = "";
                var variationThemeFinal = string.Empty;
                var variationColor = "";
                var variationSize = "";
                var variationPlus1 = "";
                var variationPlus2 = "";
                var description = viewModel.Description.Replace("&nbsp;", string.Empty).Replace("\n", string.Empty)
                    .Replace("<span>", "<p>").Replace("</span>", "</p>").Replace("\"", string.Empty)
                    .Replace("\\n", string.Empty).Replace("\\t", string.Empty);
                if (!description.Contains("<p>") || !description.Contains("<strong>"))
                {
                    description = string.Empty;
                }

                var specificationHtml = "<p></p><strong>" + viewModel.SpecificationHtml + "</strong>";

                if (!string.IsNullOrEmpty(viewModel.StoreRatingMultiple))
                {
                    var cleanStoreRatingMultiple = viewModel.StoreRatingMultiple.Replace("\n\t", string.Empty);
                    var storeRatingMultiple = new Regex("jQuery.*?\\(.*?({.*}).*?\\)")
                        .Match(cleanStoreRatingMultiple).Groups[1].Value.Trim();
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
                            onTimeDelivery = ePack.First().CommitDay;
                        }

                        var sorts = shippingData.ShippingBody.ShippingFreightResult.Where(n => n.Tracking)
                            .Select(n => new
                            {
                                n.Company,
                                n.ShippingFreightAmount
                            }).OrderBy(n => n.ShippingFreightAmount.Value).ToList();

                        subTotalPrice = ePack.Any()
                            ? 0.8M + ePack.First().ShippingFreightAmount.Value
                            : 0.8M + (sorts.Any() ? sorts.First().ShippingFreightAmount.Value : 0);

                        for (var i = 0; i < sorts.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    shipping1 = sorts[i].Company + " | " + sorts[i].ShippingFreightAmount.Value;
                                    break;
                                case 1:
                                    shipping2 = sorts[i].Company + " | " + sorts[i].ShippingFreightAmount.Value;
                                    break;
                                case 2:
                                    shipping3 = sorts[i].Company + " | " + sorts[i].ShippingFreightAmount.Value;
                                    break;
                            }
                        }
                        


                    }
                }

                var catPaths = viewModel.PatchCategory.Split("|").ToList();
                if (catPaths.Any() && inputUrlModel.IsCategory)
                {
                    category = new CategoryPath
                    {
                        ProductId = viewModel.ProductId,
                        CategoryId = inputUrlModel.Id,
                    };
                    for (var i = 0; i < catPaths.Count; i++)
                    {
                        if (i == 0)
                        {
                            category.CatMain = catPaths[i];
                        }
                        else if (i == 1)
                        {
                            category.CatSub1 = catPaths[i];
                        }
                        else if (i == 2)
                        {
                            category.CatSub2 = catPaths[i];
                        }
                        else if (i == 3)
                        {
                            category.CatSub3 = catPaths[i];
                        }
                    }
                }

                var product = new Product
                {
                    ProductName = viewModel.ProductName,
                    ProductId = viewModel.ProductId,
                    ProductKeyId = viewModel.ProductId.ToString(),
                    IsParent = true,
                    Description = description,
                    ItemLot = viewModel.ItemLot,
                    BrandName = viewModel.BrandName,
                    StockNumber = viewModel.StockNumber,
                    CategoryId = inputUrlModel.IsCategory ? inputUrlModel.Id : (long?) null,
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
                    ShippingFee = shippingFee > 0 ? shippingFee :  null,
                    Shipping1ST = shipping1,
                    Shipping2ND = shipping2,
                    Shipping3RD = shipping3,
                    OnTimeDelivery = onTimeDelivery.ToString(),
                    Specification10 = specificationHtml,
                    Bullet1ST = viewModel.Specification1,
                    Bullet2ND = viewModel.Specification2,
                    Bullet3RD = viewModel.Specification3,
                    Bullet4TH = viewModel.Specification4,
                    Bullet5TH = viewModel.Specification5,
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
                    
                    var prices = JsonConvert.DeserializeObject<List<SkuPriceList>>(viewModel.SkuPriceList);
                    if (!string.IsNullOrEmpty(viewModel.ProductSKUPropertyList))
                    {

                        var skuProps =
                            JsonConvert.DeserializeObject<List<SkuPropertyListModel>>(viewModel.ProductSKUPropertyList);
                        if (skuProps.Any() && prices.Any())
                        {
                            var count = 1;
                            foreach (var price in prices)
                            {
                                var productChild = new Product
                                {
                                    IsParent = null,
                                    ParentChild = "Child",
                                    ProductName = viewModel.ProductName,
                                    ProductId = viewModel.ProductId,
                                    ProductKeyId = viewModel.ProductId + " - " + count,
                                    Description = description,
                                    //BuyingPrice = viewModel.BuyingPrice,
                                    ItemLot = viewModel.ItemLot,
                                    BrandName = viewModel.BrandName,
                                    //StockNumber = viewModel.StockNumber,
                                    CategoryId = inputUrlModel.IsCategory ? inputUrlModel.Id : (long?)null,
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
                                    ShippingFee = shippingFee > 0 ? shippingFee : null,
                                    Shipping1ST = shipping1,
                                    Shipping2ND = shipping2,
                                    Shipping3RD = shipping3,
                                    OnTimeDelivery = onTimeDelivery.ToString(),
                                    Specification10 = specificationHtml,
                                    Bullet2ND = viewModel.Specification1,
                                    Bullet3RD = viewModel.Specification2,
                                    Bullet4TH = viewModel.Specification3,
                                    Bullet5TH = viewModel.Specification4,
                                };


                                var ids = price.skuPropIds.Split(",");
                                var childName = " - ( ";
                                var bullet1 = "NOTE - You are Choosing: \"";
                                var imgChild = "";

                                var shippingFrom = "";

                                foreach (var id in ids)
                                {
                                    var currentProp = skuProps.Where(n =>
                                            n.SkuPropertyValues.Select(i => i.propertyValueId.ToString()).Contains(id))
                                        .ToList();

                                    if (currentProp.Any())
                                    {
                                        if (imgChild == "")
                                        {
                                            imgChild = currentProp.First().SkuPropertyValues.First(n =>
                                                n.propertyValueId.ToString() == id).skuPropertyImagePath;
                                        }
                                        

                                        if (childName == " - ( ")
                                        {
                                            childName += currentProp.First().SkuPropertyName + ": " +
                                                         currentProp.First().SkuPropertyValues.First(n =>
                                                                 n.propertyValueId.ToString() == id)
                                                             .propertyValueDisplayName;
                                        }
                                        else
                                        {
                                            childName +=
                                                ", " + currentProp.First().SkuPropertyName + ": " + currentProp.First().SkuPropertyValues
                                                    .First(n => n.propertyValueId.ToString() == id)
                                                    .propertyValueDisplayName;
                                        }

                                        if (bullet1 == "NOTE - You are Choosing: \"")
                                        {
                                            bullet1 += currentProp.First().SkuPropertyName + ": " + currentProp.First().SkuPropertyValues
                                                .First(n => n.propertyValueId.ToString() == id)
                                                .propertyValueDisplayName;
                                        }
                                        else
                                        {
                                            bullet1 += ", " + currentProp.First().SkuPropertyName + ": "  + currentProp.First().SkuPropertyValues
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
                                        else if (!string.IsNullOrEmpty(variationColor) &&
                                                 !string.IsNullOrEmpty(variationSize))
                                        {
                                            variationPlus1 = currentProp.First().SkuPropertyValues
                                                .First(n => n.propertyValueId.ToString() == id)
                                                .propertyValueDisplayName;
                                        }
                                        else if (!string.IsNullOrEmpty(variationColor) &&
                                                 !string.IsNullOrEmpty(variationSize) &&
                                                 !string.IsNullOrEmpty(variationPlus1))
                                        {
                                            variationPlus2 = currentProp.First().SkuPropertyValues
                                                .First(n => n.propertyValueId.ToString() == id)
                                                .propertyValueDisplayName;
                                        }
                                        else if (currentProp.Any(n => n.SkuPropertyName.Contains("Ships From")))
                                        {
                                            shippingFrom += currentProp.First().SkuPropertyValues
                                                .First(n => n.propertyValueId.ToString() == id)
                                                .propertyValueDisplayName + " , ";
                                        }
                                    }

                                }

                                if (!string.IsNullOrEmpty(variationTheme))
                                {
                                    if (variationTheme.Contains("Color") && variationTheme.Contains("SizeName"))
                                    {
                                        variationThemeFinal = "SizeName-ColorName";
                                    }
                                    else if (variationTheme.Contains("Color") && !variationTheme.Contains("SizeName"))
                                    {
                                        variationThemeFinal = "Color";
                                    }
                                    else if (!variationTheme.Contains("Color") && variationTheme.Contains("SizeName"))
                                    {
                                        variationThemeFinal = "SizeName";
                                    }
                                }



                                bullet1 += " \", Price for this selection only not for all.";
                                childName += " )";

                                var shipping1StValue = !string.IsNullOrEmpty(product.Shipping1ST)
                                    ? Convert.ToDecimal((product.Shipping1ST.Split("|"))[1])
                                    : 0;

                                productChild.BuyingPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice);
                                productChild.TotalPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice) + subTotalPrice;

                                productChild.StockNumber = Convert.ToInt64(price.skuVal.availQuantity);


                                productChild.ProductName = product.ProductName + childName;
                               // productChild.BuyingPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice);
                                productChild.Bullet1ST = bullet1;
                                productChild.ParentSku = product.ProductId;
                                productChild.RelationshipType = "Variation";
                                productChild.VariationTheme = variationThemeFinal;

                                productChild.VariationColor = variationColor;
                                productChild.VariationSize = variationSize;
                                productChild.VariationPlus1ST = variationPlus1;
                                productChild.VariationPlus2ND = variationPlus2;
                                productChild.Image1ST = imgChild;

                                variationTheme = "";
                                variationThemeFinal = string.Empty;
                                variationColor = "";
                                variationSize = "";
                                variationPlus1 = "";
                                variationPlus2 = "";

                                
                                AddImageToProduct(viewModel, productChild);

                                products.Add(productChild);
                                count += 1;
                            }

                        }
                    }
                    else
                    {
                        //single product

                    }



                }

                product.VariationTheme = variationThemeFinal;
                product.ParentSku = null;
                products.Add(product);
            }

            using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            {

                context.CategoryPaths.Add(category);

                context.ProductParents.AddRange(products.Where(n => n.IsParent != null && n.IsParent == true).Select(
                    n => new ProductParent
                    {
                        ProductId = n.ProductId
                    }));


                context.Products.UpdateRange(products);

                context.SaveChanges();

                RedisConnectionFactory.GetConnection().GetSubscriber().PublishAsync("redis::singleCount-" + inputUrlModel.SignalRKeyId, products.Count);

                var totalCount = context.Products.Count();

                RedisConnectionFactory.GetConnection().GetSubscriber().PublishAsync("redis::totalCounter", totalCount);
            }
        }

        private static void AddImageToProduct(CategoryViewModel viewModel, Product productChild)
        {
            if (!string.IsNullOrEmpty(viewModel.ImagePatchList))
            {
                var imgs = viewModel.ImagePatchList.Split("|").ToList();

                if (imgs.Any())
                {
                    for (var i = 0; i < imgs.Count; i++)
                    {
                        if (i == 0)
                        {
                            productChild.Image2ND = imgs[i];
                        }
                        else if (i == 1)
                        {
                            productChild.Image3RD = imgs[i];
                        }
                        else if (i == 2)
                        {
                            productChild.Image4TH = imgs[i];
                        }
                        else if (i == 3)
                        {
                            productChild.Image5TH = imgs[i];
                        }
                        else if (i == 4)
                        {
                            productChild.Image6TH = imgs[i];
                        }
                        else if (i == 5)
                        {
                            productChild.Image7TH = imgs[i];
                        }
                    }
                }
            }
        }
    }
}
