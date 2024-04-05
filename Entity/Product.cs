using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ThiToeic.Entity
{
    public partial class BaiLam
    {
        public BaiLam()
        {
            // KetQuas = new HashSet<>();
        }

        public int IdbaiLam { get; set; }
        public TimeSpan? ThoiGian { get; set; }
        public DateTime? NgayThi { get; set; }
        public int IddeThi { get; set; }
        public int IdnguoiDung { get; set; }

        // public virtual DeThi IddeThiNavigation { get; set; } = null!;
        // public virtual NguoiDung IdnguoiDungNavigation { get; set; } = null!;
        // public virtual ICollection<KetQua> KetQuas { get; set; }

        // Order.find({

    }
}
