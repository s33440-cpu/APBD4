using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces.Calculator
{
    public interface IInvoiceCalculator
    {
        InvoiceDetails Calculate(Customer customer,
            SubscriptionPlan plan,
            PaymentMethod paymentMethod,
            int seatCount,
            bool useLoyaltyPoints,
            bool includePremiumSupport);
    }
}