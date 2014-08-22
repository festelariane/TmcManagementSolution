using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Web.Framework.Models
{
    public class BaseModel
    {
        public BaseModel()
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
