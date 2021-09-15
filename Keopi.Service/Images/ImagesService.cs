using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Images
{
    public class ImagesService : IImagesService
    {
        private readonly IGenericRepository<ImagesDbModel> _repository;
        public ImagesService(IGenericRepository<ImagesDbModel> repository)
        {
            _repository = repository;
        }

        public string[] GetByCafeId(string cafeId)
        {
            var imagesDbModel = _repository.FilterBy(x => x.CafeId == cafeId).FirstOrDefault();

            if (imagesDbModel is null)
            {
                return Array.Empty<string>();
            }

            return imagesDbModel.Urls;
        }
    }
}
