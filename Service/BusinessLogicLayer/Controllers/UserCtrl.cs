using System;
using ModelLayer;
using DataAccessLayer;
using DataAccessLayer.Modules;

namespace BusinessLogicLayer
{
    public class UserCtrl
    {
        private IUserDB userDb;
        private CryptoModule crypto;

        public UserCtrl()
        {
            userDb = new UserDB();
            crypto = new CryptoModule();
        }


        public void AddUser(string email, string pword, string name, string phone, string address, string zipcode, string age)
        {
            var salt = crypto.GenerateSaltString();
            User user = new User
            {
                Email = email,
                Salt = salt,
                PWord = crypto.HashPassword(pword, salt),
                Name = name,
                Phone = phone,
                Address = address,
                Zipcode = zipcode,
                Age = age
            };

            userDb.AddUser(user);
        }

        public void ModifyUser(int id, string email, string name, string phone, string address, string zipcode, string age)
        {
            User user = userDb.GetUser(id);

            user.Email = email;
            user.Name = name;
            user.Phone = phone;
            user.Address = address;
            user.Zipcode = zipcode;
            user.Age = age;

            userDb.ModifyUser(user);
        }

        public void RemoveUser(int id)
        {
            userDb.RemoveUser(id);
        }

        public User LoginUser(string email, string pword)
        {
            var user = userDb.GetUserByEmail(email);
            
            string hashedPass = crypto.HashPassword(pword, user.Salt);
            try
            {
                if (hashedPass == user.PWord)
                {
                    return userDb.LoginUser(email, hashedPass);

                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User GetUser(int id)
        {
            try
            {
                User user = userDb.GetUser(id);
                return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                User user = userDb.GetUserByEmail(email);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
