using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
    /// <summary>
    /// database context interface
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// data collection
        /// </summary>
        /// <typeparam name="TEntity"><para>TEntity</para> 数据类型</typeparam>
        /// <returns></returns>
        public DbCollection<TEntity> Collection<TEntity>() where TEntity : class;
    }
}
