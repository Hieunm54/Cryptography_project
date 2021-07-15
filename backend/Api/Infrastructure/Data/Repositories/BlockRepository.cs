using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using APITemplate.Dto;
using APITemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Infrastructure.Data.Repositories
{
    public class BlockRepository : GenericRepository<Block>, IBlockRepository
    {
        private readonly ApplicationDatabaseContext contexts;
        public List<Block> ListBlock { get; set; }
        //public int DoKho { get; set; }


        public BlockRepository(IUnitOfWork context, ApplicationDatabaseContext _contexts) : base(context)
        {
            this.contexts = _contexts;
            this.ListBlock = new List<Block>();
            
        }

        public override async Task<Block> CreateOrUpdateAsync(Block entity)
        {
            bool exists = await Exists(x => x.BlockID == entity.BlockID);
            if (entity.BlockID != Guid.Empty && exists)
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
            return entity;
        }

        //lấy phần tử cuối cùng
        public async Task<BlockDto> GetLastBlock()
        {
            var lstBlock = await (from b in contexts.Block
                                  select new BlockDto
                                  {
                                      BlockID = b.BlockID,
                                      DateCreate = b.DateCreate,
                                      Hash = b.Hash,
                                      PreviousHash = b.PreviousHash,
                                      Nonce = b.Nonce
                                  }).ToListAsync();
            lstBlock = lstBlock.OrderByDescending(x => x.DateCreate).ToList();
            if(lstBlock != null && lstBlock.Count > 0)
            {
                var lastBlock = lstBlock[0];
                return lastBlock;
            } else
            {
                return null;
            }
            
        }

        //tạo mới block
        public Block CreateNewBlock(Block newBlock)
        {
            newBlock.PreviousHash = this.GetLastBlock().Result.Hash; //Lấy Hash của phần tử cuối cùng của mảng và lưu vào HasTruocDo của phần tử này
            //newBlock.Hash = newBlock.TinhToanHash(); //Lúc này ta không tính toán Hash đơn thuần nữa mà phải "đào" thì mới có Hash cho Block mới.
            newBlock.DaoBlock();
            //Nối phần tử newBlock vào làm phần tử cuối cùng của mảng Blockchain sau khi đã "đào" được.
            return newBlock;
        }

        public async void DaoTienAo(string DiaChiViNhanTienThuong)
        { //Hàm dùng để đào (thêm mới) một Block vào Blockchain.
          //Lúc này ta sẽ tạo mới một Block, trong Block này sẽ chứa toàn bộ các giao dịch đã bị tạm hoãn trước đó, do nó chưa được đào và chưa có Hash.
            Block block;
            if(this.GetLastBlock().Result != null)
            {
                block = new Block(DateTime.Now, this.GetLastBlock().Result.Hash);
            }
            else
            {
                block = new Block(DateTime.Now,"");
            }

            block.DaoBlock(); //Vẫn phải đào Hash bình thường cho lần này.
            this.ListBlock.Add(block); //Nối phần tử block vào làm phần tử cuối cùng của mảng Blockchain sau khi đã "đào" được.
            //thêm block vào db
            var resBlock = await CreateOrUpdateAsync(block);
            //await SaveChangesAsync();
            //Sau khi đã bỏ công ra đào 1 Hash cho giao dịch hiện tại, ta sẽ có quyền được thưởng một phần tiền thưởng cố định sẵn. GiaoDichTamHoan đã được xử lý xong nên có thể xóa nó đi, sau đó ta gán một GiaoDichTamHoan mới, trong đó chuyển lượng tiền ta nhận được vào ví của chính mình.
            //thực hiện thêm vào db giao dịch này
            await contexts.Exchange.AddAsync(new Exchange
            {
                AddressTo = DiaChiViNhanTienThuong,
                Value = block.Value,
                BlockID = block.BlockID
            });
            await contexts.SaveChangesAsync();
            //await _exchangeRepository.CreateOrUpdateAsync(new Exchange
            //{
            //    AddressTo = DiaChiViNhanTienThuong,
            //    Value = block.Value
            //});
            //await _exchangeRepository.SaveChangesAsync();
            //Chú ý là chỗ này ta không thể nhận được ngay lượng tiền này trong ví, vì giao dịch chưa được tạo và chưa có Hash. Nên trong Blockchain chưa có bản ghi mới ghi nhận số tiền đã chuyển vào ví nhận tiền thưởng.
            //Để nhận được khoản tiền thưởng cho lần đào này. Thì ta phải đợi đến lần đào kế tiếp, giao dịch tạm hoãn này sẽ được khớp lệnh và lúc đó tiền thưởng mới có trong ví.
        }

        //kiểm tra tính toàn vẹn
        public async Task<bool> CheckValidate()
        {
            var lstBlock = await (from b in contexts.Block
                                  from d in contexts.DataUser
                                  from c in contexts.BlockData
                                  where b.BlockID == c.BlockID && d.DataUserID == c.DataUserID
                                  select new BlockDto
                                  {
                                      BlockID = b.BlockID,
                                      DateCreate = b.DateCreate,
                                      Hash = b.Hash,
                                      PreviousHash = b.PreviousHash,
                                      Nonce = b.Nonce,
                                      Data = d.UserName
                                  }).ToListAsync();
            for (int i = 1; i < lstBlock.Count(); i++)
            {
                var BlockHienTai = lstBlock[i]; //Lấy ra phần tử ở vị trí hiện tại
                var BlockTruocDo = lstBlock[i - 1]; //Lấy ra phần tử ở ngay trước vị trí hiện tại
                if (BlockHienTai.Hash != BlockHienTai.TinhToanHash())
                { //Kiểm tra lại Hash của toàn bộ Block hiện tại và Hash đã lưu xem có trùng nhau không.
                    return false; //Nếu không trùng tức là Dữ liệu trong Block hiện tạiđã bị chỉnh sửa, hàm KiemTraToanVen sẽ trả về false luôn.
                }
                if (BlockHienTai.PreviousHash != BlockTruocDo.Hash)
                { //Lấy Hash hiện tại và Hash phần tử trước đó đã lưu xem có trùng nhau không.
                    return false; //Nếu không trùng tức là Hash của Block hiện tại đã bị chỉnh sửa, hàm KiemTraToanVen sẽ trả về false luôn.
                }
            }
            return true;
        }

    }

}
