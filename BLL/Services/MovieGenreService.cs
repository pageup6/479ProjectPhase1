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
    public class MovieGenreService : ServiceBase, IService<MovieGenre, MovieGenreModel>
    {
        public MovieGenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(MovieGenre record)
        {
            // Check if movie exists
            if (!_db.Movies.Any(m => m.Id == record.MovieId))
                return Error("Movie doesn't exist!");

            // Check if genre exists
            if (!_db.Genres.Any(g => g.Id == record.GenreId))
                return Error("Genre doesn't exist!");

            // Check for duplicate relationship
            if (_db.MovieGenres.Any(mg => mg.MovieId == record.MovieId && mg.GenreId == record.GenreId))
                return Error("This Movie already has this Genre assigned!");

            _db.MovieGenres.Add(record);
            _db.SaveChanges();
            return Success("Movie-Genre relationship created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.MovieGenres
                .Include(mg => mg.Movie)
                .Include(mg => mg.Genre)
                .SingleOrDefault(mg => mg.Id == id);

            if (entity == null)
                return Error("Movie-Genre relationship not found");

            _db.MovieGenres.Remove(entity);
            _db.SaveChanges();
            return Success($"Genre '{entity.Genre.Name}' removed from movie '{entity.Movie.Name}'");
        }

        public IQueryable<MovieGenreModel> Query()
        {
            return _db.MovieGenres
                .Include(mg => mg.Movie)
                .Include(mg => mg.Genre)
                .OrderBy(mg => mg.Movie.Name)
                .ThenBy(mg => mg.Genre.Name)
                .Select(mg => new MovieGenreModel { Record = mg });
        }

        public ServiceBase Update(MovieGenre record)
        {
            // Check if movie exists
            if (!_db.Movies.Any(m => m.Id == record.MovieId))
                return Error("Movie doesn't exist!");

            // Check if genre exists
            if (!_db.Genres.Any(g => g.Id == record.GenreId))
                return Error("Genre doesn't exist!");

            // Check for duplicate while excluding current record
            if (_db.MovieGenres.Any(mg =>
                mg.Id != record.Id &&
                mg.MovieId == record.MovieId &&
                mg.GenreId == record.GenreId))
                return Error("This Movie already has this Genre assigned!");

            var existing = _db.MovieGenres.Find(record.Id);
            if (existing == null)
                return Error("Movie-Genre relationship not found");

            existing.MovieId = record.MovieId;
            existing.GenreId = record.GenreId;

            _db.SaveChanges();
            return Success("Movie-Genre relationship updated successfully");
        }
    }
}
