using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Enums;

namespace Tmc.BLL.Contract.Customers
{
    public partial interface ICustomerRegistrationService
    {

        LoginResult ValidateUserForLogin(string userName, string password, out Customer customer);

        ChangePasswordResult ChangePassword(int customerId, string oldPassword, string newPassword);
    }
}
