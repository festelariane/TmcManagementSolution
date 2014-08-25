using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Admin.Models.CardTypes;
using Tmc.Admin.Models.Customers;
using Tmc.Core.Domain.Cards;
using Tmc.Core.Domain.Customers;

namespace Tmc.Admin.Extensions
{
    public static class MappingExtensions
    {
        public static CardTypeModel ToModel(this CardType entity)
        {
            return Mapper.Map<CardType, CardTypeModel>(entity);
        }
        public static CardType ToEntity(this CardTypeModel model)
        {
            return Mapper.Map<CardTypeModel, CardType>(model);
        }
        public static CardType ToEntity(this CardTypeModel model, CardType destination)
        {
            return Mapper.Map(model, destination);
        }

        public static CustomerModel ToModel(this Customer entity)
        {
            return Mapper.Map<Customer, CustomerModel>(entity);
        }
        public static Customer ToEntity(this CustomerModel model)
        {
            return Mapper.Map<CustomerModel, Customer>(model);
        }
        public static Customer ToEntity(this CustomerModel model, Customer destination)
        {
            return Mapper.Map(model, destination);
        }
    }
}