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
    public class GenreService : ServiceBase, IService<Genre, GenreModel>
    { 
    
    
        public GenreService(Db db) : base(db)
        {
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(s => s.Name).Select(s => new GenreModel() { Record = s });
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists");
            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(s => s.MovieGenres).SingleOrDefault(s => s.Id == id);
            if (entity == null)
                return Error("Genre can't be found");
            if (entity.MovieGenres.Any())
                return Error("Genre has relational users");
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully");
        }


        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists");
            var entity = _db.Genres.SingleOrDefault(s => s.Id == record.Id);
            if (entity == null)
                return Error("that Genre cant be found");
            entity.Name = record.Name?.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genres updated successfully");
        }
    }
    
}


