using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore.Abstraction
{
    public interface IRepository<T> // where T : Entity
    {	
		IEnumerable<T> GetAll();
		T GetById(int id);
    }
}
