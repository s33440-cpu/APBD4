using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Models
{
    public record InvoiceDetails(
        decimal BaseAmount,
        decimal DiscountAmount,
        decimal SupportFee,
        decimal PaymentFee,
        decimal TaxAmount,
        decimal TotalAmount,
        string Notes)
    { }
}