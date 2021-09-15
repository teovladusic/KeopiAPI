using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Images
{
    public interface IImagesService
    {
        public string[] GetByCafeId(string cafeId);
    }
}
