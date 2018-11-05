using PAD.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PAD.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase(IOptions<ConnectionStringList> connectionStrings)
        {
            _connectionString = connectionStrings.Value.ConnectionString1;
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_connectionString);
            }
        }

        public List<Authors> GetAuthors()
        {
            using (IDbConnection conn = Connection)
            {
                List<Authors> result;
                conn.Open();
                result = conn.Query<Authors>("SELECT * FROM authors").ToList();

                return result;
            }
        }

        public List<Posts> GetPosts()
        {
            using (IDbConnection conn = Connection)
            {
                List<Posts> result;
                conn.Open();
                result = conn.Query<Posts>("SELECT * FROM posts").ToList();

                return result;
            }
        }
    }
}