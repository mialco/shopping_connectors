using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.repositories
{
	public interface IIncrementalIdRepository
	{
		string GetNextId(string entityName);
	}
}
