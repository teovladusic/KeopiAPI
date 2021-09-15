using Keopi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keopi.Service.PromoCafes
{
    public interface IPromoCafesService
    {
        Task<List<Cafe>> GetAll();
    }
}