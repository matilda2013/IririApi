using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Inteface.IService
{
    public interface IEmailService
    {
        long AddEmaillog(string emailMsg, string receipientEmail, string subject);

    }
}
