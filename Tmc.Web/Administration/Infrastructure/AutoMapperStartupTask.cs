using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tmc.Admin.Models.CardTypes;
using Tmc.Core.Domain.Cards;
using Tmc.Core.Infrastructure;
using Tmc.Admin.Models.Customers;
using Tmc.Core.Domain.Customers;

namespace Tmc.Admin.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<CardType, CardTypeModel>();
            Mapper.CreateMap<CardTypeModel, CardType>();

            Mapper.CreateMap<Customer, CustomerModel>();
            Mapper.CreateMap<CustomerModel, Customer>();
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }
    }
}