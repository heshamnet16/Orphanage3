using OrphanageService.DataContext;
using OrphanageService.Services.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface ICheckerService
    {
        /// <summary>
        /// check if Name is Existing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        Task<CheckerResultData> IsNameExist(OrphanageDataModel.RegularData.Name name, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// check if the identity number exist
        /// </summary>
        /// <param name="IdNumber"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        Task<CheckerResultData> IsIdentityNumberExist(string IdNumber, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// check if th phone number or Email is exist
        /// </summary>
        /// <param name="address"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        Task<CheckerResultData> IsContactDataExist(OrphanageDataModel.RegularData.Address address, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// check if the family card number in the Family object is exist
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        Task<CheckerResultData<OrphanageDataModel.RegularData.Family>> IsFamilyCardNumberExist(string cardNumber, OrphanageDbCNoBinary orphanageDbCNo);


    }
}