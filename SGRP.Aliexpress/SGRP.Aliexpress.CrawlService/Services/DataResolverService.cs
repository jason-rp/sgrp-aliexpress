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
                var variationThemeFinal = string.Empty;
                var variationColor = "";
                var variationSize = "";
                var variationPlus1 = "";
                var variationPlus2 = "";
                var variationShipFrom = "";

                //var description = "";
                //if (!string.IsNullOrEmpty(viewModel.Description) && description != "{}")
                //{
                //    var des = JsonConvert.DeserializeObject<List<string>>(viewModel.Description);
                //    if (des.Any())
                //    {
                //        description = des.Aggregate(description, (current, d) => current + ("<p>" + d + "</p>"));

                //        description = "<strong><p>Item Description: </p></strong>" + description +
                //                      "<strong><p>Item Specification: </p></strong>";
                //    }
                //}

                var description = viewModel.Description.Replace("&nbsp;", string.Empty).Replace("\\n", "<p></p>")
                    .Replace("<span>", "<p>").Replace("</span>", "</p>").Replace("\"", string.Empty)
                    .Replace("\\t", string.Empty);


                var desSplits = description.Split(new string[] {"<p></p>"}, StringSplitOptions.None);
                var desResult = new List<string>();
                if (desSplits.Any())
                {
                    
                    foreach (var desSplit in desSplits)
                    {
                        if (!string.IsNullOrWhiteSpace(desSplit))
                        {
                            if (desSplit.Contains("USD") || desSplit.Contains("$"))
                            {
                                if (desResult.Count > 1)
                                {
                                    desResult.RemoveAt(desResult.Count-1);
                                }
                            }
                            else
                            {
                                desResult.Add(desSplit);
                            }
                        }   
                    }
                }

                var finalDescription = "";

                if (desResult.Any())
                {
                    foreach (var dr in desResult)
                    {
                        if (!dr.Contains("44894") && !dr.Contains("DIN912"))
                        {
                            finalDescription += "<p>" + dr + "</p>";
                        }
                    }
                }


                description = "<strong><p>Item Descripton: </p></strong>" + finalDescription +
                              "<strong><p>Item Specification: </p></strong>";

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
                    ShippingFee = shippingFee,
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
                                    ItemLot = viewModel.ItemLot,
                                    BrandName = viewModel.BrandName,
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


                                var ids = price.skuPropIds.Split(",").Select(int.Parse).ToList();
                                var childName = " - ( ";
                                var bullet1 = "NOTE - You are Choosing: \"";
                                var imgChild = "";
                                var shippingFrom = "";
                                var variations = new List<VariationModel>();

                                if (ids.Any())
                                {

                                    if (ids.Count == 1)
                                    {
                                        var currentProp = skuProps.FirstOrDefault(n =>
                                            n.SkuPropertyValues.Any(i => i.propertyValueId == ids[0]));

                                        if (currentProp != null)
                                        {
                                            var displayName = currentProp.SkuPropertyValues.First(n => n.propertyValueId == ids[0]).propertyValueDisplayName;
                                            if (currentProp.SkuPropertyName.Contains("Color"))
                                            {
                                                variationThemeFinal = "Color";
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.Color,
                                                    Value = displayName
                                                });
                                            }
                                            else
                                            {
                                                variationThemeFinal = "SizeName";
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.Size,
                                                    Value = displayName
                                                });
                                            }
                                        }

                                        //get child name
                                        if (childName == " - ( ")
                                        {
                                            childName +=
                                                currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                    .First(n => n.propertyValueId == ids[0]).propertyValueDisplayName;
                                        }
                                        else
                                        {
                                            childName +=
                                                ", " + currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                    .First(n => n.propertyValueId == ids[0]).propertyValueDisplayName;
                                        }
                                        //get bullet 1
                                        if (bullet1 == "NOTE - You are Choosing: \"")
                                        {
                                            bullet1 += currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                .First(n => n.propertyValueId == ids[0])
                                                .propertyValueDisplayName;
                                        }
                                        else
                                        {
                                            bullet1 += ", " + currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                .First(n => n.propertyValueId == ids[0])
                                                .propertyValueDisplayName;
                                        }

                                        var img1 = currentProp.SkuPropertyValues.Where(n =>
                                            !string.IsNullOrEmpty(n.skuPropertyImagePath) && n.propertyValueId == ids[0]).ToList();
                                        if (img1.Any())
                                        {
                                            imgChild = img1.First().skuPropertyImagePath;
                                        }
                                    }
                                    else
                                    {
                                        variationThemeFinal = "SizeName-ColorName";
                                        foreach (var id in ids)
                                        {
                                            var currentProp = skuProps.FirstOrDefault(n =>
                                                n.SkuPropertyValues.Any(i => i.propertyValueId == id));
                                            if (currentProp == null) continue;
                                            var displayName = currentProp.SkuPropertyValues.First(n => n.propertyValueId == id).propertyValueDisplayName;
                                            if (currentProp.SkuPropertyName.Contains("Color"))
                                            {
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.Color,
                                                    Value = displayName
                                                });
                                            }
                                            else if (currentProp.SkuPropertyName.Contains("SizeName"))
                                            {
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.Size,
                                                    Value = displayName
                                                });
                                            }
                                            else if (currentProp.SkuPropertyName.Contains("Ships From"))
                                            {
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.ShipFrom,
                                                    Value = displayName
                                                });
                                            }
                                            else
                                            {
                                                variations.Add(new VariationModel
                                                {
                                                    Id = VariationEnums.Other,
                                                    Value = displayName
                                                });
                                            }

                                            //get child name
                                            if (childName == " - ( ")
                                            {
                                                childName +=
                                                    currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                        .First(n => n.propertyValueId == id).propertyValueDisplayName;
                                            }
                                            else
                                            {
                                                childName +=
                                                    ", " + currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                        .First(n => n.propertyValueId == id).propertyValueDisplayName;
                                            }
                                            //get bullet 1
                                            if (bullet1 == "NOTE - You are Choosing: \"")
                                            {
                                                bullet1 += currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                    .First(n => n.propertyValueId == id)
                                                    .propertyValueDisplayName;
                                            }
                                            else
                                            {
                                                bullet1 += ", " + currentProp.SkuPropertyName + ": " + currentProp.SkuPropertyValues
                                                    .First(n => n.propertyValueId == id)
                                                    .propertyValueDisplayName;
                                            }

                                            var img1 = currentProp.SkuPropertyValues.Where(n =>
                                                !string.IsNullOrEmpty(n.skuPropertyImagePath) && n.propertyValueId == id).ToList();
                                            if (img1.Any())
                                            {
                                                imgChild = img1.First().skuPropertyImagePath;
                                            }
                                        }
                                    }

                                    variationColor = variations.Any(n => n.Id == VariationEnums.Color)
                                        ? variations.First(n => n.Id == VariationEnums.Color).Value
                                        : "";
                                    variationSize = variations.Any(n => n.Id == VariationEnums.Size)
                                        ? variations.First(n => n.Id == VariationEnums.Size).Value
                                        : "";
                                    variationShipFrom = variations.Any(n => n.Id == VariationEnums.ShipFrom)
                                        ? variations.First(n => n.Id == VariationEnums.ShipFrom).Value
                                        : "";

                                    foreach (var variation in variations.Where(n=>n.Id == VariationEnums.Other || n.Id == VariationEnums.ShipFrom))
                                    {
                                        if (string.IsNullOrEmpty(variationColor))
                                        {
                                            variationColor = variation.Value;
                                        }
                                        else if (string.IsNullOrEmpty(variationSize))
                                        {
                                            variationSize = variation.Value;
                                        }
                                        else if (string.IsNullOrEmpty(variationPlus1))
                                        {
                                            variationPlus1 = variation.Value;
                                        }
                                        else if (string.IsNullOrEmpty(variationPlus2))
                                        {
                                            variationPlus2 = variation.Value;
                                        }
                                    }

                                }

                                bullet1 += " \", Price for this selection only not for all.";
                                childName += " )";


                                productChild.BuyingPrice = price.skuVal.actSkuCalPrice.HasValue
                                    ? Convert.ToDecimal(price.skuVal.actSkuCalPrice.Value)
                                    : Convert.ToDecimal(price.skuVal.skuCalPrice);

                                productChild.TotalPrice = Convert.ToDecimal(price.skuVal.actSkuCalPrice.HasValue
                                    ? Convert.ToDecimal(price.skuVal.actSkuCalPrice.Value)
                                    : Convert.ToDecimal(price.skuVal.skuCalPrice)) + subTotalPrice;

                                productChild.StockNumber = Convert.ToInt64(price.skuVal.availQuantity);


                                productChild.ProductName = product.ProductName + childName;
                                productChild.Bullet1ST = bullet1;
                                productChild.ParentSku = product.ProductId;
                                productChild.RelationshipType = "Variation";
                                productChild.VariationTheme = variationThemeFinal;

                                productChild.VariationColor = variationColor;
                                productChild.VariationSize = variationSize;
                                productChild.VariationPlus1ST = variationPlus1;
                                productChild.VariationPlus2ND = variationPlus2;
                                productChild.VariationShippingFrom = variationShipFrom;

                                productChild.Image1ST = !string.IsNullOrEmpty(imgChild) ? imgChild : product.Image1ST;
                                productChild.Image2ND =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image1ST : product.Image2ND;
                                productChild.Image3RD =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image2ND : product.Image3RD;
                                productChild.Image4TH =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image3RD : product.Image4TH;
                                productChild.Image5TH =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image4TH : product.Image5TH;
                                productChild.Image6TH =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image5TH : product.Image6TH;
                                productChild.Image7TH =
                                    !string.IsNullOrEmpty(imgChild) ? product.Image6TH : product.Image7TH;

                                
                                variationColor = "";
                                variationSize = "";
                                variationPlus1 = "";
                                variationPlus2 = "";
                                variationShipFrom = "";

                                products.Add(productChild);
                                count += 1;
                            }

                        }
                    }
                    else
                    {
                        product.IsParent = false;
                        product.ParentChild = "";
                        product.ProductKeyId = viewModel.ProductId.ToString();
                        product.BuyingPrice = Convert.ToDecimal(prices[0].skuVal.actSkuCalPrice ?? prices[0].skuVal.skuCalPrice);
                        product.TotalPrice = Convert.ToDecimal(prices[0].skuVal.actSkuCalPrice ?? prices[0].skuVal.skuCalPrice) + subTotalPrice;
                        product.Image1ST = viewModel.ImagePatchList;
                    }
                }

                product.VariationTheme = product.IsParent == false ? null : variationThemeFinal;
                product.StockNumber = product.IsParent == false ? product.StockNumber : null;
                product.ParentSku = null;
                product.TotalPrice = product.IsParent == false ? product.TotalPrice : null;
                products.Add(product);
            }

            using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            {
                context.CategoryPaths.Update(category);

                context.ProductParents.UpdateRange(products.Where(n => n.IsParent != null).Select(
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
