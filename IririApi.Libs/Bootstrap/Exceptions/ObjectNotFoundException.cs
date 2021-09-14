using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Bootstrap.Exceptions
{
    public class ObjectNotFoundException  : AppException
    {
        public ObjectNotFoundException(string message, string friendlyMessage = null)
            : base(message, friendlyMessage)
        {
        }
    }
}
