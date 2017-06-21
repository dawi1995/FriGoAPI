using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Db.EntityConfigurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
        }
    }
}