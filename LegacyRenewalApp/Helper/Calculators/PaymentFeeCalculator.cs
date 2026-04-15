using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Interfaces.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.Calculators
{
    public class PaymentFeeCalculator : IPaymentFeeCalculator
    {
        public decimal Calculate(PaymentMethod method, decimal subtotal, decimal supportFee, StringBuilder notes)
        {
            decimal baseAmount = subtotal + supportFee;

            return method switch
            {
                PaymentMethod.Card => Add(baseAmount * 0.02m, "card payment fee", notes),
                PaymentMethod.BankTransfer => Add(baseAmount * 0.01m, "bank transfer fee", notes),
                PaymentMethod.PayPal => Add(baseAmount * 0.035m, "paypal fee; ", notes),
                PaymentMethod.Invoice => Add(0m, "invoice payment", notes),
                _ => throw new NotImplementedException(),
            };
        }

        private decimal Add(decimal value, string note, StringBuilder notes)
        {
            notes.Append($"{note}; ");
            return value;
        }
    }

}