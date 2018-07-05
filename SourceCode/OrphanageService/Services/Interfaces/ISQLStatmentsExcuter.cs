using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface ISQLStatmentsExecuter
    {
        Task<int> ExecuteCommand(string command);

        Task<int> ExecuteCommands(string commands);
    }
}