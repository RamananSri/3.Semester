using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ModelLayer;

namespace DataAccessLayer
{
    public class UserDB : IUserDB
    {

        public User GetUser(int? id)
        {
            try
            {
                using (Context db = new Context())
                {
                    var user = db.Users.Find(id);
                    return user;
                }
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
                using (Context db = new Context())
                {
                    var user = db.Users
                        .FirstOrDefault(i => i.Email == email);
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyUser(User user)
        {
            using (Context db = new Context())
            {
                var original = db.Users.Find(user.Id);

                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(user);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveUser(int id)
        {
            using (Context db = new Context())
            {
                User user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public List<User> GetAllUser()
        {
            try
            {
                List<User> users;

                using (Context db = new Context())
                {
                    users = db.Users.ToList();                  
                }

                if (users.Equals(null))
                {
                    return null;
                }
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public User LoginUser(string email, string pword)
        {
            try
            {
                using (Context db = new Context())
                {
                    User user = (from u in db.Users
                                 where u.Email.Equals(email) && u.PWord.Equals(pword)
                                 select u).FirstOrDefault();

                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // kan laves på en smartere måde men hvordan?!
        // Methoden tilføjer en User til databasen, og gemmer den ved hjælp af saveChanges.
        // TODO: smartere måde at lave det på.

        public void AddUser(User user)
        {
            using (Context db = new Context())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}