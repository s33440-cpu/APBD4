using LegacyRenewalApp.Helper.DiscountStrategy;
using LegacyRenewalApp.Interfaces.Discount;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.Discount
{
    public class LoyaltyDiscount : IDiscountStrategy
    {
        public decimal Apply(DiscountContext context, StringBuilder notes)
        {
            if (context.Customer.YearsWithCompany >= 5)
                return Add(context.BaseAmount * 0.07m, "long-term loyalty discount", notes);

            if (context.Customer.YearsWithCompany >= 2)
                return Add(context.BaseAmount * 0.03m, "basic loyalty discount", notes);

            return 0m;
        }

        private decimal Add(decimal value, string note, StringBuilder notes)
        {
            notes.Append($"{note}; ");
            return value;
        }
    }
}