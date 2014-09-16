using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Media
{
    public interface IPictureRepository : IRepository<Picture>
    {
        IEnumerable<Picture> GetAllPicture();
        Picture GetPictureById(int Id);
        Picture Create();
        int Insert(Picture item);
        int Remove(int Id);
    }
}
