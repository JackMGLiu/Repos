using AutoMapper;
using NetCoreModel;

namespace NetCoreService.DTO
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<SysUser, SysUserViewModel>();
            CreateMap<SysUserViewModel, SysUser>();

            CreateMap<SysMenu, SysMenuViewModel>();
            CreateMap<SysMenuViewModel, SysMenu>();

            CreateMap<DictDetail, DictDetailViewModel>();
            CreateMap<DictDetailViewModel, DictDetail>();
        }
        
        //protected void Configure()
        //{
        //    CreateMap<SysUser, SysUserViewModel>();
        //    CreateMap<SysUserViewModel, SysUser>();
        //}
    }
}
