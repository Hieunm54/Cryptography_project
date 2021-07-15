using Api.Base.Pagination.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Base.Utilities
{
    public static class QueryableExtensions
    {
        // PageIndex start at 1;
        // PageSize limited from 5 to 100 items per page - config from Constants.cs

        public static IPage<TEntity> UsePageable<TEntity>(this IQueryable<TEntity> query, IPageable pageable)
            where TEntity : class
        {
            var pageSize = Math.Max(Constants.Constants.PAGE_SIZE_MIN, Math.Min(pageable.PageSize, Constants.Constants.PAGE_SIZE_MAX));
            var pageIndex = Math.Max(Constants.Constants.PAGE_INDEX_START, pageable.PageNumber);
            var result = query.ApplySort(pageable.Sort)
                .Skip(pageSize * (pageIndex - Constants.Constants.PAGE_INDEX_START))
                .Take(pageSize);
            return new Page<TEntity>
            {
                Content = result,
                PageIndex = pageIndex,
                Sort = pageable.Sort,
                TotalItem = result.Count()
            };
        }

        public static async Task<IPage<TDto>> UsePageableAsDtoAsync<TEntity, TDto>(this IQueryable<TEntity> query, IPageable pageable, IMapper mapper)
            where TEntity : class where TDto : class
        {
            var pageSize = Math.Max(Constants.Constants.PAGE_SIZE_MIN, Math.Min(pageable.PageSize, Constants.Constants.PAGE_SIZE_MAX));
            var pageIndex = Math.Max(Constants.Constants.PAGE_INDEX_START, pageable.PageNumber);
            var result = await query.ApplySort(pageable.Sort)
                .Skip(pageSize * (pageIndex - Constants.Constants.PAGE_INDEX_START))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return new Page<TDto>
            {
                Content = result.Select(x => mapper.Map<TDto>(x)),
                PageIndex = pageIndex,
                Sort = pageable.Sort,
                TotalItem = result.Count
            };
        }

        public static async Task<IPage<TEntity>> UsePageableAsync<TEntity>(this IQueryable<TEntity> query, IPageable pageable)
            where TEntity : class
        {
            var pageSize = Math.Max(Constants.Constants.PAGE_SIZE_MIN, Math.Min(pageable.PageSize, Constants.Constants.PAGE_SIZE_MAX));
            var pageIndex = Math.Max(Constants.Constants.PAGE_INDEX_START, pageable.PageNumber);
            var result = await query.ApplySort(pageable.Sort)
                .Skip(pageSize * (pageIndex - Constants.Constants.PAGE_INDEX_START))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return new Page<TEntity>
            {
                Content = result,
                PageIndex = pageIndex,
                Sort = pageable.Sort,
                TotalItem = result.Count
            };
        }

        public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> query, Sort sort)
            where TEntity : class
        {
            if (!Queryable.Any(query) || sort == null || sort.Field == null)
            {
                return query;
            }

            PropertyInfo[] properties = typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var property = properties.FirstOrDefault(x => x.Name.Equals(sort.Field, StringComparison.InvariantCultureIgnoreCase));
            if (property != null)
            {
                var keySelectExpresstion = GetKeyExpression<TEntity, object>(property);
                if (sort.Direction.IsDescending())
                {
                    return query.OrderByDescending(keySelectExpresstion);
                }
                else
                {
                    return query.OrderBy(keySelectExpresstion);
                }
            }
            return query;
        }

        private static Expression<Func<TEntity, TKey>> GetKeyExpression<TEntity, TKey>(PropertyInfo propertyInfo)
        {
            var expresstionParameter = Expression.Parameter(typeof(TEntity), "x");
            Expression expresionProperty = Expression.Property(expresstionParameter, propertyInfo);
            Expression expressionConver = Expression.Convert(expresionProperty, typeof(TKey));
            return Expression.Lambda<Func<TEntity, TKey>>(expressionConver, expresstionParameter);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IQueryable<T> TakeIf<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy, bool condition, int limit, bool orderByDescending = true)
        {
            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            return condition ? query.Take(limit) : query;
        }

        public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex, bool orderByDescending = true)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            pageSize = Math.Max(Constants.Constants.PAGE_SIZE_MIN, Math.Min(pageSize, Constants.Constants.PAGE_SIZE_MAX));
            pageIndex = Math.Max(Constants.Constants.PAGE_INDEX_START, pageIndex);

            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            return query.Skip((pageIndex - Constants.Constants.PAGE_INDEX_START) * pageSize).Take(pageSize);
        }
    }
}
