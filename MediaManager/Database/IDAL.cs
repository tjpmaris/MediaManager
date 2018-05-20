using MediaManager.Models;
using System.Collections.Generic;

namespace MediaManager.Database
{
    public interface IDAL
    {
        T Get<T>(int Id) where T : class;
        T Update<T>(T obj) where T : DBModel;
        T Create<T>(T obj) where T : class;
        T Delete<T>(int Id) where T : class;

        List<Picture> GetAllPictures();
        List<Song> GetAllSongs();
        List<Video> GetAllVideos();
        List<User> GetAllUsers();
    }
}
