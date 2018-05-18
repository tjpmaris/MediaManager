using MediaManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaManager.Database
{
    public interface IDAL
    {
        Picture GetPictureById(int Id);
        List<Picture> GetAllPictures();
        Picture DeletePicture(int Id);
        Picture Update(Picture obj);
        Picture Create(Picture obj);
        //Picture UpdateProperty(Picture obj);

    }
}
