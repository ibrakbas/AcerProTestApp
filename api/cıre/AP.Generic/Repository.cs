using AutoFilterer.Extensions;
using AutoFilterer.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using AP.Generic.Abstraction;
using AP.Generic.Services;
using System.Globalization;
using System.Linq.Expressions;

namespace AP.Generic;

/// <summary>
/// Bu sınıf Repository fonksiyonlarının implementasyonlarını içerir, Tüm veritabanı işlemleri için kullanılabilir.
/// </summary>
internal sealed class Repository : IRepository
{
    private readonly DbContext _context;

     
    public Repository(DbContext context)
    {
        this._context = context;
    }

    

     
    public async Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.CreationDate = DateTime.UtcNow;
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        return entity;
    }
     

    public void Complete()
    {
        _context.SaveChanges();
    }

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private Expression<Func<TEntity, bool>> GenerateExpression<TEntity>(object id)
    {
        var type = _context.Model.FindEntityType(typeof(TEntity));
        string pk = type.FindPrimaryKey().Properties.Select(s => s.Name).FirstOrDefault();
        Type pkType = type.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

        object value = Convert.ChangeType(id, pkType, CultureInfo.InvariantCulture);

        ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
        MemberExpression me = Expression.Property(pe, pk);
        ConstantExpression constant = Expression.Constant(value, pkType);
        BinaryExpression body = Expression.Equal(me, constant);
        Expression<Func<TEntity, bool>> expression = Expression.Lambda<Func<TEntity, bool>>(body, new[] { pe });

        return expression;
    }
     
    public Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
     
    public Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(entity);
    } 
    public async Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.IsDeleted = true;
        entity.DeletionDate = DateTime.UtcNow;
        await ReplaceAsync<TEntity, TPrimaryKey>(entity, cancellationToken).ConfigureAwait(false);
    }
     
    public Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EasyBaseEntity<TPrimaryKey>
    {
        entity.ModificationDate = DateTime.UtcNow;
        _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
        _context.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }
     
    public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>().AsQueryable();
    }
     
    private IQueryable<TEntity> FindQueryable<TEntity>(bool asNoTracking) where TEntity : class
    {
        var queryable = GetQueryable<TEntity>();
        if (asNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }
        return queryable;
    }
     
    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);
        return queryable.ToList();
    }

    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking);
        queryable = includeExpression(queryable);
        return await queryable.ToListAsync(cancellationToken).ConfigureAwait(false);
    }


    public TEntity GetById<TEntity>(bool asNoTracking, object id) where TEntity : class
    {
        return _context.Set<TEntity>().FirstOrDefault(GenerateExpression<TEntity>(id));
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default) where TEntity : class
    {
        return await FindQueryable<TEntity>(asNoTracking).FirstOrDefaultAsync(GenerateExpression<TEntity>(id), cancellationToken).ConfigureAwait(false);
    }

    public TEntity GetById<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);
        return queryable.FirstOrDefault();
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);
        return await queryable.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }

    public TProjected GetById<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        return queryable.Select(projectExpression).FirstOrDefault();
    }

    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        return await queryable.Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }

    public TProjected GetById<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);
        return queryable.Select(projectExpression).FirstOrDefault();
    }

    public async Task<TProjected> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
    {
        var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
        queryable = includeExpression(queryable);
        return await queryable.Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }


}
