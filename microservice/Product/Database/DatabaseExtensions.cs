using System;
using System.Linq;
using System.Linq.Expressions;

namespace Product.Database
{
    public static class DatabaseExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, string propertyName, Expression<Func<TSource, TKey>> defaultOrderingProperty)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return source.OrderBy(defaultOrderingProperty);
            }

            propertyName = UpperFirst(propertyName);

            if (typeof(TSource).GetProperty(propertyName) == null)
            {
                return source.OrderBy(defaultOrderingProperty);
            }
            
            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(TSource), "x");
            Expression property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2);
            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);
            var result = orderByGeneric.Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TSource>)result;
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, string propertyName, Expression<Func<TSource, TKey>> defaultOrderingProperty)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return source.OrderByDescending(defaultOrderingProperty);
            }

            propertyName = UpperFirst(propertyName);

            if (typeof(TSource).GetProperty(propertyName) == null)
            {
                return source.OrderByDescending(defaultOrderingProperty);
            }

            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(TSource), "x");
            Expression property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2);
            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);
            var result = orderByGeneric.Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TSource>)result;
        }

        private static string UpperFirst(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);  
        }
    }
}