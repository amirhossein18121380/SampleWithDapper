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
    public interface ICategoryDal
    {
        Task<long> Insert(Category entity);
    }

    public class CategoryDal : ICategoryDal
    {
        #region DataMember
        private const string TableName = "[dbo].[Category]";
        #endregion

        #region Insert
        public async Task<long> Insert(Category entity)
        {
            using var db = new DbEntity().GetConnectionString();

            var prams = new DynamicParameters();
            prams.Add("@ParentId", entity.ParentId);
            prams.Add("@Title", entity.Title);
            prams.Add("@Createon", entity.Createon);

            var entityId = (await db.QueryAsync<long>(
                $@"INSERT INTO {TableName} 
                               (
                                       ParentId
                                      ,Title                         
                                      ,Createon
                               )
                               VALUES
                               (
                                       @ParentId
                                      ,@Title
                                      ,@Createon
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

            return entityId;
        }
        #endregion
    }
}
