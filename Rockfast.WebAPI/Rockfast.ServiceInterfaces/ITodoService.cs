using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ViewModels;

namespace Rockfast.ServiceInterfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(int id);
        Task AddAsync(Todo entity);
        Task UpdateAsync(Todo entity);
        Task DeleteAsync(int id);
        Task<UserTodoVM> GetUserToDoList(int uerid);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<UserTodoVM>> GetToDoListAll();
    }
}
