using System.Threading.Tasks;
using Todoer.SharedKernel.Dto;
using Todoer.SharedKernel.Model;

namespace Todoer.Dal.Repositories
{
    interface ITodoRepository
    {
        Task<TodoDto> GetTodo(int id);
        Task<int> SaveTodo(Todo todo);

    }
}
