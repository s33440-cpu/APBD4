using LegacyRenewalApp.Enums;
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
    public class SegmentDiscount : IDiscountStrategy
    {
        public decimal Apply(DiscountContext context, StringBuilder notes)
        {
            return context.Customer.Segment switch
            {
                CustomerSegment.Silver => Add(context.BaseAmount * 0.05m, "silver discount", notes),
                CustomerSegment.Gold => Add(context.BaseAmount * 0.10m, "gold discount", notes),
                CustomerSegment.Platinum => Add(context.BaseAmount * 0.15m, "platinum discount", notes),
                CustomerSegment.Education when context.Plan.IsEducationEligible => Add(context.BaseAmount * 0.20m, "education discount; ", notes),
                _ => 0m
            };
        }

        private decimal Add(decimal value, string note, StringBuilder notes)
        {
            notes.Append($"{note}; ");
            return value;
        }
    }

}