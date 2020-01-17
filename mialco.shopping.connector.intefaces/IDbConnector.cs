using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.intefaces
{
    public interface IDbConnector<T> where T : IProduct
    {
		IEnumerable<T> GetProducts();
		IEnumerable<T> GetProducts(int page=0, int pageSize=0);
    }
}
