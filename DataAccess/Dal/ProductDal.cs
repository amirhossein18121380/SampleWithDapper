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
    public interface IProductDal
    {
        Task<long> Insert(Product entity);
        Task<int> Update(Product ti);
        Task<Product?> GetById(long id);
    }

    public class ProductDal : IProductDal
    {
        #region DataMember
        private const string TableName = "[dbo].[Product]";
        #endregion

        #region Insert
        public async Task<long> Insert(Product entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@Name", entity.Name);
            prams.Add("@Price", entity.Price);
            prams.Add("@Code", entity.Code);
            prams.Add("@Description", entity.Description);
            prams.Add("@Createon", entity.Createon);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       Name
                                      ,Price
                                      ,Code
                                      ,Description
                                      ,Createon
                               )
                               VALUES
                               (
                                       @Name
                                      ,@Price
                                      ,@Code
                                      ,@Description
                                      ,@Createon
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion

        public async Task<Product?> GetById(long id)
        {
            using var db = new DbEntity().GetConnectionString();
            var result = await db.QueryAsync<Product>($@"Select * From {TableName} WHERE Id=@id ", new { id });
            return result.SingleOrDefault();
        }

        public async Task<int> Update(Product ti)
        {
            using var db = new DbEntity().GetConnectionString();
            var query = $@"update {TableName} " +
                        "set " +
                        "Name=@Name, " +
                        "Price=@Price, " +
                        "Code=@Code, " +
                        "Description=@Description, " +
                        "Createon=@Createon " +
                        "WHERE Id = @Id";

            var parameter = new DynamicParameters();
            parameter.Add("Name", ti.Name);
            parameter.Add("Price", ti.Price);
            parameter.Add("Code", ti.Code);
            parameter.Add("Description", ti.Description);
            parameter.Add("Createon", ti.Createon);
            parameter.Add("Id", ti.Id);

            var result = await db.ExecuteAsync(query, parameter);

            return result;
        }
    }
}
