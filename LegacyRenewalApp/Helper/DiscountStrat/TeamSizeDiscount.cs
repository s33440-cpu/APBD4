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
    public class TeamSizeDiscount : IDiscountStrategy
    {
        public decimal Apply(DiscountContext context, StringBuilder notes)
        {
            if (context.SeatCount >= 50)
                return Add(context.BaseAmount * 0.12m, "large team discount", notes);

            if (context.SeatCount >= 20)
                return Add(context.BaseAmount * 0.08m, "medium team discount", notes);

            if (context.SeatCount >= 10)
                return Add(context.BaseAmount * 0.04m, "small team discount", notes);

            return 0m;
        }

        private decimal Add(decimal value, string note, StringBuilder notes)
        {
            notes.Append($"{note}; ");
            return value;
        }
    }
}