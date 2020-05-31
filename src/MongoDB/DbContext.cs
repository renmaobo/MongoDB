using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MongoDB
{
    /// <summary>
    /// Mongo database context
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        /// <summary>
        /// database name
        /// </summary>
        public string DatabaseName { get; protected set; }

        /// <summary>
        /// mongo connection url address
        /// </summary>
        public MongoUrl MongoUrl { get; protected set; }

        /// <summary>
        /// mongo database client
        /// </summary>
        public MongoClient MongoClient { get; protected set; }

        /// <summary>
        /// mongo database client setting
        /// </summary>
        public MongoClientSettings MongoClientSettings { get; protected set; }

        /// <summary>
        /// constructor
        /// </summary>
        public DbContext() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString">database connection string</param>
        public DbContext(string connectionString)
        {
            this.MongoUrl = new MongoUrl(connectionString);

            MongoClient = new MongoClient(this.MongoUrl);

            this.DatabaseName = this.MongoUrl.DatabaseName;

            this.InitDbCollection();
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="url">mongo database connection url address</param>
        public DbContext(MongoUrl url)
        {
            this.MongoUrl = url;

            this.DatabaseName = this.MongoUrl.DatabaseName;

            MongoClient = new MongoClient(url);
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="databaseName">database name</param>
        /// <param name="mongoClientSettings">mongo client setting</param>
        public DbContext(string databaseName, MongoClientSettings mongoClientSettings)
        {
            this.MongoClientSettings = mongoClientSettings;
            this.DatabaseName = databaseName;
            MongoClient = new MongoClient(mongoClientSettings);
        }

        /// <summary>
        /// document collection
        /// </summary>
        /// <typeparam name="TEntity"><typeparamref name="TEntity"/> is data entity model</typeparam>
        /// <returns></returns>
        public DbCollection<TEntity> Collection<TEntity>() where TEntity : class
        {
            DbCollection<TEntity> collection = new DbCollection<TEntity>(this.MongoClient.Settings);

            return collection;
        }

        /// <summary>
        /// document collection
        /// </summary>
        /// <typeparam name="TEntity"><typeparamref name="TEntity"/> is data entity model</typeparam>
        /// <param name="collectionName">collection name</param>
        /// <returns></returns>
        public DbCollection<TEntity> Collection<TEntity>(string collectionName) where TEntity : class
        {
            DbCollection<TEntity> collection = new DbCollection<TEntity>(this.MongoClient.Settings, this.DatabaseName, collectionName);

            return collection;

        }

        /// <summary>
        /// init database collection
        /// </summary>
        private void InitDbCollection()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            Type type = typeof(DbCollection<>);
            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.Name.Contains("DbCollection"))
                {
                    object value = Activator.CreateInstance(propertyType);
                    var method = value.GetType().GetMethod("SetMongoClient");
                    method.Invoke(value, new object[] { this.MongoClient, this.DatabaseName });

                    property.SetValue(this, value);
                }
            }
        }
    }
}
