const __Apache = require('http');
const __Http = require('axios');
const __Chrome = require('puppeteer-core');

let Args = process.argv.slice(2);
var _loginCase = parseInt(Args[0]);
var __loginUrl = Args[1];
var __Mp = Args[2];


var cookie_all;
const get_all_cookie = function (item) {
    cookie_all += item.name + "=" + encodeURI(item.value) + "; ";
};

const __Set_TOut = function (ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
};

const __Server = __Apache.createServer(((req, res) => {
    res.writeHead(200, {'Content-Type': 'text/plain'});
    res.end('sgannon72@gmail.com|freedom2010');
}));

const __Server_2 = __Apache.createServer(((req, res) => {
    res.writeHead(200, {'Content-Type': 'text/plain'});
    res.end('127.0.0.1:8878');
}));

__Server.listen(8044, () =>{
});

__Server_2.listen(8199, () =>{
});

__Http.get('http://127.0.0.1:8044/').then((res) => {
    return res.data;
}).then(() => {
    __Http.get('http://127.0.0.1:8199/').then((res) => {
        return res.data;
    }).then(() => {

        (async () => {
            if(_loginCase === -109){
                await registerNewAccount();
            }
            else if(_loginCase == -107)
            {
                await slideCapcha();
            }
            else if(_loginCase == -101){
                await getDetailCookie();
            }
        })();

    }).catch((error) => {
        console.error(error)
    });
}).catch((error) => {
    console.error(error)
});

let __Width = 1600;
let __Height = 900;

