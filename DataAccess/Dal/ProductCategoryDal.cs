using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Tools;
using DataModel.Models;

namespace DataAccess.Dal
{
    public interface IProductCategoryDal
    {
        Task<long> Insert(ProductCategory entity);
    }

    public class ProductCategoryDal : IProductCategoryDal
    {
        #region DataMember
        private const string TableName = "[dbo].[ProductCategory]";
        #endregion

        #region Insert
        public async Task<long> Insert(ProductCategory entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@CategoryId", entity.CategoryId);
            prams.Add("@ProductId", entity.ProductId);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       CategoryId
                                      ,ProductId                         
                      
                               )
                               VALUES
                               (
                                       @CategoryId
                                      ,@ProductId
                                
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion
    }
}
