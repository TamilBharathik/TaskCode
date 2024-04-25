using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtask.Model;

namespace backendtask.Repo
{
    public interface IAddInvoice
    {
         public Task<Addinvoice> Create(Addinvoice Entry);
    }
}