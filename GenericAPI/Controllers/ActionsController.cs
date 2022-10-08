using System.Text.Json.Nodes;
using Generic.API.dto;
using Generic.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace Generic.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly ResponseModel _rm = new ResponseModel();
       private readonly IMethodsRepository _methodsRepository;

       public ActionsController(IMethodsRepository methodsRepository)
       {
           _methodsRepository = methodsRepository ?? throw new ArgumentNullException(nameof(methodsRepository));
       }
        /// <summary>
        /// Please use content-type:application/json when posting a body
        /// </summary>
        /// <param name="area">The Controller Name</param>
        /// <param name="method">The Function Name</param>
        /// <param name="version">The Function Version Number</param>
        /// <param name="paramData">Any JSON parameters required</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost(template: "{area}/{method}/{version}")]
        public object GetData(string area, string method, string version,[FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] dynamic? paramData = null)
        {
            _rm.StartTimestamp = DateTime.Now;
            var cmd = _methodsRepository.GeTmMetMethodsByRouteParams(area, method, version);
            if (cmd is null)
            {
                return NotFound();
            }
            string? queryParams = null;
            if (paramData is not null)
            {
                queryParams = paramData.ToString();
            }

            var data = _methodsRepository.ActionExecuteMethodsAsync(cmd.met_Command, queryParams).Result;
            _rm.Data = data;
            _rm.EndTimestamp = DateTime.Now;
            return _rm;
        }


    }
}
