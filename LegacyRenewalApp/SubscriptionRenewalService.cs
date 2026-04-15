using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Helper;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Interfaces.Calculator;
using LegacyRenewalApp.Interfaces.Repository;
using LegacyRenewalApp.Models;
using LegacyRenewalApp.Repository;
using System;
using System.ComponentModel.DataAnnotations;


namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionPlanRepository _planRepository;
        private readonly IRenewalServiceValidator _validator;

        private readonly IBillingGateway _billingGateway;
        private readonly IInvoiceGenerator _invoiceGenerator;
        private readonly IInvoiceCalculator _calculator;

        public SubscriptionRenewalService()
            : this(
                new CustomerRepository(), 
                new SubscriptionPlanRepository(), 
                new RenewalServiceValidator(),
                new BillingGateway(),
                new InvoiceGenerator())
            { }

        public SubscriptionRenewalService(
            ICustomerRepository customerRepository,
            ISubscriptionPlanRepository planRepository,
            IRenewalServiceValidator validator,
            IBillingGateway billingGateway,
            IInvoiceGenerator invoiceGenerator)
        {
            _customerRepository = customerRepository;
            _planRepository = planRepository;
            _validator = validator;
            _billingGateway = billingGateway;
            _invoiceGenerator = invoiceGenerator;
        }
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {

            _validator.Validate(customerId, planCode, seatCount, paymentMethod);
            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            PaymentMethod paymentMethodEnum = ParseEnum<PaymentMethod>(normalizedPaymentMethod, nameof(paymentMethod));

            var customer = _customerRepository.GetById(customerId);
            var plan = _planRepository.GetByCode(normalizedPlanCode);

             InvoiceDetails details = _calculator.Calculate(customer, plan,
                                                            paymentMethodEnum,
                                                            seatCount, useLoyaltyPoints,
                                                            includePremiumSupport);

            var invoice = _invoiceGenerator.GenerateInvoice(customer, details, normalizedPlanCode, paymentMethodEnum, seatCount);

            _billingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                _billingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
            private T ParseEnum<T>(string value, string paramName) where T : struct, Enum
        {
            if (!Enum.TryParse<T>(value, true, out var result))
            {
                throw new ArgumentException($"Invalid {paramName}: {value}");
            }
            return result;
        }
        
    }
}
