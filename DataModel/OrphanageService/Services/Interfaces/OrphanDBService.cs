﻿using OrphanageService.DataContext.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IOrphanDBService
    {
        Task<OrphanDC> GetOrphan(int id);

        Task<IEnumerable<OrphanDC>> GetOrphans(int pageSize, int pageNum);

        Task<byte[]> GetOrphanFaceImage(int Oid);

        Task<byte[]> GetOrphanBirthCertificate(int Oid);

        Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid);

        Task<byte[]> GetOrphanFullPhoto(int Oid);
    }
}
