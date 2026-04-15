using LegacyRenewalApp.Enums;
using LegacyRenewalApp.Interfaces.Calculator;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper.Calculators
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal GetTaxRate(Customer customer)
        {
            return customer.Country switch
            {
                Country.Poland => 0.23m,
                Country.Germany => 0.19m,
                Country.CzechRepublic => 0.21m,
                Country.Norway => 0.25m,
                _ => 0.20m
            };
        }
    }
}