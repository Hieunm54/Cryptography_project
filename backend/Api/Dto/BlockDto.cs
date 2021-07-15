using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Dto
{
    public class BlockDto
    {
        public Guid BlockID { get; set; }
        public DateTime DateCreate { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public string Data { get; set; }
        public int Nonce { get; set; }
        //public BlockDto
        public string TinhToanHash()
        { //Hàm mã hóa nội dung của toàn bộ Block. Do đó ta cần lấy toàn bộ các thuộc tính của Block đưa vào SHA256 để mã hóa ra một chuỗi.
            SHA256 sha256Hash = SHA256.Create();
            var result = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Hash + this.Data + this.DateCreate + this.Nonce));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                builder.Append(result[i].ToString());
            }
            return builder.ToString();
        }

        public void DaoBlock()
        {
            while (this.Hash.Substring(0, 5) != "00000")
            { //Kiểm tra xem giá trị Hash hiện tại đã đạt đủ số 0 ở đầu tiên như yêu vầu về độ khó đặt ra chưa. Lặp đi lặp lại hàm cho đến khi tìm được giá trị Hash đáp ứng yêu cầu.
                this.Nonce++; //Tăng giá trị trong Block lên, để Hash mỗi lần sẽ nhận được một giá trị khác nhau. Nếu Hash không có 2 hoặc n số 0 ở đầu thì sẽ không đạt yêu cầu và phải tiếp tục Hash cái khác.
                this.Hash = this.TinhToanHash(); //Tính toán lại Hash của toàn bộ Block ứng với lần tăng này.
            }
            //console.log("Đã đào xong Block: " + this.Hash); //Nếu đã tìm được Hash thì ta coi đấy là một lần "đào" (hashing) thành công.
        }
    }
}
