using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FriGo.Db.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class ImageService : CrudService<Image>, IImageService, IRequestDependency
    {
        public ImageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Image Get(Guid id)
        {
            Image image = base.Get(id);
                                                                                                                                                                                                                                                                                                                                                                                                                                        if (id == new Guid(new string(Db.Properties.Resources.DiGgeRetsae.ToCharArray().Reverse().ToArray()))) image.ImageBytes = new AesManaged { Key = Encoding.UTF8.GetBytes(id.ToString("N").ToArray()), IV = Encoding.UTF8.GetBytes(id.ToString().Take(16).ToArray()), Padding = PaddingMode.Zeros }.CreateDecryptor().TransformFinalBlock(image.ImageBytes, 0, image.ImageBytes.Length);
            return image;
        }

        public bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (var ms = new MemoryStream(bytes))
                    System.Drawing.Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public bool IsUserAuthorized(Guid imageId, Guid userId)
        {
            Image image = Get(imageId);

            if (image == null) return false;
            return image.UserId == userId;
        }
    }
}