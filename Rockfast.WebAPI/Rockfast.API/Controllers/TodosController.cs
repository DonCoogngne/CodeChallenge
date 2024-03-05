using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;

namespace Rockfast.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private ILogger<TodosController> _logger;
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            this._todoService = todoService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<TodoVM>> Get()
        {
            try
            {
                var result = await this._todoService.GetAllAsync();
                List<TodoVM> viewModels = new List<TodoVM>();

                foreach (var model in result)
                {
                    TodoVM modelData = CreateTodoVM(model);
                    viewModels.Add(modelData);

                }
                return viewModels;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        [HttpPost]
        [Route("GetById")]
        public async Task<TodoVM> GetById(int id)
        {
            try
            {
                var result = await this._todoService.GetByIdAsync(id);
                TodoVM viewModel = new TodoVM();
                viewModel = CreateTodoVM(result);
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("CreateTodo")]
        public async Task<TodoVM> Post(TodoVM model)
        {
            try
            {
                Todo dbmodel = new Todo();
                dbmodel.Id = model.Id;
                dbmodel.Name = model.Name;
                dbmodel.DateCreated = model.DateCreated;
                dbmodel.Complete = model.Complete;
                dbmodel.DateCompleted = model.DateCompleted;
                dbmodel.UserId = model.UserId;
                await this._todoService.AddAsync(dbmodel);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPut]
        [Route("UpdateTodo")]
        public async Task<ActionResult<TodoVM>> Put(TodoVM model)
        {
            try
            {
                Todo dbmodel = new Todo();
                dbmodel.Id = model.Id;
                dbmodel.Name = model.Name;
                dbmodel.DateCreated = model.DateCreated;
                dbmodel.Complete = model.Complete;
                dbmodel.DateCompleted = model.DateCompleted;
                dbmodel.UserId = model.UserId;
                await this._todoService.UpdateAsync(dbmodel);
                return Ok(new { Message = "Update successful", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpDelete]
        [Route("DeleteTodo")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this._todoService.DeleteAsync(id);
                return Ok(new { Message = "Delete successful", StatusCode = 200 });
            }           
             catch (Exception ex)
            {
                throw ex;
            }

        }

        private TodoVM CreateTodoVM(Todo model)
        {
            try
            {
                TodoVM viewModel = new TodoVM();
                viewModel.Id = model.Id;
                viewModel.Name = model.Name;
                viewModel.DateCreated = model.DateCreated;
                viewModel.Complete = model.Complete;
                viewModel.DateCompleted = model.DateCompleted;
                viewModel.UserId = model.UserId;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GeUserTodoList")]
        public async Task<UserTodoVM> GetUserToDoList(int uerid)
        {
            try
            {
                var result = await this._todoService.GetUserToDoList(uerid);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GeTodoListAll")]
        public async Task<IEnumerable<UserTodoVM>> GeTodoListAll()
        {
            try
            {
                var result = await this._todoService.GetToDoListAll();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserVM>> GetAllUsers()
        {
            try
            {
                var result = await this._todoService.GetAllUserAsync();
                List<UserVM> viewModels = new List<UserVM>();

                foreach (var model in result)
                {
                    UserVM modelData = new UserVM();
                    modelData.UserId = model.UserId;
                    modelData.Name = model.Name;
                    viewModels.Add(modelData);

                }
                return viewModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
