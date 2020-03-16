using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRP.Aliexpress.Web.Interfaces
{
    public interface IConnectionMapping
    {
        string SetConnection(int productId, int memberUid, string connectionId);

        bool ContainsConnection(string connectionId);

        string RemoveConnection(string connectionId);

        int Count();
    }
}
