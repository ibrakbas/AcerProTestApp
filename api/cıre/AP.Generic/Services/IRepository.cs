using AutoFilterer.Types;
using Microsoft.EntityFrameworkCore.Query;
using AP.Generic.Abstraction;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AP.Generic.Services;

/// <summary> 
///  Bu arayüz, Repository desenini uygulayan temel veritabanı işlemlerini uygular
/// </summary>
public interface IRepository
{


    Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;


    void Complete();
    Task CompleteAsync(CancellationToken cancellationToken = default);


    Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;
    Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;
    Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>;
    IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
    List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;
    Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;
    Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default) where TEntity : class;
    Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;
    Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;
    Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;
}
