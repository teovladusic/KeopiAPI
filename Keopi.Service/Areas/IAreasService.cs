using Keopi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Areas
{
    public interface IAreasService
    {
        public List<Area> GetByCityId(string cityId);
    }
}
