using Dapper;
using Microsoft.Data.SqlClient;

namespace AllPractice.Models
{
    public class DBManagerModel
    {
        public List<Work> GetWork(SqlConnection conn)
        {

            List<Work> lstWork;
            try
            {
                var results = conn.Query<Work>("SELECT * FROM work").ToList();
                lstWork = results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Work>();
            }

            return lstWork;
        }
        public void InsertWork(SqlConnection conn, Work work)
        {
            try
            {
                var sql =
                @"INSERT INTO Work ([Name],[CreateDateTime],[UpdateTime] ) 
                VALUES (@Name,@CreateDateTime,@UpdateTime );";

                var result = conn.Execute(sql, work);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateWork(SqlConnection conn, Work work)
        {
            try
            {
                var sql =
                @"UPDATE Work SET [Name] = @Name,[CreateDateTime] = @CreateDateTime,[UpdateTime] = @UpdateTime
                WHERE Id = @Id";
                var result = conn.Execute(sql, work);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteWork(SqlConnection conn, Work work)
        {
            try
            {
                var sql =
            @"DELETE FROM Work WHERE Id = @Id";
                var result = conn.Execute(sql, new { Id = work.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
