using System;
using System.Collections.Generic;

namespace mialco.shopping.connector.entities.abstraction
{
	public abstract class Entity
	{
		private long _id = 0;

		public Entity()
		{
		}

		public Entity(long id)
		{
			AssignId(id);
		}

		private string a;
		public string Name { get; set;}
		public string Description { get; set; }
		//public string LongDescription { get; set; }


		public void AssignId(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			_id = id;


		}

		public override bool Equals(object obj)
		{
			//If Objects are null return false
			if (obj == null)
				return false;

			if (this.GetType() != obj.GetType())
				return false;

			// if ids and names and descriptions are empty object are equals
			var p = obj as Entity;

			if (_id == 0 && p._id == 0 && string.IsNullOrEmpty(Name)
				&& string.IsNullOrEmpty(p.Name))
				return true;



				return false;
		}

		public override int GetHashCode()
		{
			var hashCode = -1712539152;
			hashCode = hashCode * -1521134295 + _id.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
			return hashCode;
		}
	}
}
