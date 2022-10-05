using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.DAL.Commands.Interface
{
    public interface IUtilCommands
    {
        /// <summary>
        /// This query will return the associated top 1 TM_MET_Methods record by the passed parameters of Area, Method and Version
        /// </summary>
        public string GeTmMetMethodsByRouteParams { get; }
    }
}
