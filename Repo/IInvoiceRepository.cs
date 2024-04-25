using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backendtask.Model;

namespace backendtask.Repositories
{
    public interface IInvoiceRepository
    {
        void CreateInvoice(Invoice invoice);
        // You can define other methods for invoice management here
    }
}
