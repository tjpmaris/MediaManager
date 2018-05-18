using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.Models;

namespace MediaManager.Database
{
    public class DAL : IDAL
    {

        MediaContext context;

        public DAL()
        {

        }

        public Picture GetPictureById(int Id)
        {
            using (context = new MediaContext())
            {
                return context.Pictures.Where(s => s.Id == Id).FirstOrDefault();
            }
        }

        //public T GetById<T>(int Id)
        //{
        //    using (context = new MediaContext())
        //    {
        //        return context.Find<T>(Id);
        //    }
        //}

        public List<Picture> GetAllPictures()
        {
            using (context = new MediaContext())
            {
                return context.Pictures.ToList();
            }
        }

        public Picture DeletePicture(int Id)
        {
            throw new NotImplementedException();
        }

        public Picture Update(Picture obj)
        {
            using(context = new MediaContext())
            {
                var foundPicture = context.Pictures.Where(s => s.Id == obj.Id).FirstOrDefault();

                if(foundPicture != null)
                {
                    obj.UserId = foundPicture.UserId;

                    foundPicture.Name = obj.Name;
                    context.SaveChanges();

                    return foundPicture;
                }
                else
                {
                    throw new ArgumentException($"Could not find picture with Id {obj.UserId}");
                }
            }
        }

        public Picture Create(Picture obj)
        {
            using (context = new MediaContext())
            {
                var user = context.Users.Where(s => s.Id == obj.UserId).FirstOrDefault();

                if (user != null)
                {
                    var picture = new Picture() { Name = obj.Name, FilePath = "Some file path.", UserId = user.Id };
                    context.Pictures.Add(picture);
                    context.SaveChanges();
                    return picture;
                }
                else
                {
                    throw new ArgumentException($"Could not find user with Id {obj.UserId}");
                }
            }
        }

        //public Picture UpdateProperty(Picture obj)
        //{
        //    int propCounter = 0;

        //    if (!string.IsNullOrWhiteSpace(obj.Name))
        //    {
        //        ++propCounter;
        //    }

        //    using (context = new MediaContext())
        //    {
        //        return context.Pictures.Where(s => s.Id == Id).FirstOrDefault();
        //    }
        //}
    }
}
