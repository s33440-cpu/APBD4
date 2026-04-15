using LegacyRenewalApp.Helper.DiscountStrategy;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces.Discount
{
    public interface IDiscountStrategy
    {
        decimal Apply(DiscountContext context, StringBuilder notes);
    }
}