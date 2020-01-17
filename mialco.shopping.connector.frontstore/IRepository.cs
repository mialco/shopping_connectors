using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
    public interface IRepository<T> where T : Entity
    {
		IEnumerable<T> GetAll();
    }
}
