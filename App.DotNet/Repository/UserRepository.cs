using App.DotNet.Data;
using App.DotNet.Models;
using App.DotNet.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DotNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteUser(int UserId)
        {
            var User = _dbContext.Users.Find(UserId);
            _dbContext.Users.Remove(User);
            Save();
        }

        public User GetUserByID(int UserId)
        {
            return _dbContext.Users.Find(UserId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void InsertUser(User User)
        {
            _dbContext.Add(User);
            Save();
        }

        public void UpdateUser(User User)
        {
            _dbContext.Entry(User).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
