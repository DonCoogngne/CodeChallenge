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
        private readonly IWebHostEnvironment _webHostEnv;

        public TodosController(ITodoService todoService, ILogger<TodosController> logger, IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
            this._todoService = todoService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<TodoVM>> Get()
        {
            LogEvents.LogToFile("TodosController", "Get All TodoList", _webHostEnv);
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
                LogEvents.LogToFile("TodosController Get All TodoList", ex.ToString(), _webHostEnv);
                throw ex;
            }
            
        }


        [HttpPost]
        [Route("GetById")]
        public async Task<TodoVM> GetById(int id)
        {
            LogEvents.LogToFile("TodosController", "Get Todo by Id", _webHostEnv);
            try
            {
                var result = await this._todoService.GetByIdAsync(id);
                TodoVM viewModel = new TodoVM();
                viewModel = CreateTodoVM(result);
                return viewModel;
            }
            catch (Exception ex)
            {
                LogEvents.LogToFile("TodosController Get Todo by Id", ex.ToString(), _webHostEnv);
                throw ex;
            }

        }

        [HttpPost]
        [Route("CreateTodo")]
        public async Task<TodoVM> Post(TodoVM model)
        {
            LogEvents.LogToFile("TodosController", "Create Todo", _webHostEnv);
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
                LogEvents.LogToFile("TodosController Create Todo", ex.ToString(), _webHostEnv);
                throw ex;
            }


        }

        [HttpPut]
        [Route("UpdateTodo")]
        public async Task<ActionResult<TodoVM>> Put(TodoVM model)
        {
            LogEvents.LogToFile("TodosController", "Update Todo", _webHostEnv);
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
                LogEvents.LogToFile("TodosController Update Todo", ex.ToString(), _webHostEnv);
                throw ex;
            }

        }


        [HttpDelete]
        [Route("DeleteTodo")]
        public async Task<ActionResult> Delete(int id)
        {
            LogEvents.LogToFile("TodosController", "Delete Todo", _webHostEnv);
            try
            {
                await this._todoService.DeleteAsync(id);
                return Ok(new { Message = "Delete successful", StatusCode = 200 });
            }           
             catch (Exception ex)
            {
                LogEvents.LogToFile("TodosController Delete Todo", ex.ToString(), _webHostEnv);
                throw ex;
            }

        }

        private TodoVM CreateTodoVM(Todo model)
        {
            LogEvents.LogToFile("TodosController", "CreateTodoVM", _webHostEnv);
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
                LogEvents.LogToFile("TodosController CreateTodoVM", ex.ToString(), _webHostEnv);
                throw ex;
            }
        }

        [HttpGet]
        [Route("GeUserTodoList")]
        public async Task<UserTodoVM> GetUserToDoList(int uerid)
        {
            LogEvents.LogToFile("TodosController", "GetUserToDoList", _webHostEnv);
            try
            {
                var result = await this._todoService.GetUserToDoList(uerid);
                return result;
            }
            catch (Exception ex)
            {
                LogEvents.LogToFile("TodosController GetUserToDoList", ex.ToString(), _webHostEnv);
                throw ex;
            }
        }

        [HttpGet]
        [Route("GeTodoListAll")]
        public async Task<IEnumerable<UserTodoVM>> GeTodoListAll()
        {
            LogEvents.LogToFile("TodosController", "Get All Todo List Call", _webHostEnv);
            try
            {
                var result = await this._todoService.GetToDoListAll();
                return result;
            }
            catch (Exception ex)
            {
                LogEvents.LogToFile("TodosController GeTodoListAll", ex.ToString(), _webHostEnv);
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserVM>> GetAllUsers()
        {
            try
            {
                LogEvents.LogToFile("TodosController", "Get All Users Call", _webHostEnv);
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
                LogEvents.LogToFile("TodosController GetAllUsers", ex.ToString(), _webHostEnv);
                throw ex;
            }
        }


    }
}
