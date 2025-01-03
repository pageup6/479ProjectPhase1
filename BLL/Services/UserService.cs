using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : ServiceBase, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public ServiceBase Create(User record)
        {
            if (_db.Users.Any(u => u.UserName.ToLower() == record.UserName.ToLower().Trim()))
                return Error("User with same username already exists!");

            // Validate role exists
            if (!_db.Roles.Any(r => r.Id == record.RoleId))
                return Error("Selected role doesn't exist!");

            record.UserName = record.UserName?.Trim();
            // You might want to hash the password before saving
            record.Password = HashPassword(record.Password);

            _db.Users.Add(record);
            _db.SaveChanges();
            return Success("User created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users
                .Include(u => u.Role)
                .SingleOrDefault(u => u.Id == id);

            if (entity == null)
                return Error("User not found");

            _db.Users.Remove(entity);
            _db.SaveChanges();
            return Success($"User '{entity.UserName}' deleted successfully");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users
                .Include(u => u.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new UserModel { Record = u });
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(u => u.Id != record.Id &&
                                  u.UserName.ToLower() == record.UserName.ToLower().Trim()))
                return Error("User with same username already exists!");

            // Validate role exists
            if (!_db.Roles.Any(r => r.Id == record.RoleId))
                return Error("Selected role doesn't exist!");

            var existing = _db.Users.Find(record.Id);
            if (existing == null)
                return Error("User not found");

            existing.UserName = record.UserName?.Trim();
            existing.RoleId = record.RoleId;
            existing.IsActive = record.IsActive;

            // Only update password if a new one is provided
            if (!string.IsNullOrEmpty(record.Password))
            {
                existing.Password = HashPassword(record.Password);
            }

            _db.SaveChanges();
            return Success("User updated successfully");
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            // This is just a placeholder - you should use a proper password hashing algorithm
            // like BCrypt, PBKDF2, or Argon2
            return password; // Don't use this in production!
        }
    }
}
