using MediaManager.Models;
using System.Collections.Generic;
using System.IO;

namespace MediaManager.Database
{
    public interface IDAL
    {
        T Get<T>(string id) where T : RavenModel;
        List<T> GetAll<T>() where T : RavenModel;
        T Update<T>(T obj) where T : RavenModel;
        T Create<T>(T obj) where T : RavenModel;
        T Delete<T>(string id) where T : RavenModel;
        void Attach(string id, Stream attachment, string extension);
        Stream GetAttachment(string id);

        User GetUser(string email);
        User UpdateUser(User update);
        User CreateUser(User create);
        User DeleteUser(string email);
        List<User> GetAllUsers();

        void Log(Log log);
    }
}
