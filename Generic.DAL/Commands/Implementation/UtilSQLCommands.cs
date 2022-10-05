using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.DAL.Commands.Interface;

namespace Generic.DAL.Commands.Implementation
{
    public class UtilSqlCommands :IUtilCommands
    {
        /// <inheritdoc cref="IUtilCommands.GeTmMetMethodsByRouteParams"/>>
        public string GeTmMetMethodsByRouteParams => @"SELECT TOP 1 [met_MethodID]
                                                                   ,[met_DisplayName]
                                                                   ,[met_Area]
                                                                   ,[met_Method]
                                                                   ,[met_Command]
                                                                   ,[met_Version]
                                                                   ,[met_Enabled]
                                                               FROM [fbo_Test].[Action].[TM_MET_Methods]
                                                               WHERE [met_Area] = @met_Area
                                                               AND [met_Method] = @met_Method
                                                               AND [met_Version] =@met_Version
                                                               ORDER BY met_Version DESC";
    }
}
