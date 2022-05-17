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
    public interface IProductReceiptDal
    {
        Task<long> Insert(ProductReceipt entity);
    }

    public class ProductReceiptDal : IProductReceiptDal
    {
        #region DataMember
        private const string TableName = "[dbo].[ProductReceipt]";
        #endregion

        #region Insert
        public async Task<long> Insert(ProductReceipt entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@ReceiptId", entity.ReceiptId);
            prams.Add("@ProductId", entity.ProductId);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       ReceiptId
                                      ,ProductId                         
                               )
                               VALUES
                               (
                                       @ReceiptId
                                      ,@ProductId
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion
    }
}
