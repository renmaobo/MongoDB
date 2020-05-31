using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
    /// <summary>
    /// mongo builder
    /// </summary>
    public class MongoBuilder
    {
        /// <summary>
        /// services
        /// </summary>
        public IServiceCollection ServiceCollection { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="serviceCollection">service collection</param>
        public MongoBuilder(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        /// <summary>
        /// use mongo constructor
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public void UseMongoContext<TDbContext>(string connectionString) where TDbContext : DbContext
        {
            this.ServiceCollection.AddSingleton(e =>
            {
                Type type = typeof(TDbContext);
                object context = Activator.CreateInstance(type, connectionString);

                return context as TDbContext;
            });
        }
    }
}
