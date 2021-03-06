﻿using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFamilyDbService
    {
        Task<OrphanageDataModel.RegularData.Family> GetFamily(int FamId);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetExcludedFamilies();

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(IEnumerable<int> familiesIds);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FamId);

        Task<int> GetFamiliesCount();

        Task<int> GetOrphansCount(int FamId);

        Task<byte[]> GetFamilyCardPage1(int FamId);

        Task<byte[]> GetFamilyCardPage2(int FamId);

        Task SetFamilyColor(int FamId, int? colorValue);

        Task SetFamilyExclude(int FamId, bool value);

        Task<bool> SetFamilyCardPage1(int FamId, byte[] data);

        Task<bool> SetFamilyCardPage2(int FamId, byte[] data);

        Task<bool> BailFamilies(int BailId, IList<int> OrphanIds);

        /// <summary>
        /// add new family object to the database
        /// </summary>
        /// <param name="family">the family object</param>
        /// <returns></returns>
        Task<OrphanageDataModel.RegularData.Family> AddFamily(OrphanageDataModel.RegularData.Family family);

        Task<bool> SaveFamily(OrphanageDataModel.RegularData.Family family);

        Task<bool> DeleteFamily(int Famid);

        Task<bool> IsExist(OrphanageDataModel.RegularData.Family family);

        /// <summary>
        /// get families with the same address object
        /// </summary>
        /// <param name="motherObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.RegularData.Family> GetFamiliesByAddress(OrphanageDataModel.RegularData.Address addressObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}