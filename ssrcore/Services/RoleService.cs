using AutoMapper;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Role> CreateRole(RoleModel model)
        {
            var entity = _mapper.Map<Role>(model);
            entity.DelFlg = false;
            entity.InsBy = Constants.Admin.ADMIN;
            entity.InsDatetime = DateTime.Now;
            entity.UpdBy = Constants.Admin.ADMIN;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.RoleRepository.Create(entity);
            await _unitOfWork.Commit();
            return entity;
        }

        public string GetRole(Users user)
        {
            var role = _unitOfWork.RoleRepository.GetRole(user);
            return role.RoleNm;
        }
    }
}
