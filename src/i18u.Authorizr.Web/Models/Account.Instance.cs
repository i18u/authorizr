using System.Runtime.Serialization;
using i18u.Repositories.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace i18u.Authorizr.Web.Models
{
    /// <summary>
    /// An account object.
    /// </summary>
    [DataContract]
    public partial class Account : IMongoModel
    {
        /// <summary>
        /// The name of the database to use.
        /// </summary>
        public const string Database = "i18u";

        /// <summary>
        /// The name of the collection to use.
        /// </summary>
        public const string Collection = "account";

        /// <summary>
        /// The 'unique' id of the account.
        /// </summary>
        /// <remarks>
        /// Given high enough throughput, a non-unique Id can be generated. If this happens, cry.
        /// </remarks>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The email address for the account.
        /// </summary>
        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The hash for this account's password.
        /// </summary>
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Whether or no this account is archived.
        /// </summary>
        [BsonElement("archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="passwordHash"></param>
        public Account(string emailAddress, string passwordHash) : this()
        {
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Account"/> class.
        /// </summary>
        public Account()
        {
            Archived = false;
        }
    }
}