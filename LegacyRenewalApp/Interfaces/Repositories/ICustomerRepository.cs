using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Interfaces
{
    public interface ICustomerRepository 
    {
        Customer GetById(int customerId);
    }
}
