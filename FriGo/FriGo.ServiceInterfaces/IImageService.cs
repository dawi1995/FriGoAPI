using System;
using System.Collections.Generic;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IImageService 
    {
        IEnumerable<Image> Get();
        Image Get(Guid id);
        void Add(Image image);
        void Edit(Image image);
        void Delete(Guid id);
        bool IsValidImage(byte[] bytes);
    }
}