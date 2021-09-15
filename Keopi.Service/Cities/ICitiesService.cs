using Keopi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Cities
{
    public interface ICitiesService
    {
        public List<City> GetAll();
    }
}
