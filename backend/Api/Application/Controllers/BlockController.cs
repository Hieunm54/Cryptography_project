using Api.Base.ExceptionFilter.Exceptions;
using Api.Base.Pagination.Base;
using APITemplate.Domain.Services.Interfaces;
using APITemplate.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BlockController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBlockService _blockService;

        public BlockController(
            IMapper mapper,
            IBlockService blockService
            )
        {
            _mapper = mapper;
            _blockService = blockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlocksAsync([FromQuery] Pageable pageable)
        {
            if (pageable == null || pageable.PageSize == 0)
                throw new DataInvalidException("Dữ liệu phân trang không đúng");
            var users = await _blockService.FindAll(pageable);
            var pageResult = users.Content.Select(x => _mapper.Map<BlockDto>(x)).ToList();
            return Ok(pageResult);
        }
        [HttpPost]
        public async Task<IActionResult> MiningCoin(string AddressTo) {
            bool checkValidate = await _blockService.CheckValidate();
			if(checkValidate)
			{
				try
				{
                    _blockService.DaoTienAo(AddressTo);
                } catch (Exception ex)
				{
                    throw new DataInvalidException("Đào tiền thất bại");
				}
                
			}
			else
			{
                throw new DataInvalidException("Có lỗi xảy ra khi check validate");
			}
            return Ok();
		}
    }
}
