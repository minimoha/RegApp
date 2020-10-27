using App.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DotNet.Repository.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}
