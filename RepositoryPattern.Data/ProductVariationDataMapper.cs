using RepositoryPattern.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RepositoryPattern.Data
{
    public class ProductVariationDataMapper : AbstractDataMapper<ProductVariation>, IProductVariationRepository
    {
        protected override string TableName
        {
            get { return "ProductionVariation"; }
        }

        public override ProductVariation Map(dynamic result)
        {
            var productVariation = new ProductVariation()
            {
                ID = result.prv_id,
                ProductID = result.prv_prd_id,
                Description = result.prv_description,
                Size = result.prv_size,
                Type = result.prv_type,
                Color = result.prv_color,
                AddUser = result.prv_add_user,
                AddDate = result.prv_add_date,
                ChangeDate = result.prv_change_date,
                ChangeUser = result.prv_change_user,
                DeleteFlag = result.prv_delete_flag
            };
            return productVariation;
        }

        public void InsertProductVariation(ProductVariation prdVariation)
        {
            var param = new
            {
                ProductID = prdVariation.ProductID,
                Description = prdVariation.Description,
                Size = prdVariation.Size,
                Type = prdVariation.Type,
                Color = prdVariation.Color,
                AddUser = prdVariation.AddUser,
                AddDate = prdVariation.AddDate,
                DeleteFlag = prdVariation.DeleteFlag
            };

            using (IDbConnection cn = Connection)
            {
                var i = cn.Execute(@"INSERT INTO ProductVariation
                                   (prv_prd_id
                                   ,prv_description
                                   ,prv_size
                                   ,prv_type
                                   ,prv_color
                                   ,prv_add_user
                                   ,prv_add_date
                                   ,prv_delete_flag)
                             VALUES
                                   (@ProductID
                                   ,@Description
                                   ,@Size
                                   ,@Type
                                   ,@Color
                                   ,@AddUser
                                   ,@AddDate
                                   ,@DeleteFlag)", param);
            }

        }

        public void DeleteProductVariation(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrductVariation(ProductVariation prdVariation)
        {
            var param = new
            {
                ID = prdVariation.ID,
                Description = prdVariation.Description,
                Size = prdVariation.Size,
                Type = prdVariation.Type,
                Color = prdVariation.Color,
                ChangeUser = prdVariation.ChangeUser,
                ChangeDate = prdVariation.ChangeDate,
                DeleteFlag = prdVariation.DeleteFlag
            };
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Execute(@"Update ProductVariation
                                    set prv_description = @Description
                                   ,prv_size = @Size
                                   ,prv_type = @Type
                                   ,prv_color = @Color 
                                   ,prv_change_user = @ChangeUser
                                   ,prv_change_date = @ChangeDate
                                   ,prv_delete_flag = @DeleteFlag where prv_id = @ID
                             ", param);
                }

                catch { }

               
            }

        }

        public IEnumerable<ProductVariation> GetAllProductVariation(int productId)
        {
            using (IDbConnection cn = Connection)
            {
                List<ProductVariation> prdvar = new List<ProductVariation>();
                var prdvariation = cn.Query<dynamic>(@"select * from ProductVariation where prv_prd_id = @productId", new {productId = productId });

                foreach (var pv in prdvariation)
                {
                    prdvar.Add(Map(pv));
                }

                return prdvar.AsEnumerable(); ;
            }
        }

        public void Add(ProductVariation item)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductVariation item)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductVariation item)
        {
            throw new NotImplementedException();
        }
    }
}
