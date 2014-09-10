using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tmc.Web.Framework.Common
{
    public class UTCDateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if(!string.IsNullOrWhiteSpace(value.AttemptedValue))
            {
                try
                {
                    var timeZoneIndex = value.AttemptedValue.IndexOf("(", System.StringComparison.Ordinal);
                    var dateTimeString = value.AttemptedValue.Substring(0, timeZoneIndex - 1);
                    var formattedDate = DateTime.ParseExact(dateTimeString, "ddd MMM dd yyyy HH:mm:ss 'GMT'zzzzz", System.Globalization.CultureInfo.InvariantCulture);
                    return formattedDate;
                }
                catch(Exception ex)
                {
                    try
                    {
                        var dt = DateTime.Parse(value.AttemptedValue);
                    }
                    catch(Exception innerEx)
                    {

                    }
                }
            }
            return null;
        }
    }
}
