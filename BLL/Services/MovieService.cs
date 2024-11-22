using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
 

    public class MovieService : ServiceBase, IService<Movie, MovieModel>
    {
        public MovieService(Db db) : base(db)
        {

        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(p => p.Name.ToLower() == record.Name.ToLower().Trim() && p.Director == record.Director))
                return Error("Movie with same Director Exists!");
            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("Movie created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.SingleOrDefault(p => p.Id == id);
            if (entity == null)
                return Error("Movie Can't be found");
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie Deleted Successfully");
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.OrderByDescending(p => p.ReleaseDate).Select(p => new MovieModel() { Record = p });
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() && p.Director == record.Director))
                return Error("Movie with same Director Exists!");
            record.Name = record.Name?.Trim();
            _db.Movies.Update(record);
            _db.SaveChanges();
            return Success("Movie updated successfully");
        }
    }
}
