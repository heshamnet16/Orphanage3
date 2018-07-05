using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class SQLStatmentsExecuterService : ISQLStatmentsExecuter
    {
        private readonly ILogger _logger;

        public SQLStatmentsExecuterService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<int> ExecuteCommand(string command)
        {
            _logger.Information($"trying to execute SQL Command //{command}//");
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                try
                {
                    int ret = await orphanageDbc.Database.ExecuteSqlCommandAsync(command);
                    _logger.Information($"command had executed successfully, {ret} changes had been made");
                    return ret >= 0 ? ret : 0;
                }
                catch (Exception ex)
                {
                    _logger.Error($"SQLException// {ex.Message} //");
                    return 0;
                }
            }
        }

        public async Task<int> ExecuteCommands(string commands)
        {
            _logger.Information($"trying to execute SQL Commands //{commands}//");
            int ret = 0;
            IEnumerable<string> commandStrings = Regex.Split(commands, @"^\s*GO\s*$",
                         RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (var command in commandStrings)
            {
                if (command.Trim() != "")
                {
                    ret += await ExecuteCommand(command);
                }
            }
            _logger.Information($"All commands had been executed, {ret} changes had been made");
            return ret;
        }
    }
}