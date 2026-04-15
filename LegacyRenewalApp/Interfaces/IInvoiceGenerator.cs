using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces
{
    public interface IInvoiceGenerator
    {
        public RenewalInvoice GenerateInvoice(
             Customer customer,
             InvoiceDetails paymentDetails,
             string normalizedPlanCode,
             PaymentMethod payment,
             int seatCount);
    }
}