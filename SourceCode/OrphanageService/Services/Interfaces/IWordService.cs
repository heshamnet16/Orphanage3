using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IWordService
    {
        /// <summary>
        /// save word Template.
        /// </summary>
        /// <returns>the template name or null when anything goes wrong</returns>
        Task<string> SaveTemplate(OrphanageDataModel.RegularData.DTOs.WordTemplete wordTemplete);

        /// <summary>
        /// Delete the specific word template from the database.
        /// </summary>
        /// <param name="data">set of columns as keys and list of string as value</param>
        /// <returns>true when template deleted in other case return false</returns>
        Task<bool> DeleteTemplate(string TemplateName);

        /// <summary>
        /// get all existing template names.
        /// </summary>
        /// <returns>list of all existing template names</returns>
        Task<IEnumerable<string>> GetAllTemplates();

        /// <summary>
        /// get WordTemplate Object by name
        /// </summary>
        /// <returns>returns an OrphanageDataModel.RegularData.DTOs.WordTemplete object or null</returns>
        Task<OrphanageDataModel.RegularData.DTOs.WordTemplete> GetTemplate(string TemplateName);
    }
}