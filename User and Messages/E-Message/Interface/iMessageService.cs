using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Message.Interfece
{
    public interface iMessageService
    {
        void DisplayAllMessage(string connectionString);
        void CreateMessage(string connectionString);
        void DeleteMessage(string connectionString);
        void DeleteAllMessages(string connectionString);
    }
}
