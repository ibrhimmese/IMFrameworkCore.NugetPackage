using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IMFrameworkCore;

public interface IRepository<TEntity, TEntityId> : IQuery<TEntity>
where TEntity : Entity<TEntityId>
{


    ////////////////////////////////////// GET ASYNC ///////////////////////////////////////////
    Task<TEntity?> GetAsync(
           Expression<Func<TEntity, bool>> predicate,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default
       );




    ////////////////////////////////////// GETLİST ASYNC ///////////////////////////////////////////
    Task<IPaginate<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );




    ////////////////////////////////////// GETLİSTBYDYNAMİC ASYNC ///////////////////////////////////////////

    Task<IPaginate<TEntity>> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );





    ////////////////////////////////////// ANY ASYNC ///////////////////////////////////////////
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        CancellationToken cancellationToken = default
    );


    ////////////////////////////////////// ADD ASYNC ///////////////////////////////////////////
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);


    ////////////////////////////////////// ADDRANGE ASYNC ///////////////////////////////////////////
    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);


    ////////////////////////////////////// UPDATE ASYNC ///////////////////////////////////////////
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);


    ////////////////////////////////////// UPDATERANGE ASYNC ///////////////////////////////////////////
    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);


    ////////////////////////////////////// DELETE ASYNC ///////////////////////////////////////////
    Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default);



    ////////////////////////////////////// DELETERANGE ASYNC ///////////////////////////////////////////
    Task<ICollection<TEntity>> DeleteRangeAsync(
        ICollection<TEntity> entities,
        bool permanent = false,
        CancellationToken cancellationToken = default
    );



    /////////////////////////////////////   GetAllNoPaginateByDynamicAsync   //////////////////////////////////////////////

    Task<List<TEntity>> GetAllByDynamicNoPagingAsync(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );



    /////////////////////////////////////  GetAllNoPaginateAsync    //////////////////////////////////////////////
    Task<List<TEntity>> GetAllNoPaginateAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////           //////////////////////////////////////////////////


    TEntity? Get(
      Expression<Func<TEntity, bool>> predicate,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
      bool withDeleted = false,
      bool enableTracking = true
  );

    IPaginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true
    );

    IPaginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true
    );

    bool Any(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false
    );

    TEntity Add(TEntity entity);

    ICollection<TEntity> AddRange(ICollection<TEntity> entities);

    TEntity Update(TEntity entity);

    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

    TEntity Delete(TEntity entity, bool permanent = false);

    ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false);


    List<TEntity> GetAllByDynamicNoPaging(
       DynamicQuery dynamic,
       Expression<Func<TEntity, bool>>? predicate = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
       bool withDeleted = false,
       bool enableTracking = true
   );



    /////////////////////////////////////  GetAllNoPaginateAsync    //////////////////////////////////////////////
    List<TEntity> GetAllNoPaginate(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true
    );

}
