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
    public interface IProductSaleFactorDal
    {
        Task<long> Insert(ProductSaleFactor entity);
    }

    public class ProductSaleFactorDal : IProductSaleFactorDal
    {
        #region DataMember
        private const string TableName = "[dbo].[ProductSaleFactor]";
        #endregion

        #region Insert
        public async Task<long> Insert(ProductSaleFactor entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@SaleFactorId", entity.SaleFactorId);
            prams.Add("@ProductId", entity.ProductId);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       SaleFactorId
                                      ,ProductId                         
                               )
                               VALUES
                               (
                                       @SaleFactorId
                                      ,@ProductId
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion
    }
}
