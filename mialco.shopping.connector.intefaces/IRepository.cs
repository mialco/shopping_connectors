using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.intefaces
{
    public interface IRepository<T>
    {
		T GetById(int id);
		IEnumerable<T> GetAll();
    }
}
