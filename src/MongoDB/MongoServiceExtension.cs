using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
    /// <summary>
    /// mongo service extension
    /// </summary>
    public static class MongoServiceExtension
    {
        /// <summary>
        /// set mongo context service
        /// </summary>
        /// <param name="serviceCollection">services</param>
        /// <param name="options">options</param>
        public static void AddMongoContext(this IServiceCollection serviceCollection, Action<MongoBuilder> options)
        {
            var build = new MongoBuilder(serviceCollection);
            options(build);
        }
    }
}
