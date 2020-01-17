using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.repositories
{
    public interface IRepository<T> where T:  AggregateRoot 
    {
		T GetById(long id);
		IEnumerable<T> GettAll();
    }
}
