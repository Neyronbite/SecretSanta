using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Interfaces
{
    public interface IMailService
    {
        string SendMails(int listID);
    }
}
