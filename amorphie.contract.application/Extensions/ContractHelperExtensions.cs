using System;
using amorphie.contract.core.Entity.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.application.Extensions
{
    public static class ContractHelperExtensions
    {
        public static IQueryable<TEntity> LikeWhere<TEntity>(this IQueryable<TEntity> source, string keyword)
            where TEntity : BaseEntity
        {
            if (string.IsNullOrEmpty(keyword) || keyword == "*" || keyword == "%")
            {
                return source.AsQueryable();
            }

            // "%" karakteri varsa deseni kontrol et
            if (keyword.Contains("%"))
            {
                // Deseni kontrol et ve LIKE operatörünü kullan
                source = source.Where(x => EF.Functions.Like(x.Code, keyword));
            }
            else
            {
                // "%" karakteri yoksa direkt olarak eşleşmeyi kontrol et
                source = source.Where(x => x.Code == keyword);
            }

            return source;
        }
    }
}

