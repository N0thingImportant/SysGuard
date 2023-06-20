using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ProcessMonitor.DB
{
    static class DbSetExtensions
    {
        public static T AddOrFind<T>(this DbSet<T> set, T obj)
            where T : class
        {
            if (obj == null)
            {
                return null;
            }

            var expr = MakeFindExpr(obj);
            T temp = set.SingleOrDefault(expr);
            if (temp != null)
                return temp;
            else
                return set.Add(obj);
        }

        private static Expression<Func<T, bool>> MakeFindExpr<T>(T obj)
        {
            var param = Expression.Parameter(typeof(T), "param");
            Expression expr = null;
            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanRead || !property.CanWrite || property.Name == "ID")
                    continue;

                Expression equal = Expression.Equal(
                    Expression.Property(param, property),
                    Expression.Constant(property.GetValue(obj)));

                if (expr == null)
                    expr = equal;
                else
                    expr = Expression.AndAlso(expr, equal);
            }
            return Expression.Lambda<Func<T, bool>>(expr, param);
        }
    }
}