const registerNewAccount = async ()=>
{
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36';
    let __Email = __Mp.split("|")[0].trim();
    let __Password = __Mp.split("|")[1].trim();

    const browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    const page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    await page.goto(__loginUrl);

    const [_tabRegister] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[1]/div/ul/li[1]/div');
    if(_tabRegister){
        await _tabRegister.click();
    }

    const [__Username] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[1]/input');
    if (__Username) {
        await __Username.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(1) > input',__Email, {delay: 20});
  
    const [__Pwd] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[2]/input');
    if (__Pwd) {
        await __Pwd.click();
    }
    await page.type('#expressbuyerlogin > div > div.next-tabs.next-tabs-pure > div.next-tabs-content > div.next-tabs-tabpane.active > div > div > div > div.batman-join > div:nth-child(2) > input', __Password, {delay: 20});

    const [__Signin] = await page.$x('//*[@id="expressbuyerlogin"]/div/div[2]/div[2]/div[1]/div/div/div/div[4]/div[5]/a');
    if (__Signin) {
        await __Signin.click();
    }
    await __Set_TOut(3000);
    
    
    // await page.goto('https://www.aliexpress.com/category/100003415/patches.html');
    // await __Set_TOut(5000);
    
    //get all item in each category
    var rpss = [];
    for(var j = 1; j <= 60; j++)
    {
        var urlItem = "https://www.aliexpress.com/category/100003415/patches.html?trafficChannel=main&catName=patches&CatId=100003415&ltype=wholesale&SortType=default&page="
                        + j +"&isrefine=y";
        await page.goto(urlItem);
        await __Set_TOut(5000);

        var runParams = await page.evaluate(() => {
            return window.runParams.items;
          });
        for (var i in runParams) {
            try{
                var item = runParams[i];
                await page.goto('https:' + item.productDetailUrl);
                await __Set_TOut(2000);
                var windowrunParams = await page.evaluate(() => {
                    return window.runParams === undefined ? "udfi" :  window.runParams;
                });
                if(windowrunParams ===  "udfi"){
                //1. login again
                
                const [__Username_a] = await page.$x('//*[@id="fm-login-id"]');
                if (__Username_a) {
                    console.log("loi login");
                    await __Username_a.click();
                    await page.type('input[id="fm-login-id"]', __Email, {delay: 20});
                    const [__Pwd_b] = await page.$x('//*[@id="fm-login-password"]');
                    await __Pwd_b.click();
                    await page.type('input[id="fm-login-password"]', __Password, {delay: 20});
                    const [__Signin_c] = await page.$x('//*[@id="login-form"]/div[5]/button');
                    await __Signin_c.click();
                }
                else{
                    console.log("loi slide");
                    await page.waitForSelector('.nc_iconfont.btn_slide');
                    let sliderElement = await page.$('.slidetounlock');
                    let slider = await sliderElement.boundingBox();
                
                    let slideHandle = await page.$('.nc_iconfont.btn_slide');
                    let handle = await slideHandle.boundingBox();
                
                    await page.mouse.move(handle.x + handle.width/2, handle.y + handle.height/2);
                    await page.mouse.down();
                    await page.mouse.move(handle.x + slider.width, handle.y + handle.height/2,{steps:50});
                    await page.mouse.up();
                    await __Set_TOut(3000);
                }
                }
                else
                {
                    rpss.push({
                        title: windowrunParams.data.pageModule.title
                      });
                }

            }
            catch(ex){
                console.log("LOI ME GI ROI" +ex);
                await __Set_TOut(80000);
                break;
            }
        }
    }
    console.log(JSON.stringify(rpss));
   process.exit(0);
   //await browser.close();
}
const slideCapcha = async()=>{
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36';
    const browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    const page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    await page.goto(__loginUrl);


    let sliderElement = await page.$x('//*[@id="nc_1__scale_text"]/span');
    let slider = await sliderElement.boundingBox();

    let slideHandle = await page.$x('//*[@id="nc_1_n1z"]');
    let handle = await slideHandle.boundingBox();

    await page.mouse.move(handle.x + handle.width/2, handle.y + handle.height/2);
    await page.mouse.down();
    await page.mouse.move(handle.x + slider.width, handle.y + handle.height/2,{steps:50});
    await page.mouse.up();


    await __Set_TOut(5000000);
};

const getDetailCookie = async ()=>
{
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36';

    const browser = await __Chrome.launch({
        executablePath: './chromium/chrome.exe',
        ignoreDefaultArgs: true,
        args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
        headless: false,
    });
    const page = await browser.newPage();
    await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
    await page.setDefaultNavigationTimeout(0);
    await page.goto(__loginUrl);

    const [__buyNow] = await page.$x('//*[@id="root"]/div/div[2]/div/div[2]/div[11]/span[1]/button');
    if (__buyNow) {
        const cookiesSet = await page.cookies(page.url());
        cookie_all = "";
        cookiesSet.forEach(get_all_cookie);
    
        console.log("DetailCookie|" + cookie_all);
    }
    else
    {
        const [__slider] = await page.$x('//*[@id="nc_1__scale_text"]/span');
        if(__slider){
            //console.log("Slider|");
            let sliderElement = await page.$x('//*[@id="nc_1__scale_text"]/span');
            let slider = await sliderElement.boundingBox();
        
            let slideHandle = await page.$x('//*[@id="nc_1_n1z"]');
            let handle = await slideHandle.boundingBox();
        
            await page.mouse.move(handle.x + handle.width/2, handle.y + handle.height/2);
            await page.mouse.down();
            await page.mouse.move(handle.x + slider.width, handle.y + handle.height/2,{steps:50});
            await page.mouse.up();
        
        
            await __Set_TOut(5000000);
        }
        
    }


    process.exit(0);
};


let __Element_Status_Fail = '#content > div.sb-contentCrate.flex-grow > div > div > div > div > div.sb-contentColumn.mx-auto.sb-contentColumn--narrow.pb5 > form > div.sb-expander > div > div > div > p';
const runThreads = async (__Mp, __Proxy) => {
    let __Email = __Mp.split("|")[0].trim();
    let __Password = __Mp.split("|")[1].trim();

    let __IP_Port = __Proxy.trim();
    let __UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36';

    console.log(__Email + ' ' + __Password + ' ' + __IP_Port + ' ' + __UA);

    try
    {
        const browser = await __Chrome.launch({
            executablePath: './chrome-win/chrome.exe',
            /*executablePath: "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe",*/
            ignoreDefaultArgs: true,
            args: ['--no-sandbox', /*'--proxy-server=' + __IP_Port + '',*/ '--user-data-dir=1', '--user-agent=' + __UA + '', '--disable-background-timer-throttling', '--disable-backgrounding-occluded-windows', '--disable-renderer-backgrounding', '--disable-setuid-sandbox', '--disable-dev-shm-usage', '--disable-gpu'],
            headless: false,
        });
        const page = await browser.newPage();
        await page.setViewport({ width: parseInt(__Width), height: parseInt(__Height) });
        await page.setDefaultNavigationTimeout(0);
        await page.goto('https://www.starbucks.com/account/signin');

        const [__Username] = await page.$x('//*[@id="username"]');
        if (__Username) {
            await __Username.click();
        }
        /*__Email = "elaineymendozag0fzx@yahoo.com";
        __Password = "Abc123!@#";*/
        await page.type('input[id="username"]', __Email, {delay: 20});

        const [__Pwd] = await page.$x('//*[@id="password"]');
        if (__Pwd) {
            await __Pwd.click();
        }
        await page.type('input[id="password"]', __Password, {delay: 20});

        // Click Signin
        const [__Signin] = await page.$x('//*[@id="content"]/div[2]/div/div/div/div/div[1]/form/div[6]/div/span/div/button');
        if (__Signin) {
            await __Signin.click();
        }
        // Set Variable For Stay on Signin Page = False To Check If This URL Stay on Will Be True And If True That Could Be Have Error Text Email Pass False
        // If Reject By FingerPrint Of Starbucks That Could Be True . When It True Just Return All And Stopping Checking .
        let __Reject_FingerPrint = false;
        let __Stayon = false;

        // Promises Will Check Stay on Page Or ReDirect To Reject
        const __Promise_Find_Default = new Promise(function(resolve, reject) {
            setTimeout(function() {
                if (page.url() === 'https://www.starbucks.com/account/signin')
                {
                    resolve("Result-Error");
                }
                else if (page.url() === 'https://app.starbucks.com/' || page.url() === 'https://app.starbucks.com' || page.url() === 'https://starbucks.com/' || page.url() === 'https://starbucks.com')
                {
                    resolve("Reject");
                }
                {
                    reject("Continues");
                }
            }, 10 * 1000);
        });

        await Promise.race([
            page.waitForNavigation({ waitUntil: "networkidle0", timeout: 120000 }),
            page.waitForNavigation({ waitUntil: "networkidle2", timeout: 120000 }),
            __Promise_Find_Default
        ]).then(function(result) {
            if (result === 'Reject')
            {
                __Reject_FingerPrint = true;
            }
            else if (result === 'Result-Error')
            {
                __Stayon = true;
            }
        }, function(err) {
            console.log("-- Error | " + err);
        });


        if(__Reject_FingerPrint === true)
        {
            console.log("-- Reject | FingerPrint");
        }
        else if(__Stayon === true)
        {
            let __Fail_Element_Exist;
            await Promise.race([
                page.waitForSelector(__Element_Status_Fail, { timeout: 60000 })
            ]).then(function(result) {
                if(result == 'JSHandle@node')
                {
                    __Fail_Element_Exist = true;
                    //console.log("-- Exist | " + result);
                }
                else if(result == '[object Object]')
                {
                    __Fail_Element_Exist = false;
                    //console.log("-- Doesn't Exist | " + result);
                }
            }, function(err) {
                console.log("-- Error | " + err);
            });

            if(__Fail_Element_Exist)
            {
                if (await page.$(__Element_Status_Fail)) {
                    let __rs = await page.$eval(__Element_Status_Fail, el => el.textContent.trim());
                    if(__rs === 'The email or password you entered is not valid. Please try again.')
                    {
                        console.log("-- False | " + __rs);
                    }
                    else
                    {
                        console.log("-- Reject | FingerPrint | ?_V2_" + __rs);
                    }
                }
                else
                {
                    console.log("-- HTTP_Proxy | #_Fail_Status_Doesn_t_Exist_?_Why | " + __rs);
                }
            }
            else
            {
                console.log("-- HTTP_Proxy | #_Fail_Status_Doesn_t_Exist | " + __rs);
            }
        }
        else
        {
            console.log("-- ? | #Could Be Success ?_" + page.html());
        }
        await page.waitFor(20000*1000);

        return;
    }
    catch(ex)
    {
        console.log('-- SSH |' + ex);
    }
};
