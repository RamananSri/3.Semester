using System.Collections.Generic;
using ModelLayer;

namespace DataAccessLayer
{
    public interface IUserDB
    {
        // CREATE 

        void AddUser(User user);

        // READ

        User GetUser(int? id);

        User GetUserByEmail(string email);

        List<User> GetAllUser();

        User LoginUser(string email, string pword);

        // UPDATE

        void ModifyUser(User user);

        // DELETE

        void RemoveUser(int id);
    }
}