using MongoDB.Driver;

namespace WebMongoDB.Models
{
	public class ContextMongodb
	{
		public static string ConnectionString { get; set; }
		public static string DatabaseName { get; set; }	
		public static bool IsSSl { get; set; }
		private IMongoDatabase _database { get; }

		public ContextMongodb()
		{
			try
			{
				MongoClientSettings setting = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

				if (IsSSl) 
				{
					setting.SslSettings = new SslSettings
					{
						EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
					};
				}
				var mongoClient = new MongoClient(setting);
				_database = mongoClient.GetDatabase(DatabaseName);
			}
			catch (System.Exception)
			{

				throw new System.Exception("Unable to connect");
			}
		}

		public IMongoCollection<User> User
		{
			get
			{
				return _database.GetCollection<User>("User");
			}
		}
	}
}
