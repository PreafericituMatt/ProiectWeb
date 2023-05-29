using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProiectWebData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectWeb.Data
{
    public class SqlCommands<T> : ISqlCommands<T> where T : class
    {

        private readonly IConfiguration _configuration;

        public SqlCommands(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       

        public IEnumerable<T> GetAll(string sqlCommand)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var result = con.Query<T>(sqlCommand);

                return result;
            }
        }
       
        public int Insert(string sqlCommand)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var result = con.Execute(sqlCommand);

                return result;         
            }
        }

        public void Delete(string sqlCommand)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                con.Execute(sqlCommand);
            }
        }

        public int GetShoppingCartId(string sqlCommand)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var result = con.Query(sqlCommand);
                var id = result.First();
               
                return id;

            }
        }
    }
}
