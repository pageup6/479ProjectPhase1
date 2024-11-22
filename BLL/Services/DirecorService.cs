using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public class DirecorService
    {
        public class DirectorService : ServiceBase, IService<Director, DirectorModel>
        {
            public DirectorService(Db db) : base(db)
            {

            }

            public ServiceBase Create(Director record)
            {
                if (_db.Directors.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim()))
                    return Error("Movie with same name Exists!");
                record.Name = record.Name?.Trim();
                _db.Directors.Add(record);
                _db.SaveChanges();
                return Success("Director created successfully");
            }

            public ServiceBase Delete(int id)
            {
                var entity = _db.Directors.SingleOrDefault(d => d.Id == id);
                if (entity == null)
                    return Error("Director Can't be found");
                _db.Directors.Remove(entity);
                _db.SaveChanges();
                return Success("Director Deleted Successfully");
            }

            public IQueryable<DirectorModel> Query()
            {
                return _db.Directors.OrderByDescending(d => d.Surname).Select(d => new DirectorModel() { Record = d });
            }

            public ServiceBase Update(Director record)
            {
                if (_db.Directors.Any(d => d.Id != record.Id && d.Name.ToLower() == record.Name.ToLower().Trim()))
                    return Error("Director with same Name Exists!");
                record.Name = record.Name?.Trim();
                _db.Directors.Update(record);
                _db.SaveChanges();
                return Success("Director updated successfully");
            }
        }
    }
}
