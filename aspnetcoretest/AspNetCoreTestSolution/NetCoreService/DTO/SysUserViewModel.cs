﻿using System;

namespace NetCoreService.DTO
{
    public class SysUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int? Nation { get; set; }
        public DateTime? BirthDay { get; set; }
        public string CardId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string WeChat { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
    }
}
