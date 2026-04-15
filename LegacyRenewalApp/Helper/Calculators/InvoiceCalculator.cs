using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Helper.Discount;
using LegacyRenewalApp.Helper.DiscountStrategy;
using LegacyRenewalApp.Interfaces.Calculator;
using LegacyRenewalApp.Interfaces.Discount;
using LegacyRenewalApp.Models;
using LegacyRenewalApp.Helper.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper.Calculator
{
    public class InvoiceCalculator : IInvoiceCalculator
    {
        private readonly IEnumerable<IDiscountStrategy> _discounts;
        private readonly IPaymentFeeCalculator _paymentFee;
        private readonly ITaxCalculator _tax;
        private readonly ISupportFeeCalculator _support;

        public InvoiceCalculator(
            IEnumerable<IDiscountStrategy> discounts,
            IPaymentFeeCalculator paymentFee,
            ITaxCalculator tax,
            ISupportFeeCalculator support)
        {
            _discounts = discounts;
            _paymentFee = paymentFee;
            _tax = tax;
            _support = support;
        }
        public InvoiceCalculator() : this(
            new List<IDiscountStrategy>()
            {
                    new LoyaltyPointsDiscount(),
                    new LoyaltyDiscount(),
                    new SegmentDiscount(),
                    new TeamSizeDiscount(),
            },
            new PaymentFeeCalculator(),
            new TaxCalculator(),
            new SupportFeeCalculator())
        { }

        public InvoiceDetails Calculate(Customer customer,
                                SubscriptionPlan plan,
                                PaymentMethod paymentMethod,
                                int seatCount,
                                bool useLoyaltyPoints,
                                bool includePremiumSupport)
        {
            StringBuilder notes = new StringBuilder();

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;

            decimal discountAmount = _discounts.Sum(d =>
                d.Apply(new DiscountContext(customer, plan, seatCount, baseAmount, useLoyaltyPoints), notes));

            decimal subtotalAfterDiscount = calculateSubtotalAfterDiscount(baseAmount, discountAmount, notes);

            decimal supportFee = _support.Calculate(plan.Code, includePremiumSupport, notes);

            decimal paymentFee = _paymentFee.Calculate(paymentMethod, subtotalAfterDiscount, supportFee, notes);

            decimal taxRate = _tax.GetTaxRate(customer);

            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes.Append("minimum invoice amount applied; ");
            }


            return new InvoiceDetails(baseAmount, discountAmount, supportFee, paymentFee, taxAmount, finalAmount, notes.ToString());
        }

        private decimal calculateSubtotalAfterDiscount(decimal baseAmount, decimal discountAmount, StringBuilder notes)
        {
            decimal subtotalAfterDiscount = baseAmount - discountAmount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                notes.Append("minimum discounted subtotal applied; ");
            }
            return subtotalAfterDiscount;
        }     
    }
}