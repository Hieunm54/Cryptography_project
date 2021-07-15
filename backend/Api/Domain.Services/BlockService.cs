using Api.Base.Pagination.Base;
using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using APITemplate.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace APITemplate.Domain.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;

        public BlockService(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public Task<bool> CheckValidate()
        {
            return _blockRepository.CheckValidate();
        }

        public void DaoTienAo(string DiaChiViNhanTienThuong)
        {
            _blockRepository.DaoTienAo(DiaChiViNhanTienThuong);
        }

        public async Task<IPage<Block>> FindAll(IPageable pageable)
        {
            return await _blockRepository.QueryHelper()
                .GetPageAsync(pageable);
        }

    }
}
