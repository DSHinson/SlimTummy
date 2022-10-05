using Generic.Models.dbo;

namespace Generic.DAL.Repositories.Interface
{
    public interface IMethodsRepository
    {
        /// <summary>
        /// Returns the record TM_MET_Methods associated with the passed in route details
        /// </summary>
        /// <param name="area">Equivalent to the controller name</param>
        /// <param name="method">Equivalent to the method name</param>
        /// <param name="version">The version number. We should return the max if not defined</param>
        /// <returns>TM_MET_Methods</returns>
        public TM_MET_Methods? GeTmMetMethodsByRouteParams(string area, string method, string version);

        /// <summary>
        /// Return the data from the command passed in
        /// </summary>
        /// <param name="Command">The database command to be executed by dapper</param>
        /// <param name="paramObj">The parameter object for dapper to bind to the SQL</param>
        /// <returns></returns>
        public Task<object> ActionExecuteMethodsAsync(string Command, object paramObj);
    }
}
