using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public interface IRepository<TEntity , TKey>
        where TEntity : class , IEntity<TKey>
    {
        void Add(TEntity entityToAddd);
      
        void Remove(TKey id);
        void Remove(TEntity entityToDelete);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Edit(TEntity update);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        IList<TEntity> GetAll();
        TEntity GetById(TKey id);


        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeChild ="");


        (IList<TEntity> data, int total, int totalDisplay) Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeChild = "", int pageIndex =1 , int pageSize =10, bool isTrackingOF =false );



        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeChild = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOF = false);




        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string oderby = null, bool isTrackingOF = false);




        IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string oderby = null, bool isTrackingOF = false);
        
        
    }
}
