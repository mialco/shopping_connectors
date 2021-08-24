using LiteDB;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.repositories
{
	public class CorrelationRepositoryLiteDb : IIncrementalIdRepository
	{
		string _connectionString;
		public CorrelationRepositoryLiteDb(string connectionString)
		{
			_connectionString = connectionString;
		}

		public string GetNextId(string entityName)
		{

			using (var db = new LiteDatabase("Filename = C:\\Data\\Mialco\\Development\\ShoppingConnectorResources\\Data\\Db\\LiteDb.db"))
			{
				var result = string.Empty;
				var collection = db.GetCollection<Correlation>("EntityIdentifiers");
				

				if (db.BeginTrans())
				{
					var records = collection.Find(c => c.Name == entityName);
					if (records.Count()==0)
					{
						collection.Insert(new Correlation { Name=entityName, Identifier = 1, Created = DateTime.Now, LastUpdated = DateTime.Now });
						result = "1";
					}
					else
					{
						var rec = records.First();
						rec.Identifier++;
						rec.LastUpdated = DateTime.Now;
						collection.Update(rec);
						result = rec.Name + "-" +  rec.Identifier.ToString() + "-" + rec.LastUpdated;
					}
					db.Commit();

				}
				return result;

			}
		}

		class Correlation
		{
			public int _id { get; set; }
			public string Name { get; set; }
			public int Identifier { get; set; }
			public DateTime Created { get; set; }
			public DateTime LastUpdated { get; set; }
		}
	}
}
