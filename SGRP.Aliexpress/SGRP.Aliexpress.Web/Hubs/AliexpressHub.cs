using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SGRP.Aliexpress.Helper;
using SGRP.Aliexpress.Web.Interfaces;
using StackExchange.Redis;

namespace SGRP.Aliexpress.Web.Hubs
{
    public class AliexpressHub : Hub
    {

        public Task UpdateTotalCounter()
        {
            if (Clients != null)
            {
                var connection = RedisConnectionFactory.GetConnection();

                connection.GetSubscriber().Subscribe("redis::totalCounter", (c, v) =>
                {
                    Clients.All.SendAsync("ReceiveMessage","*" ,v);
                });

                connection.GetSubscriber().Subscribe("redis::singleCount-1" , (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 1 + "|" + v);
                });

                connection.GetSubscriber().Subscribe("redis::singleCount-2", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 2 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-3", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 3 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-4", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 4 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-5", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 5 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-6", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 6 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-7", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 7 + "|" + v);
                });
                connection.GetSubscriber().Subscribe("redis::singleCount-8", (c, v) =>
                {

                    Clients.All.SendAsync("SingleCounterMessage", "*", 8 + "|" + v);
                });
            }
            return Task.CompletedTask;
            
        }
    }
}
