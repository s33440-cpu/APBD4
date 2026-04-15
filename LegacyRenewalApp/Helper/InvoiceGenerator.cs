using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper
{
    public class InvoiceGenerator : IInvoiceGenerator
    {
        public RenewalInvoice GenerateInvoice(
            Customer customer,
            InvoiceDetails paymentDetails,
            string normalizedPlanCode,
            PaymentMethod payment,
            int seatCount)
        {
            return new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customer.Id}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = payment.ToString(),
                SeatCount = seatCount,
                BaseAmount = Math.Round(paymentDetails.BaseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(paymentDetails.DiscountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(paymentDetails.SupportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentDetails.PaymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(paymentDetails.TaxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(paymentDetails.TotalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = paymentDetails.Notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };
        }
    }
}