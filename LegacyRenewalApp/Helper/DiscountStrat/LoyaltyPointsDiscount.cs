using LegacyRenewalApp.Interfaces.Discount;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.DiscountStrategy
{
    public class LoyaltyPointsDiscount : IDiscountStrategy
    {
        public decimal Apply(DiscountContext context, StringBuilder notes)
        {
            if (context.Customer.LoyaltyPoints <= 0) return 0m;

            int pointsToUse = context.Customer.LoyaltyPoints > 200 ? 200 : context.Customer.LoyaltyPoints;
            notes.Append($"loyalty points used: {pointsToUse}; ");
            return pointsToUse;
        }
    }

}