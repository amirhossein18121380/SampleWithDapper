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
    public interface ISaleFactorDal
    {
        Task<long> Insert(SaleFactor entity);
        Task<int> Update(SaleFactor ti);
    }

    public class SaleFactorDal : ISaleFactorDal
    {
        #region DataMember
        private const string TableName = "[dbo].[SaleFactor]";
        #endregion

        #region Insert
        public async Task<long> Insert(SaleFactor entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@TotalCost", entity.TotalCost);
            prams.Add("@ReceiptNumber", entity.ReceiptNumber);
            prams.Add("@ReceiptDate", entity.ReceiptDate);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       TotalCost
                                      ,ReceiptNumber                         
                                      ,ReceiptDate
                               )
                               VALUES
                               (
                                       @TotalCost
                                      ,@ReceiptNumber
                                      ,@ReceiptDate
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion

        public async Task<int> Update(SaleFactor ti)
        {
            using var db = new DbEntity().GetConnectionString();
            var query = $@"update {TableName} " +
                        "set " +
                        "ReceiptNumber=@ReceiptNumber " +
                        "WHERE Id = @Id";

            var parameter = new DynamicParameters();
            parameter.Add("ReceiptNumber", ti.ReceiptNumber);
            parameter.Add("Id", ti.Id);

            var result = await db.ExecuteAsync(query, parameter);

            return result;
        }
    }
}
