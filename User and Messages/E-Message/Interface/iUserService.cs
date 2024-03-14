
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Message.Interfece
{
    public interface iUserService
    {
        void DisplayAllUser(string connectionString);
        void DisplayMessageInUser(string connectionString);
        void CreateUser(string connectionString);
        void DeleteUser(string connectionString);
        void DeleteAllUsers(string connectionString);
    }
}
