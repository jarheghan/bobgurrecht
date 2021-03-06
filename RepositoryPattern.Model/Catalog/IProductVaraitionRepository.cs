﻿using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public interface IProductVariationRepository : IRepository<ProductVariation>
    {
        void InsertProductVariation(ProductVariation prdVariation);
        void DeleteProductVariation(int Id);
        void UpdatePrductVariation(ProductVariation prdVariation);
        IEnumerable<ProductVariation> GetAllProductVariation(int productId);
        ProductVariation GetSingeProductVariation(int Id);
        int UpdatePrductVariationWithOutput(ProductVariation prdVariation);
        int InsertProductVariationSpecial(ProductVariation prdVariation);
    }
}
