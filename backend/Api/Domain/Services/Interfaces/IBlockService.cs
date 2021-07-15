using Api.Base.Pagination.Base;
using APITemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Domain.Services.Interfaces
{
    public interface IBlockService
    {
        Task<IPage<Block>> FindAll(IPageable pageable);
        void DaoTienAo(string DiaChiViNhanTienThuong);
        Task<bool> CheckValidate();
    }
}
