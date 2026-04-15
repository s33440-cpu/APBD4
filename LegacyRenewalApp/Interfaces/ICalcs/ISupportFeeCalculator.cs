using LegacyRenewalApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces.Calculator
{
    public interface ISupportFeeCalculator
    {
        decimal Calculate(PlanCode planCode, bool includePremiumSupport, StringBuilder notes);
    }
}