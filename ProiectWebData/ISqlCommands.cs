using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectWebData
{
    public interface ISqlCommands<T> where T : class
    {
        public IEnumerable<T> GetAll(string sqlCommand);
        public int Insert(string sqlCommand);
        public void Delete(string sqlCommand);
        public int GetShoppingCartId(string sqlCommand);
    }
}
