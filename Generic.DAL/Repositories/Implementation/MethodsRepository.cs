using System.Data;
using Dapper;
using Generic.DAL.Commands.Interface;
using Generic.DAL.Repositories.Interface;
using Generic.Models.dbo;

namespace Generic.DAL.Repositories.Implementation
{
    public class MethodsRepository:IMethodsRepository
    {
        private readonly IDbConnection _connection;
        private readonly IUtilCommands _utilCommands;

        public MethodsRepository(IDbConnection connection, IUtilCommands utilCommands)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _utilCommands = utilCommands ?? throw new ArgumentNullException(nameof(utilCommands));
        }

        /// <inheritdoc cref="IMethodsRepository.GeTmMetMethodsByRouteParams"/>>
        public TM_MET_Methods GeTmMetMethodsByRouteParams(string area, string method, string version)
        {
            return _connection.QueryFirstOrDefault<TM_MET_Methods>(_utilCommands.GeTmMetMethodsByRouteParams,new
            {
                  met_Area = area
                , met_Method = method
                , met_Version = version
            });
        }

        /// <inheritdoc cref="IMethodsRepository.ActionExecuteMethodsAsync"/>>
        public async Task<object> ActionExecuteMethodsAsync(string command, object paramObj)
        {
            return  (from row in await _connection.QueryAsync(command,new { Param = paramObj } ) select (IDictionary<string, object>)row).AsList();
             
        }
    }
}
