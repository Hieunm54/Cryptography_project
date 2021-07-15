namespace Api.Base.Enums
{
    /// <summary>
    /// Sort Direction - Hướng sắp xếp
    /// 0: Tăng dần
    /// 1: Giảm dần
    /// </summary>
    public enum Direction
    {
        Asc = 0,
        Desc = 1
    }

    /// <summary>
    /// LogLevel - Cấp độ Log
    /// 0: Info - Thông tin
    /// 1: Warning - Cảnh báo
    /// 2: Error/Exception - Lỗi/Ngoại lệ
    /// </summary>
    public enum LogLevel
    {
        Info = 1,
        Warning = 2,
        Error = 3
    }

    /// <summary>
    /// LogType - Kiểu log
    /// 0: System - Hệ thống
    /// 1: Request - Yêu cầu
    /// 2: Event - Sự kiện
    /// </summary>
    public enum LogType
    {
        System = 1,
        Request = 2,
        Event = 3
    }

    /// <summary>
    /// 0: Trạng thái đã duyệt
    /// 1: Chờ duyệt
    /// 2: Hủy
    /// 3: Có lỗi
    /// </summary>
    public enum Status
    {
        Pendding = 0,
        Approved = 1,
        Reject = 2,
        Error = 3
    }

    /// <summary>
    /// 0: Phương thức GET
    /// 1: Phương thức POST
    /// 2: Phương thức PUT
    /// 3: Phương thức DELETE
    /// </summary>
    public enum Method
    {
        GET = 0,
        POST = 1,
        PUT = 3,
        DELETE = 2
    }

    //Sử dụng cho hiển thị trạng thái của phân quyền group
    public enum GroupStatus
    {
        INACTIVE = 0,
        ACTIVE = 1
    }
    // Sử dụng cho hiển thị trạng thái của phân quyền group
    public enum UserStatus
    {
        PENDDING = 0,
        APPROVED = 1,
        REJECT = 2,
        ERROR = 3,
    }
    // Biến gán giá trị Gới tính
    public enum Sex
    {
        MALE = 1,
        FEMALE = 2,
    }
    // Khích thước app card
    public enum AppIconSize
    {
        BIG = 1,
        MEDIUM = 2,
        SMALL = 3
    }
    // Loại gọi kafka user
    public enum SSOUserEventType
    {
        Create = 0,
        Update = 1,
        ResetPassword = 2,
        SetPassword = 3,
        ChangeStatus = 4,
        Delete = 5,
        CreateSync = 6
    }

    public enum UserToolStatus
    {
        SUCCESS = 0,
        NOTEXIST = 1,
        ONLYVIEW = 2
    }
    // Loại người dùng
    public enum UserType
    {
        SuperAdmin = 999, // admin vào đc quản trị
        AdminSo = 100, //
        BanGiamDocSo = 1, 
        PhongToChucCanBo = 2,
        PhongKeHoachTaiChinh = 3,
        PhongThanhTra = 4,
        PhongGiaoDucMamNon = 5 ,
        PhongTieuHoc = 6,
        AdminPhong = 200,//
        BanLanhDaoPhong = 7,
        BanCongDoan = 8,
        BanHanhChinh = 9,
        BanToChucDang = 10,
        ToMamNon = 11,
        ToTieuHoc = 12,
        AdminTruong = 300,//
        HieuTruong = 13,
        PhoHieuTruong = 14,
        ToTruong = 15,
        GiaoVien = 16,
        KeToan = 17,
        VanThu = 18,
        HocSinh = 101,
        PhuHuynh = 102
    }
    // loại phân trang
    public enum LoadMode
    {
        ALL = 0, // load all,trả về page và totalItem
        PAGEONLY = 1,// trả về page
    }
    public enum SSOAppEventType
    {
        Create = 0,
        Update = 1,
        Delete = 2,
    }
    
    public enum CheckImport
    {
        USERNAME = 0,
        EMAIL = 1,
        PHONENUMBER = 2
    }
    public enum DeleteUserRoleList
    {
        LISTUSER = 0,
        USERROLE = 1
    }
    public enum GetUserRole
    {
        BYUSERIDANDROLEID = 0,
        BYUSERID = 1
    }

    // loại truy vấn
    public enum Compatiton
    {
        EQUAL = 0,
        CONTAIN = 1,
        STARTWWITH = 2,
        ENDWITH = 3,
        LESSTHAN = 4,
        MORETHAN = 5,
    }
    public enum Operator
    {
        AND = 1,
        OR = 2
    }
    // mode kiểm tra user đã tồn tại chưa theo điều kiện
    public enum ModeCheckUser
    {
        UserName = 0,
        Email = 1,
        Phone = 2
    }

    public enum ChartType
    {
        Access = 1,
        Summary = 2
    }

}
