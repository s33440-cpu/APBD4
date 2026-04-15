using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Interfaces.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.Calculators
{
    public class SupportFeeCalculator : ISupportFeeCalculator
    {
        public decimal Calculate(PlanCode planCode, bool includePremiumSupport, StringBuilder notes)
        {
            if (!includePremiumSupport) return 0m;

            decimal fee = planCode switch
            {
                PlanCode.Start => 250m,
                PlanCode.Pro => 400m,
                PlanCode.Enterprise => 700m,
                _ => throw new NotImplementedException()
            };

            notes.Append("premium support included; ");
            return fee;
        }
    }
}