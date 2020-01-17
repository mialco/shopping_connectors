using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
    public interface IMialcoWorkflowRepository<T> where T: Entity
    {
		IEnumerable<T> GetAll();
		T GetById(long id);
    }
}
