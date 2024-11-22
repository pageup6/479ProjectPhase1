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
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public IQueryable<RoleModel> Query()
        {
            return  _db.Roles.OrderBy(s => s.Name).Select(s => new RoleModel() { Record = s });
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists");
            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(s => s.Users).SingleOrDefault(s => s.Id == id);
            if (entity == null)
                return Error("Role can't be found");
            if (entity.Users.Any())
                return Error("Role has relational users");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully");
        }


        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists");
            var entity = _db.Roles.SingleOrDefault(s => s.Id == record.Id);
            if (entity == null)
                return Error("that role cant be found");
            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Roles updated successfully");
        }
    }
}
