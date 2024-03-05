using Microsoft.EntityFrameworkCore;
using Rockfast.ApiDatabase;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.Dependencies
{
    public class TodoService : ITodoService
    {
        private readonly ApiDbContext _database;
        public TodoService(ApiDbContext db)
        {
            this._database = db;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            try
            {
                return await this._database.Set<Todo>().ToListAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }
            
          
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            try
            {
                return await _database.Set<Todo>().FindAsync(id);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }

            
        }

        public async Task AddAsync(Todo entity)
        {
            try
            {
                _database.Set<Todo>().Add(entity);
                await _database.SaveChangesAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }
            
        }

        public async Task UpdateAsync(Todo entity)
        {
            try
            {
                _database.Entry(entity).State = EntityState.Modified;
                await _database.SaveChangesAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }
            
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _database.Set<Todo>().FindAsync(id);
                if (entity != null)
                {
                    _database.Set<Todo>().Remove(entity);
                    await _database.SaveChangesAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }
            
        }

        public async Task<UserTodoVM> GetUserToDoList(int userid)
        {
            try
            {
                var userTodomodels = await this._database.Set<User>()
                .Where(u => u.UserId == userid)
                .Select(u => new UserTodoVM
                {
                    UserId = u.UserId,
                    ToDoList = u.Todos.Select(t => new TodoVM
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DateCreated = t.DateCreated,
                        Complete = t.Complete,
                        DateCompleted = t.DateCreated,
                        UserId = t.UserId
                    }).ToList()
                })
                .FirstOrDefaultAsync();


                return userTodomodels;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }


        }


        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
                return await this._database.Set<User>().ToListAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected error occurred : {ex.Message}");
                throw;
            }


        }
    }
}
