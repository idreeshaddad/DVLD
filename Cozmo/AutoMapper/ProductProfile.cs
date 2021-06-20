using AutoMapper;
using Cozmo.Models.Product;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozmo.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Product, ProductCreateEditVM>().ReverseMap();
        }
    }
}
