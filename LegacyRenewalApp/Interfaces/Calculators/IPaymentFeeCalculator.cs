using LegacyRenewalApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces.Calculator
{
    public interface IPaymentFeeCalculator
    {
        decimal Calculate(PaymentMethod paymentMethod, decimal subtotal, decimal supportFee, StringBuilder notes);
    }
}