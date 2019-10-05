using System;
using System.Linq;
using i18u.Authorizr.Web.Util;
using i18u.Repositories.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace i18u.Authorizr.Web.Models
{
    public partial class Account
    {
        private static readonly Lazy<IMongoRepository<Account>> RepositoryLazy;
        private static IMongoRepository<Account> Repository => RepositoryLazy.Value;

        private static IMongoRepository<Account> CreateRepository()
        {
            return Mongo.GetRepository<Account>(Account.Database, Account.Collection);
        }

        static Account()
        {
            RepositoryLazy = new Lazy<IMongoRepository<Account>>(CreateRepository);
        }

        /// <summary>
        /// Retrieve an account by the given email address.
        /// </summary>
        /// <param name="email">The email address to query by.</param>
        /// <param name="archived">Whether to include archived accounts.</param>
        /// <returns></returns>
        public static Account Get(string email, bool archived = false)
        {
            var sanitizedEmail = email.ToLower();
            var filterDefinition = Builders<Account>.Filter.And(
                Builders<Account>.Filter.Eq(account => account.EmailAddress, sanitizedEmail),
                Builders<Account>.Filter.Eq(account => account.Archived, archived)
            );

            var matchingAccounts = Repository.Get(filterDefinition);

            if (matchingAccounts.Count() > 1)
            {
                // todo: log
            }

            // Either return the matching account, or null
            return matchingAccounts.FirstOrDefault();
        }

        /// <summary>
        /// Returns an account by the given id.
        /// </summary>
        /// <param name="id">The <see cref="ObjectId"/> to search by.</param>
        /// <returns>The matching account, otherwise null.</returns>
        public static Account Get(ObjectId id)
        {
            return Repository.Get(id);
        }
    }
}