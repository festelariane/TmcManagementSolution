using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tmc.Web.Framework.Models
{
    public class BaseModel
    {
        public BaseModel()
        {

        }
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }
    }
    public class BaseEntityModel : BaseModel
    {
        public BaseEntityModel():base()
        {

        }
        public virtual int Id { get; set; }
    }
}
