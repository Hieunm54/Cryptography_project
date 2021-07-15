using System;

namespace Api.Base.Logging
{
    public partial class ActionAudit
    {
        public long ActionAuditId { get; set; }
        public string UserName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; } 
        public string Parameter { get; set; } // tham số truyền vào
        public DateTime? BeginAuditTime { get; set; }
        public DateTime? EndAuditTime { get; set; }
        public string Ip { get; set; } 
        public string Referer { get; set; } // 
        public string UserAgent { get; set; } // from webbrowser ex: chrome86
        public int? ObjectId { get; set; } // id của đối tượng (query/update/delete)
        public string OldObjectValue { get; set; } // giá trị cũ của đối tượng
        public string NewObjectValue { get; set; } // giá trị mới của đối tượng
        public string Description { get; set; } // 
        public string TargetObject { get; set; } // đối tượng đích
        public string ErrorMessage { get; set; } // 
    }
}
