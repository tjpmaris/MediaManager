using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaManager.Models;

namespace MediaManager.Database
{
    public class DAL : IDAL
    {

        public T Get<T>(string id) where T : RavenModel
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public List<T> GetAll<T>() where T : RavenModel
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<T>().ToList();
            }
        }

        public T Update<T>(T obj) where T : RavenModel
        {
            using(var session = DocumentStoreHolder.Store.OpenSession())
            {
                var doc = session.Load<T>(obj.Id);

                if(doc != null)
                {
                    doc.Update(obj);
                    session.SaveChanges();
                }

                return doc;
            }
        }

        public T Create<T>(T obj) where T : RavenModel
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                obj.Id = Guid.NewGuid().ToString();

                session.Store(obj, obj.Id.ToString());
                session.SaveChanges();
                
                return obj;
            }
        }

        public T Delete<T>(string id) where T : RavenModel
        {
            using(var session = DocumentStoreHolder.Store.OpenSession())
            {
                var doc = session.Load<T>(id);

                if (doc != null)
                {
                    session.Delete(doc);
                    session.SaveChanges();
                }

                return doc;
            }
        }

        public void Attach(string id, Stream attachment, string contentType)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Advanced.Attachments.Store(id, "Attachment" + GetFileExtension(contentType), attachment, contentType);
                session.SaveChanges();
            }
        }

        public Stream GetAttachment(string id)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var result = session.Advanced.Attachments.Get(id, "Attachment.mp4");

                return result.Stream;
            }
        }

        public User GetUser(string email)
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Users.Where(s => s.Email == email).FirstOrDefault();
            }
        }

        public List<User> GetAllUsers()
        {
            using (MediaContext context = new MediaContext())
            {
                return context.Users.ToList();
            }
        }

        public User UpdateUser(User update)
        {
            using (MediaContext context = new MediaContext())
            {
                context.Update(update);
                context.SaveChanges();
                return update;
            }
        }

        public User CreateUser(User create)
        {
            using (MediaContext context = new MediaContext())
            {
                context.Users.Add(create);
                context.SaveChanges();
                return create;
            }
        }

        public User DeleteUser(string email)
        {
            using (MediaContext context = new MediaContext())
            {
                var delete = context.Users.Where(s => s.Email == email).FirstOrDefault();
                context.Remove(delete);
                context.SaveChanges();
                return delete;
            };
        }

        public void Log(Log log)
        {
            using (MediaContext context = new MediaContext())
            {
                context.Logs.Add(log);
                context.SaveChanges();
            };
        }

        public static string GetFileExtension(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "audio/mpeg":
                    return ".mp3";
                case "video/mp4":
                    return ".mp4";
                default:
                    return null;
            }
        }
    }
}
