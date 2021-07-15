
using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using APITemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Domain.Repositories.Interfaces
{
    public interface IBlockRepository : IGenericRepository<Block>
    {
        Task<BlockDto> GetLastBlock();
        Block CreateNewBlock(Block newBlock);
        void DaoTienAo(string DiaChiViNhanTienThuong);
        Task<bool> CheckValidate();

    }
}
