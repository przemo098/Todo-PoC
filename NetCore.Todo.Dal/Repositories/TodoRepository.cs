using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Todoer.SharedKernel.Dto;
using Todoer.SharedKernel.Model;

namespace Todoer.Dal.Repositories
{
    public class TodoRepository : SqLiteBaseRepository, ITodoRepository, IDisposable
    {
        private readonly SqliteConnection _cnn;

        public TodoRepository(SqliteConnection cnn = null)
        {
            if (!File.Exists(DbFile))
            {
                _cnn = cnn ?? SimpleDbConnection();
                CreateDatabase();
                return;
            }
            _cnn = cnn ?? SimpleDbConnection();
            _cnn.Open();
        }

        public void Dispose()
        {
            _cnn.Dispose();
        }

        public async Task<TodoDto> GetTodo(int id)
        {
            var result = (await _cnn.QueryAsync<TodoDto>(
                @"SELECT Name, Description, IsDone
                    FROM Todo
                    WHERE Id = @id", new {id})).FirstOrDefault();
            return result;
        }
        
        public async Task<int> SaveTodo(Todo todo)
        {
            return (await _cnn.QueryAsync<int>(
                @"INSERT INTO Todo 
                                ( Name, Description, IsDone, User ) VALUES 
                                ( @Name, @Description, @IsDone, @User );
                                select last_insert_rowid()", todo)).First();
        }

        public async Task<IEnumerable<Todo>> GetTodos(string user)
        {
            var result = await _cnn.QueryAsync<Todo>(
                @"SELECT Id, Name, Description, IsDone
                    FROM Todo
                    WHERE User = @user", new {user});
            return result;
        }

        public async Task<int> RemoveTodo(Todo todo)
        {
            var res =  await _cnn.ExecuteAsync(@"DELETE FROM [Todo] 
                    WHERE Name = @Name", todo);
            return res;
        }

        public async Task<int> Update(Todo todo)
        {
             var res = await _cnn.ExecuteAsync("UPDATE Todo SET Name = @Name WHERE Id = @Id", todo);
            return res;
        }

        private void CreateDatabase()
        {
            _cnn.Open();
            _cnn.Execute(
                @"create table Todo
                      (
                         Id                                  integer primary key AUTOINCREMENT,
                         Name                           varchar(100) not null,
                         Description                            varchar(100) not null,
                         IsDone                         integer not null,
                         User                            varchar(100) not null
                      )");
        }
    }
}