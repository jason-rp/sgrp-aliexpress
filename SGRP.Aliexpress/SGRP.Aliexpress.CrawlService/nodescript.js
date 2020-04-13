let __Chrome = require('puppeteer-core');

let Args = process.argv.slice(2);
let _loginCase = parseInt(Args[0]);
let __loginUrl = Args[1];
let _loginUrlId = Args[2];
let __isCategory = Args[3];
let __Mp = Args[4];
let __fakeUrlLogin = Args[5];
let __catUrlDetails = Args[6];

let cookie_all;
let get_all_cookie = function (item) {
    cookie_all += item.name + "=" + encodeURI(item.value) + "; ";
};

let __Set_TOut = function (ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
};
let __randomString = function (length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

async function AutoSlide(page, url) {
    var counter = 0;
    let redifineCategory = await page.evaluate(() => {
        return window.runParams;
    });
    await __Set_TOut(2000);
    if (redifineCategory === undefined) {
        while (counter <= 5) {
            await page.goto(url, { waitUntil: 'domcontentloaded' });
            let runParams = await page.evaluate(() => {
                return window.runParams;
            });
            await __Set_TOut(2000);
            if (runParams === undefined) {
                await page.waitForSelector('.nc_iconfont.btn_slide');
                let sliderElement = await page.$('.slidetounlock');
                let slider = await sliderElement.boundingBox();

                let slideHandle = await page.$('.nc_iconfont.btn_slide');
                let handle = await slideHandle.boundingBox();

                await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
                await page.mouse.down();
                await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
                await page.mouse.up();
                await __Set_TOut(3000);
            }
            delete runParams;
            counter++;
        }
    }
    delete redifineCategory;

};


let __Width = 1800;
let __Height = 1200;


let loopSubCategory = async () => {
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36';
    let __Email = __Mp.split("|")[0].trim();
    let __Password = __Mp.split("|")[1].trim();

    let browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    let page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    // await page.goto(__loginUrl);
    await page.goto(__fakeUrlLogin);


    let [_tabRegister] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[1]/div/ul/li[1]/div');
    if (_tabRegister) {
        await _tabRegister.click();
    }

    let [__Username] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[1]/input');
    if (__Username) {
        await __Username.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(1) > input', __Email, { delay: 20 });

    let [__Pwd] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[2]/input');
    if (__Pwd) {
        await __Pwd.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > input', __Password, { delay: 20 });

    let [__Signin] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[5]/a');
    if (__Signin) {
        await __Signin.click();
    }

    await __Set_TOut(4000);

    // await page.waitForSelector('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > div')
    // let element = await page.$('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > div')
    // let _singinError = await page.evaluate(el => el.textContent, element)
    // if(_singinError == "error")
    // {
    //     var i = 0;
    //     while(i <= 6)
    //     {
    //         i +=1;
    //         if (_tabRegister) {
    //             await _tabRegister.click();
    //         }
    //         if (__Username) {
    //             await __Username.click();
    //         }
    //         var emails = __Email.split('@');
    //         var email = emails[0] + __randomString(4) + '@' + emails[1];

    //         await page.evaluate( () => document.getElementById("expressbuyerlogin").value = "");
    //         await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(1) > input', email, { delay: 20 });
    //         if (__Pwd) {
    //             await __Pwd.click();
    //         }
    //         await page.evaluate( () => document.getElementById("expressbuyerlogin").value = "");
    //         await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > input', __Password, { delay: 20 });
    //         if (__Signin) {
    //             await __Signin.click();
    //         }
    //         await __Set_TOut(3000);
    //     }

    // }
    let [__shopNow] = await page.$x('//*[@id="expressbuyerlogin"]/div/div/div[3]/a');
    if (__shopNow) {
        await __shopNow.click();
    }

    await __Set_TOut(3000);

    await page.goto(__loginUrl);

    await __Set_TOut(3000);

    var __Prepairing_Ajax = false;

    var __Data_Shipping_jSon = "";
    var __Data_AJAX = "";

    var __Done_Url = false;
    var __Done_Url_AJAX = false;


    await page.on('response', async (response) => {
        let responseUrl = response.url();
        if (responseUrl.indexOf("/aeglodetailweb/api/logistics/freight") != -1) {
            __Data_Shipping_jSon = await response.text();
            __Done_Url = true;
        }
        else if (responseUrl.startsWith('https://feedback.aliexpress.com') && responseUrl.indexOf('/display/evaluationDsrAjaxService.htm?callback=') != -1) {
            if (__Prepairing_Ajax) {
                var jQuery = await response.text();
                if (JSON.stringify(jQuery).indexOf('jQuery') !== -1) {
                    __Data_AJAX = jQuery;

                }
                __Done_Url_AJAX = true;
                __Prepairing_Ajax = false;
            }
        }
    });

    let rpss = [];
    if (__isCategory === "True") {
        var urlCategory = null;
        try {
            console.log(__catUrlDetails);
            urlCategory = eval(__catUrlDetails);
        }
        catch (ex) {
            console.log("err: " + ex);
            process.exit(0);
        }

        for (var cat in urlCategory) {
            if (((__catUrlDetails[cat].resultCount / 60) <= 0)) {
                var urlItem = cat.url + '&page=1&isrefine=y';
                await page.goto(urlItem, { waitUntil: 'domcontentloaded' });
                let windowRunParam = await page.evaluate(() => {
                    return window.runParams || "udfi";
                });
                if (windowRunParam === "udfi") {
                    await page.waitForSelector('.nc_iconfont.btn_slide');
                    let sliderElement = await page.$('.slidetounlock');
                    let slider = await sliderElement.boundingBox();

                    let slideHandle = await page.$('.nc_iconfont.btn_slide');
                    let handle = await slideHandle.boundingBox();

                    await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
                    await page.mouse.down();
                    await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
                    await page.mouse.up();
                    await __Set_TOut(3000);
                }
                let categoryName = "";
                let pathCategories = "";

                var redifineCategory = await page.evaluate(() => {
                    return window.runParams === undefined ? undefined : window.runParams.refineCategory;
                });

                try {
                    if (redifineCategory === undefined) {
                        await page.waitForSelector('.nc_iconfont.btn_slide');
                        let sliderElement = await page.$('.slidetounlock');
                        let slider = await sliderElement.boundingBox();

                        let slideHandle = await page.$('.nc_iconfont.btn_slide');
                        let handle = await slideHandle.boundingBox();

                        await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
                        await page.mouse.down();
                        await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
                        await page.mouse.up();
                        await __Set_TOut(3000);
                    }
                    else {
                        if (redifineCategory.length > 0) {
                            categoryName = redifineCategory[0].categoryName;
                            if (redifineCategory[0].pathCategories != undefined && redifineCategory[0].pathCategories.length > 0) {
                                for (var g in redifineCategory[0].pathCategories) {
                                    if (pathCategories === "") {
                                        pathCategories += redifineCategory[0].pathCategories[g].categoryEnName;
                                    }
                                    else {
                                        pathCategories += "|" + redifineCategory[0].pathCategories[g].categoryEnName;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (ex) {
                    console.log(">?: " + ex);
                }

                let runParams = windowRunParam.items;
                let count = 0;
                for (let i in runParams) {
                    try {
                        count += 1;
                        let crawlItem = {
                            categoryId: -1,
                            categoryName: "",
                            pathCategories: "",
                            productId: -1,
                            productName: "",
                            productSkuProps: "",
                            description: "",
                            buyingPrice: "",
                            itemLot: "",
                            brandName: "",
                            stockNumber: 0,
                            storeId: 0,
                            storeName: "",
                            storeYear: "",
                            storeRating: "",
                            storeRatingTotal: 0,
                            orderNumber: 0,
                            ratingNumber: 0,
                            ratingPercentNumber: "",
                            shippingContent: "",
                            imagePathList: "",
                            specificationHtml: "",
                            storeRatingMultiple: "",
                            productSKUPropertyList: "",
                            skuPriceList: "",
                            specification1: "",
                            specification2: "",
                            specification3: "",
                            specification4: "",
                            specification5: "",
                        };


                        let item = runParams[i];

                        var productDetailUrl = 'https:' + item.productDetailUrl;
                        await page.goto(productDetailUrl, { waitUntil: 'domcontentloaded' });

                        AutoSlide(page, productDetailUrl);

                        let windowrunParams = await page.evaluate(() => {
                            return window.runParams || "udfi";
                        });

                        crawlItem.categoryName = categoryName;
                        crawlItem.pathCategories = pathCategories;
                        crawlItem.productSKUPropertyList = windowrunParams.data.skuModule.productSKUPropertyList != undefined ? JSON.stringify(windowrunParams.data.skuModule.productSKUPropertyList) : "";
                        crawlItem.skuPriceList = JSON.stringify(windowrunParams.data.skuModule.skuPriceList);


                        crawlItem.categoryId = windowrunParams.data.commonModule.categoryId;
                        crawlItem.productId = windowrunParams.data.commonModule.productId;
                        crawlItem.productName = windowrunParams.data.titleModule.subject;

                        //get description
                        await page.evaluate(() => window.scrollTo(0, 1000));
                        await page.waitForSelector('.product-overview');
                        await page.$('.product-overview');
                        let text = await page.evaluate(() => document.getElementsByClassName('product-overview')[0].innerHTML);
                        let textJson = JSON.stringify(text);

                        let textReplace = textJson.replace('<span>', '::span::');
                        textReplace = textReplace.replace('</span>', '::_span::');
                        textReplace = textReplace.replace('<strong>', '::strong::');
                        textReplace = textReplace.replace('</strong>', '::_strong::');
                        textReplace = textReplace.replace(/<script>.*?<\/script>/ig, "");
                        let newString = textReplace.replace(/<.*?>/ig, "");

                        let description = newString.replace('::span::', '<span>');
                        description = description.replace('::_span::', '</span>');
                        description = description.replace('::strong::', '<strong>');
                        description = description.replace('::_strong::', '</strong>');

                        crawlItem.description = description;

                        crawlItem.buyingPrice = windowrunParams.data.priceModule.formatedActivityPrice;
                        crawlItem.itemLot = windowrunParams.data.priceModule.lot == true ? windowrunParams.data.priceModule.numberPerLot
                            + " " + windowrunParams.data.priceModule.oddUnitName : "";
                        if (windowrunParams.data.specsModule.props.length > 1) {
                            for (let prop in windowrunParams.data.specsModule.props) {
                                if (windowrunParams.data.specsModule.props[prop].attrNameId == 2) {
                                    crawlItem.brandName = windowrunParams.data.specsModule.props[prop].attrValue;
                                }
                            }
                        }
                        crawlItem.stockNumber = windowrunParams.data.quantityModule.totalAvailQuantity;
                        crawlItem.storeId = windowrunParams.data.storeModule.storeNum;
                        crawlItem.storeName = windowrunParams.data.storeModule.storeName;
                        crawlItem.storeYear = windowrunParams.data.storeModule.openTime;
                        crawlItem.storeRating = windowrunParams.data.storeModule.positiveRate;
                        crawlItem.storeRatingTotal = windowrunParams.data.storeModule.positiveNum;
                        crawlItem.orderNumber = windowrunParams.data.titleModule.tradeCount;
                        crawlItem.ratingNumber = windowrunParams.data.titleModule.feedbackRating.totalValidNum;
                        crawlItem.ratingPercentNumber = windowrunParams.data.titleModule.feedbackRating.averageStar;

                        let imgPathes = "";
                        if (windowrunParams.data.imageModule.imagePathList.length > 0) {
                            for (let img in windowrunParams.data.imageModule.imagePathList) {
                                imgPathes += imgPathes === "" ? windowrunParams.data.imageModule.imagePathList[img] : "|" + windowrunParams.data.imageModule.imagePathList[img];
                            }
                        }
                        crawlItem.imagePathList = imgPathes;

                        let specHtml = "";
                        let restrictionKey = "Brand Name,Certification,Model Number,Size,Length,Height,Width,Wide,Quantity,Pcs";
                        let specCount = 0;
                        if (windowrunParams.data.specsModule.props.length > 0) {
                            for (let elementPath in windowrunParams.data.specsModule.props) {

                                if (restrictionKey.indexOf(windowrunParams.data.specsModule.props[elementPath].attrName) === -1) {
                                    if (specCount < 11) {
                                        specHtml += "<p>" + windowrunParams.data.specsModule.props[elementPath].attrName + " : "
                                            + windowrunParams.data.specsModule.props[elementPath].attrValue + "</p>";
                                    }
                                    if (crawlItem.specification1 == "") {
                                        crawlItem.specification1 = windowrunParams.data.specsModule.props[elementPath].attrName
                                            + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                    }
                                    else if (crawlItem.specification2 == "") {
                                        crawlItem.specification2 = windowrunParams.data.specsModule.props[elementPath].attrName
                                            + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                    }
                                    else if (crawlItem.specification3 == "") {
                                        crawlItem.specification3 = windowrunParams.data.specsModule.props[elementPath].attrName
                                            + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                    }
                                    else if (crawlItem.specification4 == "") {
                                        crawlItem.specification4 = windowrunParams.data.specsModule.props[elementPath].attrName
                                            + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                    }
                                    else if (crawlItem.specification5 == "") {
                                        crawlItem.specification5 = windowrunParams.data.specsModule.props[elementPath].attrName
                                            + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                    }
                                    specCount += 1;
                                }

                            }
                        }

                        crawlItem.specificationHtml = specHtml;

                        if (await page.$('#store-info-wrap > div.store-container > .store-name') !== null) {
                            await page.evaluate(() => window.scrollTo(0, 0));

                            await page.evaluate(x => {
                                const __Scrollable_Section = document.querySelector(x);

                                __Scrollable_Section.scrollTop = __Scrollable_Section.offsetHeight;
                            }, '.product-title');
                            __Data_AJAX = "";
                            __Prepairing_Ajax = false;
                            __Done_Url_AJAX = false;
                            await page.waitForSelector('#store-info-wrap > div.store-container > .store-name', { timeout: 2000, visible: true })
                                .then(async () => {
                                    let __Ajax_X = await page.evaluate(() => {
                                        const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                        return docALL.getBoundingClientRect().x;
                                    });
                                    let __Ajax_Y = await page.evaluate(() => {
                                        const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                        return docALL.getBoundingClientRect().y;
                                    });
                                    let __Ajax_W = await page.evaluate(() => {
                                        const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                        return docALL.getBoundingClientRect().width;
                                    });
                                    let __Ajax_H = await page.evaluate(() => {
                                        const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                        return docALL.getBoundingClientRect().height;
                                    });
                                    __Data_AJAX = "";
                                    __Done_Url_AJAX = false;
                                    __Prepairing_Ajax = true;
                                    await page.mouse.move(__Ajax_X + __Ajax_W / 2, __Ajax_Y + __Ajax_H / 2);
                                    await page.waitForResponse(response => response.url().indexOf("/display/evaluationDsrAjaxService.htm?callback=") !== -1 && response.status() === 200);
                                    await __Set_TOut(500);

                                    crawlItem.storeRatingMultiple = __Data_AJAX;
                                }).catch(async (err) => {
                                });
                        }

                        if (await page.$('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link') !== null) {
                            __Data_Shipping_jSon = "";
                            __Done_Url = false;
                            await page.waitForSelector('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link', { timeout: 2000 })
                                .then(async () => {
                                    await page.click('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link');
                                    await __Set_TOut(500);

                                    crawlItem.shippingContent = __Data_Shipping_jSon;
                                }).catch(async (err) => {
                                });
                        }

                        //result.push(crawlItem);
                        // //end crawl
                        rpss.push(crawlItem);
                        if (count == 3) {
                            console.log(JSON.stringify(rpss));
                            await browser.close();
                            process.exit(0);
                        }

                    }
                    catch (ex) {
                        console.log("Error: " + ex);
                        console.log(item.productDetailUrl);
                    }
                }

            }
            else {
                let numPage = Math.floor((parseInt(urlCategory[cat].resultCount) / 60)) < (parseInt(urlCategory[cat].resultCount) / 60)
                    ? Math.floor((parseInt(urlCategory[cat].resultCount) / 60)) + 1 : Math.floor((parseInt(urlCategory[cat].resultCount) / 60));

                let categoryName = "";
                let pathCategories = "";

                var redifineCategory = await page.evaluate(() => {
                    return window.runParams === undefined ? undefined : window.runParams.refineCategory;
                });

                try {
                    if (redifineCategory === undefined) {
                        await page.waitForSelector('.nc_iconfont.btn_slide');
                        let sliderElement = await page.$('.slidetounlock');
                        let slider = await sliderElement.boundingBox();

                        let slideHandle = await page.$('.nc_iconfont.btn_slide');
                        let handle = await slideHandle.boundingBox();

                        await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
                        await page.mouse.down();
                        await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
                        await page.mouse.up();
                        await __Set_TOut(3000);
                    }
                    else {
                        if (redifineCategory.length > 0) {
                            categoryName = redifineCategory[0].categoryName;
                            if (redifineCategory[0].pathCategories != undefined && redifineCategory[0].pathCategories.length > 0) {
                                for (var g in redifineCategory[0].pathCategories) {
                                    if (pathCategories === "") {
                                        pathCategories += redifineCategory[0].pathCategories[g].categoryEnName;
                                    }
                                    else {
                                        pathCategories += "|" + redifineCategory[0].pathCategories[g].categoryEnName;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (ex) {
                    console.log(">?: " + ex);
                }
                for (let i = 1; i <= numPage; i++) {
                    var urlItem = urlCategory[cat].url + '&page=' + i + '&isrefine=y';
                    await page.goto(urlItem, { waitUntil: 'domcontentloaded' });
                    AutoSlide(page, urlItem);
                    let windowRunParam = await page.evaluate(() => {
                        return window.runParams;
                    });
                    let runParams = windowRunParam.items;
                    // var resultDetailItem = await getItemDetail(browser, page, runParams, categoryName, pathCategories);
                    // rpss.push(resultDetailItem);
                    let count = 0;
                    for (let i in runParams) {
                        try {
                            count += 1;
                            let crawlItem = {
                                categoryId: -1,
                                categoryName: "",
                                pathCategories: "",
                                productId: -1,
                                productName: "",
                                productSkuProps: "",
                                description: "",
                                buyingPrice: "",
                                itemLot: "",
                                brandName: "",
                                stockNumber: 0,
                                storeId: 0,
                                storeName: "",
                                storeYear: "",
                                storeRating: "",
                                storeRatingTotal: 0,
                                orderNumber: 0,
                                ratingNumber: 0,
                                ratingPercentNumber: "",
                                shippingContent: "",
                                imagePathList: "",
                                specificationHtml: "",
                                storeRatingMultiple: "",
                                productSKUPropertyList: "",
                                skuPriceList: "",
                                specification1: "",
                                specification2: "",
                                specification3: "",
                                specification4: "",
                                specification5: "",
                            };


                            let item = runParams[i];

                            var productDetailUrl = 'https:' + item.productDetailUrl;
                            await page.goto(productDetailUrl, { waitUntil: 'domcontentloaded' });

                            AutoSlide(page, productDetailUrl);

                            let windowrunParams = await page.evaluate(() => {
                                return window.runParams || "udfi";
                            });

                            crawlItem.categoryName = categoryName;
                            crawlItem.pathCategories = pathCategories;
                            crawlItem.productSKUPropertyList = windowrunParams.data.skuModule.productSKUPropertyList != undefined ? JSON.stringify(windowrunParams.data.skuModule.productSKUPropertyList) : "";
                            crawlItem.skuPriceList = JSON.stringify(windowrunParams.data.skuModule.skuPriceList);


                            crawlItem.categoryId = windowrunParams.data.commonModule.categoryId;
                            crawlItem.productId = windowrunParams.data.commonModule.productId;
                            crawlItem.productName = windowrunParams.data.titleModule.subject;

                            //get description
                            await page.evaluate(() => window.scrollTo(0, 1000));
                            await page.waitForSelector('.product-overview');
                            await page.$('.product-overview');

                            let text = await page.evaluate(() => document.getElementsByClassName('product-overview')[0].innerHTML);
                            let textJson = JSON.stringify(text);
                            let textReplace = textJson.replace('<span>', '::span::');
                            textReplace = textReplace.replace('</span>', '::_span::');
                            textReplace = textReplace.replace('<strong>', '::strong::');
                            textReplace = textReplace.replace('</strong>', '::_strong::');
                            textReplace = textReplace.replace(/<script>.*?<\/script>/ig, "");
                            let newString = textReplace.replace(/<.*?>/ig, "");
                            let description = newString.replace('::span::', '<span>');
                            description = description.replace('::_span::', '</span>');
                            description = description.replace('::strong::', '<strong>');
                            description = description.replace('::_strong::', '</strong>');
                            crawlItem.description = description;

                            // let text = await page.evaluate(() => document.querySelectorAll('.product-overview')[0].innerText);
                            // crawlItem.description = JSON.stringify(text);

                            crawlItem.buyingPrice = windowrunParams.data.priceModule.formatedActivityPrice;
                            crawlItem.itemLot = windowrunParams.data.priceModule.lot == true ? windowrunParams.data.priceModule.numberPerLot
                                + " " + windowrunParams.data.priceModule.oddUnitName : "";
                            if (windowrunParams.data.specsModule.props.length > 1) {
                                for (let prop in windowrunParams.data.specsModule.props) {
                                    if (windowrunParams.data.specsModule.props[prop].attrNameId == 2) {
                                        crawlItem.brandName = windowrunParams.data.specsModule.props[prop].attrValue;
                                    }
                                }
                            }
                            crawlItem.stockNumber = windowrunParams.data.quantityModule.totalAvailQuantity;
                            crawlItem.storeId = windowrunParams.data.storeModule.storeNum;
                            crawlItem.storeName = windowrunParams.data.storeModule.storeName;
                            crawlItem.storeYear = windowrunParams.data.storeModule.openTime;
                            crawlItem.storeRating = windowrunParams.data.storeModule.positiveRate;
                            crawlItem.storeRatingTotal = windowrunParams.data.storeModule.positiveNum;
                            crawlItem.orderNumber = windowrunParams.data.titleModule.tradeCount;
                            crawlItem.ratingNumber = windowrunParams.data.titleModule.feedbackRating.totalValidNum;
                            crawlItem.ratingPercentNumber = windowrunParams.data.titleModule.feedbackRating.averageStar;

                            let imgPathes = "";
                            if (windowrunParams.data.imageModule.imagePathList.length > 0) {
                                for (let img in windowrunParams.data.imageModule.imagePathList) {
                                    imgPathes += imgPathes === "" ? windowrunParams.data.imageModule.imagePathList[img] : "|" + windowrunParams.data.imageModule.imagePathList[img];
                                }
                            }
                            crawlItem.imagePathList = imgPathes;

                            let specHtml = "";
                            let restrictionKey = "Brand Name,Certification,Model Number,Size,Length,Height,Width,Wide,Quantity,Pcs";
                            let specCount = 0;
                            if (windowrunParams.data.specsModule.props.length > 0) {
                                for (let elementPath in windowrunParams.data.specsModule.props) {

                                    if (restrictionKey.indexOf(windowrunParams.data.specsModule.props[elementPath].attrName) === -1) {
                                        if (specCount < 11) {
                                            specHtml += "<p>" + windowrunParams.data.specsModule.props[elementPath].attrName + " : "
                                                + windowrunParams.data.specsModule.props[elementPath].attrValue + "</p>";
                                        }
                                        if (crawlItem.specification1 == "") {
                                            crawlItem.specification1 = windowrunParams.data.specsModule.props[elementPath].attrName
                                                + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                        }
                                        else if (crawlItem.specification2 == "") {
                                            crawlItem.specification2 = windowrunParams.data.specsModule.props[elementPath].attrName
                                                + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                        }
                                        else if (crawlItem.specification3 == "") {
                                            crawlItem.specification3 = windowrunParams.data.specsModule.props[elementPath].attrName
                                                + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                        }
                                        else if (crawlItem.specification4 == "") {
                                            crawlItem.specification4 = windowrunParams.data.specsModule.props[elementPath].attrName
                                                + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                        }
                                        else if (crawlItem.specification5 == "") {
                                            crawlItem.specification5 = windowrunParams.data.specsModule.props[elementPath].attrName
                                                + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                        }
                                        specCount += 1;
                                    }

                                }
                            }

                            crawlItem.specificationHtml = specHtml;

                            if (await page.$('#store-info-wrap > div.store-container > .store-name') !== null) {
                                await page.evaluate(() => window.scrollTo(0, 0));

                                await page.evaluate(x => {
                                    const __Scrollable_Section = document.querySelector(x);

                                    __Scrollable_Section.scrollTop = __Scrollable_Section.offsetHeight;
                                }, '.product-title');
                                __Data_AJAX = "";
                                __Prepairing_Ajax = false;
                                __Done_Url_AJAX = false;
                                await page.waitForSelector('#store-info-wrap > div.store-container > .store-name', { timeout: 2000, visible: true })
                                    .then(async () => {
                                        let __Ajax_X = await page.evaluate(() => {
                                            const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                            return docALL.getBoundingClientRect().x;
                                        });
                                        let __Ajax_Y = await page.evaluate(() => {
                                            const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                            return docALL.getBoundingClientRect().y;
                                        });
                                        let __Ajax_W = await page.evaluate(() => {
                                            const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                            return docALL.getBoundingClientRect().width;
                                        });
                                        let __Ajax_H = await page.evaluate(() => {
                                            const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                            return docALL.getBoundingClientRect().height;
                                        });
                                        __Data_AJAX = "";
                                        __Done_Url_AJAX = false;
                                        __Prepairing_Ajax = true;
                                        await page.mouse.move(__Ajax_X + __Ajax_W / 2, __Ajax_Y + __Ajax_H / 2);
                                        await page.waitForResponse(response => response.url().indexOf("/display/evaluationDsrAjaxService.htm?callback=") !== -1 && response.status() === 200);
                                        await __Set_TOut(500);

                                        crawlItem.storeRatingMultiple = __Data_AJAX;
                                    }).catch(async (err) => {
                                    });
                            }

                            if (await page.$('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link') !== null) {
                                __Data_Shipping_jSon = "";
                                __Done_Url = false;
                                await page.waitForSelector('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link', { timeout: 2000 })
                                    .then(async () => {
                                        await page.click('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link');
                                        await __Set_TOut(500);

                                        crawlItem.shippingContent = __Data_Shipping_jSon;
                                    }).catch(async (err) => {
                                    });
                            }

                            //result.push(crawlItem);
                            // //end crawl
                            rpss.push(crawlItem);
                            if (count == 3) {
                                console.log(JSON.stringify(rpss));
                                await browser.close();
                                process.exit(0);
                            }

                        }
                        catch (ex) {
                            console.log("Error: " + ex);
                            console.log(item.productDetailUrl);
                        }
                    }
                }

            }
        }
        console.log(JSON.stringify(rpss));
        await browser.close();
        process.exit(0);
    };
}
// let getItemDetail = async (browser, page, runParams, categoryName, pathCategories) => {
//     let count = 0;
//     let result = [];
//     for (let i in runParams) {
//         try {
//             count += 1;
//             let crawlItem = {
//                 categoryId: -1,
//                 categoryName: "",
//                 pathCategories: "",
//                 productId: -1,
//                 productName: "",
//                 productSkuProps: "",
//                 description: "",
//                 buyingPrice: "",
//                 itemLot: "",
//                 brandName: "",
//                 stockNumber: 0,
//                 storeId: 0,
//                 storeName: "",
//                 storeYear: "",
//                 storeRating: "",
//                 storeRatingTotal: 0,
//                 orderNumber: 0,
//                 ratingNumber: 0,
//                 ratingPercentNumber: "",
//                 shippingContent: "",
//                 imagePathList: "",
//                 specificationHtml: "",
//                 storeRatingMultiple: "",
//                 productSKUPropertyList: "",
//                 skuPriceList: "",
//                 specification1: "",
//                 specification2: "",
//                 specification3: "",
//                 specification4: "",
//                 specification5: "",
//             };


//             let item = runParams[i];

//             var productDetailUrl = 'https:' + item.productDetailUrl;
//             await page.goto(productDetailUrl, { waitUntil: 'domcontentloaded' });

//             AutoSlide(page, productDetailUrl);

//             let windowrunParams = await page.evaluate(() => {
//                 return window.runParams || "udfi";
//             });

//             crawlItem.categoryName = categoryName;
//             crawlItem.pathCategories = pathCategories;
//             crawlItem.productSKUPropertyList = windowrunParams.data.skuModule.productSKUPropertyList != undefined ? JSON.stringify(windowrunParams.data.skuModule.productSKUPropertyList) : "";
//             crawlItem.skuPriceList = JSON.stringify(windowrunParams.data.skuModule.skuPriceList);


//             crawlItem.categoryId = windowrunParams.data.commonModule.categoryId;
//             crawlItem.productId = windowrunParams.data.commonModule.productId;
//             crawlItem.productName = windowrunParams.data.titleModule.subject;

//             //get description
//             await page.evaluate(() => window.scrollTo(0, 1000));
//             await page.waitForSelector('.product-overview');
//             await page.$('.product-overview');
//             let text = await page.evaluate(() => document.getElementsByClassName('product-overview')[0].innerHTML);
//             let textJson = JSON.stringify(text);

//             let textReplace = textJson.replace('<span>', '::span::');
//             textReplace = textReplace.replace('</span>', '::_span::');
//             textReplace = textReplace.replace('<strong>', '::strong::');
//             textReplace = textReplace.replace('</strong>', '::_strong::');
//             textReplace = textReplace.replace(/<script>.*?<\/script>/ig, "");
//             let newString = textReplace.replace(/<.*?>/ig, "");

//             let description = newString.replace('::span::', '<span>');
//             description = description.replace('::_span::', '</span>');
//             description = description.replace('::strong::', '<strong>');
//             description = description.replace('::_strong::', '</strong>');

//             crawlItem.description = description;

//             crawlItem.buyingPrice = windowrunParams.data.priceModule.formatedActivityPrice;
//             crawlItem.itemLot = windowrunParams.data.priceModule.lot == true ? windowrunParams.data.priceModule.numberPerLot
//                 + " " + windowrunParams.data.priceModule.oddUnitName : "";
//             if (windowrunParams.data.specsModule.props.length > 1) {
//                 for (let prop in windowrunParams.data.specsModule.props) {
//                     if (windowrunParams.data.specsModule.props[prop].attrNameId == 2) {
//                         crawlItem.brandName = windowrunParams.data.specsModule.props[prop].attrValue;
//                     }
//                 }
//             }
//             crawlItem.stockNumber = windowrunParams.data.quantityModule.totalAvailQuantity;
//             crawlItem.storeId = windowrunParams.data.storeModule.storeNum;
//             crawlItem.storeName = windowrunParams.data.storeModule.storeName;
//             crawlItem.storeYear = windowrunParams.data.storeModule.openTime;
//             crawlItem.storeRating = windowrunParams.data.storeModule.positiveRate;
//             crawlItem.storeRatingTotal = windowrunParams.data.storeModule.positiveNum;
//             crawlItem.orderNumber = windowrunParams.data.titleModule.tradeCount;
//             crawlItem.ratingNumber = windowrunParams.data.titleModule.feedbackRating.totalValidNum;
//             crawlItem.ratingPercentNumber = windowrunParams.data.titleModule.feedbackRating.averageStar;

//             let imgPathes = "";
//             if (windowrunParams.data.imageModule.imagePathList.length > 0) {
//                 for (let img in windowrunParams.data.imageModule.imagePathList) {
//                     imgPathes += imgPathes === "" ? windowrunParams.data.imageModule.imagePathList[img] : "|" + windowrunParams.data.imageModule.imagePathList[img];
//                 }
//             }
//             crawlItem.imagePathList = imgPathes;

//             let specHtml = "";
//             let restrictionKey = "Brand Name,Certification,Model Number,Size,Length,Height,Width,Wide,Quantity,Pcs";
//             let specCount = 0;
//             if (windowrunParams.data.specsModule.props.length > 0) {
//                 for (let elementPath in windowrunParams.data.specsModule.props) {

//                     if (restrictionKey.indexOf(windowrunParams.data.specsModule.props[elementPath].attrName) === -1) {
//                         if (specCount < 11) {
//                             specHtml += "<p>" + windowrunParams.data.specsModule.props[elementPath].attrName + " : "
//                                 + windowrunParams.data.specsModule.props[elementPath].attrValue + "</p>";
//                         }
//                         if (crawlItem.specification1 == "") {
//                             crawlItem.specification1 = windowrunParams.data.specsModule.props[elementPath].attrName
//                                 + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
//                         }
//                         else if (crawlItem.specification2 == "") {
//                             crawlItem.specification2 = windowrunParams.data.specsModule.props[elementPath].attrName
//                                 + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
//                         }
//                         else if (crawlItem.specification3 == "") {
//                             crawlItem.specification3 = windowrunParams.data.specsModule.props[elementPath].attrName
//                                 + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
//                         }
//                         else if (crawlItem.specification4 == "") {
//                             crawlItem.specification4 = windowrunParams.data.specsModule.props[elementPath].attrName
//                                 + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
//                         }
//                         else if (crawlItem.specification5 == "") {
//                             crawlItem.specification5 = windowrunParams.data.specsModule.props[elementPath].attrName
//                                 + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
//                         }
//                         specCount += 1;
//                     }

//                 }
//             }

//             crawlItem.specificationHtml = specHtml;

//             if (await page.$('#store-info-wrap > div.store-container > .store-name') !== null) {
//                 await page.evaluate(() => window.scrollTo(0, 0));

//                 await page.evaluate(x => {
//                     const __Scrollable_Section = document.querySelector(x);

//                     __Scrollable_Section.scrollTop = __Scrollable_Section.offsetHeight;
//                 }, '.product-title');
//                 __Data_AJAX = "";
//                 __Prepairing_Ajax = false;
//                 __Done_Url_AJAX = false;
//                 await page.waitForSelector('#store-info-wrap > div.store-container > .store-name', { timeout: 2000, visible: true })
//                     .then(async () => {
//                         let __Ajax_X = await page.evaluate(() => {
//                             const docALL = document.querySelector('#store-info-wrap > div.store-container');
//                             return docALL.getBoundingClientRect().x;
//                         });
//                         let __Ajax_Y = await page.evaluate(() => {
//                             const docALL = document.querySelector('#store-info-wrap > div.store-container');
//                             return docALL.getBoundingClientRect().y;
//                         });
//                         let __Ajax_W = await page.evaluate(() => {
//                             const docALL = document.querySelector('#store-info-wrap > div.store-container');
//                             return docALL.getBoundingClientRect().width;
//                         });
//                         let __Ajax_H = await page.evaluate(() => {
//                             const docALL = document.querySelector('#store-info-wrap > div.store-container');
//                             return docALL.getBoundingClientRect().height;
//                         });
//                         __Data_AJAX = "";
//                         __Done_Url_AJAX = false;
//                         __Prepairing_Ajax = true;
//                         await page.mouse.move(__Ajax_X + __Ajax_W / 2, __Ajax_Y + __Ajax_H / 2);
//                         await page.waitForResponse(response => response.url().indexOf("/display/evaluationDsrAjaxService.htm?callback=") !== -1 && response.status() === 2000);
//                         await __Set_TOut(500);

//                         crawlItem.storeRatingMultiple = __Data_AJAX;
//                     }).catch(async (err) => {
//                         console.log("1: " + err);
//                     });
//             }

//             if (await page.$('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link') !== null) {
//                 __Data_Shipping_jSon = "";
//                 __Done_Url = false;
//                 await page.waitForSelector('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link', { timeout: 2000 })
//                     .then(async () => {
//                         await page.click('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link');
//                         await __Set_TOut(500);

//                         crawlItem.shippingContent = __Data_Shipping_jSon;
//                     }).catch(async (err) => {
//                         console.log("2: " + err);
//                     });
//             }

//             result.push(crawlItem);
//             // //end crawl
//             // // rpss.push(crawlItem);
//             // if (count == 3) {
//             //     console.log(JSON.stringify(result));
//             //     await browser.close();
//             //     process.exit(0);
//             // }

//         }
//         catch (ex) {
//             console.log("Error: " + ex);
//             console.log(item.productDetailUrl);
//         }
//     }
//     return result;

// };

let registerNewAccount = async () => {
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36';
    let __Email = __Mp.split("|")[0].trim();
    let __Password = __Mp.split("|")[1].trim();

    let browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    let page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    await page.goto(__loginUrl);

    let [_tabRegister] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[1]/div/ul/li[1]/div');
    if (_tabRegister) {
        await _tabRegister.click();
    }

    let [__Username] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[1]/input');
    if (__Username) {
        await __Username.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(1) > input', __Email, { delay: 20 });

    let [__Pwd] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[2]/input');
    if (__Pwd) {
        await __Pwd.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > input', __Password, { delay: 20 });

    let [__Signin] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[5]/a');
    if (__Signin) {
        await __Signin.click();
    }

    await __Set_TOut(5000);


    let [__shopNow] = await page.$x('//*[@id="expressbuyerlogin"]/div/div/div[3]/a');
    if (__shopNow) {
        await __shopNow.click();
    }

    await __Set_TOut(3000);

    var __Prepairing_Ajax = false;

    var __Data_Shipping_jSon = "";
    var __Data_AJAX = "";

    var __Done_Url = false;
    var __Done_Url_AJAX = false;


    await page.on('response', async (response) => {
        let responseUrl = response.url();
        if (responseUrl.indexOf("/aeglodetailweb/api/logistics/freight") != -1) {
            __Data_Shipping_jSon = await response.text();
            __Done_Url = true;
        }
        else if (responseUrl.startsWith('https://feedback.aliexpress.com') && responseUrl.indexOf('/display/evaluationDsrAjaxService.htm?callback=') != -1) {
            if (__Prepairing_Ajax) {
                var jQuery = await response.text();
                if (JSON.stringify(jQuery).indexOf('jQuery') !== -1) {
                    __Data_AJAX = jQuery;

                }
                __Done_Url_AJAX = true;
                __Prepairing_Ajax = false;
            }
        }
    });

    //get all item in each category
    let rpss = [];

    if (__isCategory == "True") {

        let urlItem = 'https://www.aliexpress.com/category/' + _loginUrlId + '/patches.html?trafficChannel=main&catName=patches&CatId=' + _loginUrlId + '&ltype=wholesale&SortType=default&page=' + 1 + '&isrefine=y';
        await page.goto(urlItem, { waitUntil: 'domcontentloaded' });

        AutoSlide(page, urlItem);

        let runConfigs = await page.evaluate(() => {
            return window.runConfigs || "udfi";
        });

        if (runConfigs !== "udfi") {
            let realCatName = runConfigs.searchQuery.catName;

            let isContinute = true;
            let min = 0.1;
            let max = 100;
            let searchCount = 0;


            let minMaxResult = [];
            let urlWithMinMax = [];

            while (isContinute) {

                urlItem = 'https://www.aliexpress.com/category/' + _loginUrlId + '/' + realCatName + '.html?trafficChannel=main&catName='
                    + realCatName + '&CatId=' + _loginUrlId + '&ltype=wholesale&SortType=total_tranpro_desc&' + '&minPrice=' + min + '&maxPrice=' + max + '&page=1&isrefine=y';

                await page.goto(urlItem, { waitUntil: 'domcontentloaded' });

                let currentWindowRunParam = await page.evaluate(() => {
                    return window.runParams || "udfi";
                });

                if (currentWindowRunParam === "udfi") {
                    await page.waitForSelector('.nc_iconfont.btn_slide');
                    let sliderElement = await page.$('.slidetounlock');
                    let slider = await sliderElement.boundingBox();

                    let slideHandle = await page.$('.nc_iconfont.btn_slide');
                    let handle = await slideHandle.boundingBox();

                    await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
                    await page.mouse.down();
                    await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
                    await page.mouse.up();
                    await __Set_TOut(3000);
                }

                if (currentWindowRunParam.resultCount == 0 && minMaxResult.length > 0) {
                    isContinute = false;
                }
                else {
                    if (currentWindowRunParam.resultCount <= 3600) {
                        minMaxResult.push({ min: min, max: max, resultCount: currentWindowRunParam.resultCount });

                        urlWithMinMax.push({
                            min: min,
                            max: max,
                            resultCount: currentWindowRunParam.resultCount,
                            url: urlItem
                        });


                        searchCount = currentWindowRunParam.resultCount;

                        let range = max - min;
                        min = max;
                        max += range;
                    }
                    else {
                        if (min == 0.1) {
                            min = min;
                            max = max / 2;
                        }
                        else {
                            let range = max - min;
                            min = max;
                            max = max + (range / 2);
                        }
                    }
                    if ((max - min) < 0.5) {
                        min = min;
                        max = max + 20;
                    }
                }
                delete currentWindowRunParam;
                await __Set_TOut(1000);
            }

            let firstPhaseData = {
                isFistPhase: true,
                data: urlWithMinMax,
                email: __Email,
                passWord: __Password
            };

            console.log(JSON.stringify(firstPhaseData));
            await browser.close();
            process.exit(0);
        }

    }
    else {
        for (let i = 1; i <= 999; i++) {
            let urlItem = 'https://www.aliexpress.com/store/' + _loginUrlId + '/search/' + i + '.html';
            await page.goto(urlItem, { waitUntil: 'domcontentloaded' });
            let runParams = [];
            await page.waitForSelector('.ui-box-body');
            let selector = await page.$('.items-list util-clearfix');
            let storeLinks = await page.evaluate(() =>
                Array.from(
                    document.querySelectorAll('a[href]'),
                    a => a.getAttribute('href')
                ), selector
            );
            if (storeLinks.length <= 0) {
                return;
            }
            for (let storeLink in storeLinks) {

                if (storeLinks[storeLink].indexOf("aliexpress.com/item/") > -1) {
                    runParams.push({
                        productDetailUrl: storeLinks[storeLink]
                    });
                }
            }
            //check single item
            for (let i in runParams) {
                try {
                    let crawlItem = {
                        categoryId: -1,
                        categoryName: "",
                        pathCategories: "",
                        productId: -1,
                        productName: "",
                        productSkuProps: "",
                        description: "",
                        buyingPrice: "",
                        itemLot: "",
                        brandName: "",
                        stockNumber: 0,
                        storeId: 0,
                        storeName: "",
                        storeYear: "",
                        storeRating: "",
                        storeRatingTotal: 0,
                        orderNumber: 0,
                        ratingNumber: 0,
                        ratingPercentNumber: "",
                        shippingContent: "",
                        imagePathList: "",
                        specificationHtml: "",
                        storeRatingMultiple: "",
                        productSKUPropertyList: "",
                        skuPriceList: "",
                        specification1: "",
                        specification2: "",
                        specification3: "",
                        specification4: "",
                        specification5: "",
                    };


                    let item = runParams[i];

                    var productDetailUrl = 'https:' + item.productDetailUrl;
                    await page.goto(productDetailUrl, { waitUntil: 'domcontentloaded' });

                    AutoSlide(page, productDetailUrl);

                    let windowrunParams = await page.evaluate(() => {
                        return window.runParams || "udfi";
                    });


                    crawlItem.productSKUPropertyList = windowrunParams.data.skuModule.productSKUPropertyList != undefined ? JSON.stringify(windowrunParams.data.skuModule.productSKUPropertyList) : "";
                    crawlItem.skuPriceList = JSON.stringify(windowrunParams.data.skuModule.skuPriceList);


                    crawlItem.categoryId = windowrunParams.data.commonModule.categoryId;
                    crawlItem.productId = windowrunParams.data.commonModule.productId;
                    crawlItem.productName = windowrunParams.data.titleModule.subject;

                    //get description
                    await page.evaluate(() => window.scrollTo(0, 1000));
                    await page.waitForSelector('.product-overview');
                    await page.$('.product-overview');
                    let text = await page.evaluate(() => document.getElementsByClassName('product-overview')[0].innerHTML);
                    let textJson = JSON.stringify(text);

                    let textReplace = textJson.replace('<span>', '::span::');
                    textReplace = textReplace.replace('</span>', '::_span::');
                    textReplace = textReplace.replace('<strong>', '::strong::');
                    textReplace = textReplace.replace('</strong>', '::_strong::');
                    textReplace = textReplace.replace(/<script>.*?<\/script>/ig, "");
                    let newString = textReplace.replace(/<.*?>/ig, "");

                    let description = newString.replace('::span::', '<span>');
                    description = description.replace('::_span::', '</span>');
                    description = description.replace('::strong::', '<strong>');
                    description = description.replace('::_strong::', '</strong>');

                    crawlItem.description = description;

                    crawlItem.buyingPrice = windowrunParams.data.priceModule.formatedActivityPrice;
                    crawlItem.itemLot = windowrunParams.data.priceModule.lot == true ? windowrunParams.data.priceModule.numberPerLot
                        + " " + windowrunParams.data.priceModule.oddUnitName : "";
                    if (windowrunParams.data.specsModule.props.length > 1) {
                        for (let prop in windowrunParams.data.specsModule.props) {
                            if (windowrunParams.data.specsModule.props[prop].attrNameId == 2) {
                                crawlItem.brandName = windowrunParams.data.specsModule.props[prop].attrValue;
                            }
                        }
                    }
                    crawlItem.stockNumber = windowrunParams.data.quantityModule.totalAvailQuantity;
                    crawlItem.storeId = windowrunParams.data.storeModule.storeNum;
                    crawlItem.storeName = windowrunParams.data.storeModule.storeName;
                    crawlItem.storeYear = windowrunParams.data.storeModule.openTime;
                    crawlItem.storeRating = windowrunParams.data.storeModule.positiveRate;
                    crawlItem.storeRatingTotal = windowrunParams.data.storeModule.positiveNum;
                    crawlItem.orderNumber = windowrunParams.data.titleModule.tradeCount;
                    crawlItem.ratingNumber = windowrunParams.data.titleModule.feedbackRating.totalValidNum;
                    crawlItem.ratingPercentNumber = windowrunParams.data.titleModule.feedbackRating.averageStar;

                    let imgPathes = "";
                    if (windowrunParams.data.imageModule.imagePathList.length > 0) {
                        for (let img in windowrunParams.data.imageModule.imagePathList) {
                            imgPathes += imgPathes === "" ? windowrunParams.data.imageModule.imagePathList[img] : "|" + windowrunParams.data.imageModule.imagePathList[img];
                        }
                    }
                    crawlItem.imagePathList = imgPathes;

                    let specHtml = "";
                    let restrictionKey = "Brand Name,Certification,Model Number,Size,Length,Height,Width,Wide,Quantity,Pcs";
                    let specCount = 0;
                    if (windowrunParams.data.specsModule.props.length > 0) {
                        for (let elementPath in windowrunParams.data.specsModule.props) {

                            if (restrictionKey.indexOf(windowrunParams.data.specsModule.props[elementPath].attrName) === -1) {
                                if (specCount < 11) {
                                    specHtml += "<p>" + windowrunParams.data.specsModule.props[elementPath].attrName + " : "
                                        + windowrunParams.data.specsModule.props[elementPath].attrValue + "</p>";
                                }
                                if (crawlItem.specification1 == "") {
                                    crawlItem.specification1 = windowrunParams.data.specsModule.props[elementPath].attrName
                                        + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                }
                                else if (crawlItem.specification2 == "") {
                                    crawlItem.specification2 = windowrunParams.data.specsModule.props[elementPath].attrName
                                        + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                }
                                else if (crawlItem.specification3 == "") {
                                    crawlItem.specification3 = windowrunParams.data.specsModule.props[elementPath].attrName
                                        + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                }
                                else if (crawlItem.specification4 == "") {
                                    crawlItem.specification4 = windowrunParams.data.specsModule.props[elementPath].attrName
                                        + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                }
                                else if (crawlItem.specification5 == "") {
                                    crawlItem.specification5 = windowrunParams.data.specsModule.props[elementPath].attrName
                                        + ": " + windowrunParams.data.specsModule.props[elementPath].attrValue;
                                }
                                specCount += 1;
                            }

                        }
                    }

                    crawlItem.specificationHtml = specHtml;

                    if (await page.$('#store-info-wrap > div.store-container > .store-name') !== null) {
                        await page.evaluate(() => window.scrollTo(0, 0));

                        await page.evaluate(x => {
                            const __Scrollable_Section = document.querySelector(x);

                            __Scrollable_Section.scrollTop = __Scrollable_Section.offsetHeight;
                        }, '.product-title');
                        __Data_AJAX = "";
                        __Prepairing_Ajax = false;
                        __Done_Url_AJAX = false;
                        await page.waitForSelector('#store-info-wrap > div.store-container > .store-name', { timeout: 2000, visible: true })
                            .then(async () => {
                                let __Ajax_X = await page.evaluate(() => {
                                    const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                    return docALL.getBoundingClientRect().x;
                                });
                                let __Ajax_Y = await page.evaluate(() => {
                                    const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                    return docALL.getBoundingClientRect().y;
                                });
                                let __Ajax_W = await page.evaluate(() => {
                                    const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                    return docALL.getBoundingClientRect().width;
                                });
                                let __Ajax_H = await page.evaluate(() => {
                                    const docALL = document.querySelector('#store-info-wrap > div.store-container');
                                    return docALL.getBoundingClientRect().height;
                                });
                                __Data_AJAX = "";
                                __Done_Url_AJAX = false;
                                __Prepairing_Ajax = true;
                                await page.mouse.move(__Ajax_X + __Ajax_W / 2, __Ajax_Y + __Ajax_H / 2);
                                await page.waitForResponse(response => response.url().indexOf("/display/evaluationDsrAjaxService.htm?callback=") !== -1 && response.status() === 200);
                                await __Set_TOut(500);

                                crawlItem.storeRatingMultiple = __Data_AJAX;
                            }).catch(async (err) => {
                            });
                    }

                    if (await page.$('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link') !== null) {
                        __Data_Shipping_jSon = "";
                        __Done_Url = false;
                        await page.waitForSelector('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link', { timeout: 2000 })
                            .then(async () => {
                                await page.click('#root > div > div.product-main > div > div.product-info > div.product-shipping > span.product-shipping-info.black-link');
                                await __Set_TOut(500);

                                crawlItem.shippingContent = __Data_Shipping_jSon;
                            }).catch(async (err) => {
                            });
                    }
                    rpss.push(crawlItem);

                    // console.log(JSON.stringify(rpss));
                    // await browser.close();
                    // process.exit(0);
                }
                catch (ex) {
                    console.log("Error: " + ex);
                    console.log(item.productDetailUrl);
                }
            }
        }
    }
    console.log(JSON.stringify(rpss));
    await browser.close();
    process.exit(0);
}

const getDetailCookie = async () => {
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36';

    let browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    let page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    await page.goto(__loginUrl);

    const [__buyNow] = await page.$x('//*[@id="root"]/div/div[2]/div/div[2]/div[11]/span[1]/button');
    if (__buyNow) {
        let cookiesSet = await page.cookies(page.url());
        cookie_all = "";
        cookiesSet.forEach(get_all_cookie);

        console.log("DetailCookie|" + cookie_all);
    }
    else {
        let [__slider] = await page.$x('//*[@id="nc_1__scale_text"]/span');
        if (__slider) {
            //console.log("Slider|");
            let sliderElement = await page.$x('//*[@id="nc_1__scale_text"]/span');
            let slider = await sliderElement.boundingBox();

            let slideHandle = await page.$x('//*[@id="nc_1_n1z"]');
            let handle = await slideHandle.boundingBox();

            await page.mouse.move(handle.x + handle.width / 2, handle.y + handle.height / 2);
            await page.mouse.down();
            await page.mouse.move(handle.x + slider.width, handle.y + handle.height / 2, { steps: 50 });
            await page.mouse.up();


            await __Set_TOut(5000000);
        }

    }


    process.exit(0);
};

(async () => {
    if (_loginCase === -101) {
        await registerNewAccount();
    }
    else if (_loginCase == -109) {
        await getDetailCookie();
    }
    else if (_loginCase == -110) {
        await loopSubCategory();
    }
})();