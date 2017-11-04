using System.Linq;
using Todoer.Dal.Repositories;
using Todoer.SharedKernel.Model;
using Xunit;

namespace Todoer.Dal.IntegrationTests
{
    public class TodoOperations
    {
        private TodoRepository _repo;

        public TodoOperations()
        {
            _repo =  new TodoRepository();
        }

        [Theory]
        [InlineData("Test description1", false, "super uber TestoTestname1", "super uber TestoTestUser1")]
        public async void can_save_and_delete_Todo(string desc, bool isDone, string name, string user)
        {
            var todo = new Todo
            {
                Description = desc,
                IsDone = isDone,
                Name = name,
                User = user
            };

            await _repo.SaveTodo(todo);
            Assert.True(_repo.GetTodos(user).Result.Count() == 1);

            await _repo.RemoveTodo(todo);
            Assert.True(!_repo.GetTodos(user).Result.Any());
        }


        [Theory]
        [InlineData("Test description2", false, "super uber TestoTestname2", "super uber TestoTestUser2")]
        public async void can_query_multiple(string desc, bool isDone, string name, string user)
        {
            var todo = new Todo
            {
                Description = desc,
                IsDone = isDone,
                Name = name,
                User = user
            };

            await _repo.SaveTodo(todo);
            await _repo.SaveTodo(todo);
            await _repo.SaveTodo(todo);

            Assert.True(_repo.GetTodos(user).Result.Count() == 3);

            await _repo.RemoveTodo(todo);

            Assert.True(!_repo.GetTodos(user).Result.Any());
        }

        [Theory]
        [InlineData("Test description3", false, "super uber TestoTestname3", "super uber TestoTestUser3")]
        public async void can_update_single_record(string desc, bool isDone, string name, string user)
        {
            var todo = new Todo
            {
                Description = desc,
                IsDone = isDone,
                Name = name,
                User = user
            };

            await _repo.SaveTodo(todo);
            var repoItem = _repo.GetTodos(user).Result.First();
            repoItem.Name = "some other name";

            await _repo.Update(repoItem);


        }
    }
}
