using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Models;

namespace MediaManager.Database
{
    public class DAL : IDAL
    {

        public T Get<T>(int Id) where T : class
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Find<T>(Id);
            }
        }

        public T Update<T>(T obj) where T : DBModel
        {
            using (MediaContext context = new MediaContext())
            {
                using (context.Database.BeginTransaction())
                {
                    var item = Get<T>(obj.Id);

                    if (item != null)
                    {
                        item.Update(obj);
                        context.Update(item);
                        context.SaveChanges();

                        context.Database.CommitTransaction();

                        return item;
                    }
                    else
                    {
                        throw new ArgumentException($"Could not find {typeof(T).FullName} with Id {obj.Id}");
                    }
                }
            }
        }

        public T Create<T>(T obj) where T : class
        {
            using (MediaContext context = new MediaContext())
            {
                using (context.Database.BeginTransaction())
                {
                    context.Add<T>(obj);
                    context.SaveChanges();

                    context.Database.CommitTransaction();

                    return obj;
                }
            }
        }

        public T Delete<T>(int Id) where T : class
        {
            using (MediaContext context = new MediaContext())
            { 
                using (context.Database.BeginTransaction())
                {
                    var item = Get<T>(Id);

                    context.Remove(item);
                    context.SaveChanges();

                    context.Database.CommitTransaction();

                    return item;
                }
            }
        }

        public List<Picture> GetAllPictures()
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Pictures.ToList();
            }
        }

        public List<Song> GetAllSongs()
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Songs.ToList();
            }
        }

        public List<Video> GetAllVideos()
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Videos.ToList();
            }
        }

        public List<User> GetAllUsers()
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
