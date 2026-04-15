using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.DiscountStrategy
{
    public record DiscountContext(
    Customer Customer,
    SubscriptionPlan Plan,
    int SeatCount,
    decimal BaseAmount,
    bool UseLoyaltyPoints);
}