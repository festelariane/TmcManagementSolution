using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Customers;
using Tmc.BLL.Contract.Security;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Enums;

namespace Tmc.BLL.Impl.Customers
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly ICustomerBiz _customerBiz;
        private readonly IEncryptionService _encryptionService;
        public CustomerRegistrationService(ICustomerBiz customerBiz,
            IEncryptionService encryptionService)
        {
            this._customerBiz = customerBiz;
            this._encryptionService = encryptionService;
        }

        public LoginResult ValidateUserForLogin(string userName, string password, out Customer validCustomer)
        {
            validCustomer = null;
            var customer = _customerBiz.GetCustomerByUserNameOrCode(userName);
            if (customer == null)
                return LoginResult.CustomerNotExist;

            string pwd = "";
            pwd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            bool isValid = pwd == customer.Password;

            //save last login date
            if (isValid)
            {
                customer.LastLoginDateUtc = DateTime.UtcNow;
                _customerBiz.UpdateCustomer(customer);
                validCustomer = customer;
                return LoginResult.Successful;
            }
            else
                return LoginResult.WrongPassword;
        }

        public ChangePasswordResult ChangePassword(int customerId, string oldPassword, string newPassword)
        {
            var customer = _customerBiz.GetCustomerById(customerId);
            if(customer == null)
            {
                return ChangePasswordResult.CustomerNotExist;
            }
            string oldPwd = _encryptionService.CreatePasswordHash(oldPassword, customer.PasswordSalt);
           
            bool oldPasswordIsValid = oldPwd == customer.Password;
            if (!oldPasswordIsValid)
            {
                return ChangePasswordResult.OldPasswordNotValid;
            }
            string saltKey = _encryptionService.CreateSaltKey(5);
            customer.PasswordSalt = saltKey;
            customer.Password = _encryptionService.CreatePasswordHash(newPassword, saltKey);
            _customerBiz.UpdateCustomer(customer);

            return ChangePasswordResult.Successful;
        }
    }
}
