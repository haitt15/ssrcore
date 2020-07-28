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
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserModel> CreateStaff(UserModel staff)
        {
            var entity = _mapper.Map<Staff>(staff);
            await _unitOfWork.StaffRepository.Create(entity);
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.StaffRepository.GetByIdToModel(entity.StaffId);
            return modelToReturn;
        }

        public async Task<bool> DeleteStaff(int staffId)
        {
            var entity = await _unitOfWork.StaffRepository.GetByIdToEntity(staffId);
            if (entity != null)
            {
                _unitOfWork.StaffRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<PagedList<UserModel>> GetAllStaffs(SearchStaffModel model)
        {
            var staffs = await _unitOfWork.StaffRepository.Get(model);
            return staffs;
        }

        public async Task<UserModel> GetStaff(int staffId)
        {
            var staff = await _unitOfWork.StaffRepository.GetByIdToModel(staffId);
            if (staff == null)
            {
                throw new AppException("Cannot find " + staffId);
            }
            return staff;
        }

        public async Task<UserModel> UpdateStaff(int staffId, UserModel staff)
        {
            var entity = await _unitOfWork.StaffRepository.GetByIdToEntity(staffId);
            entity.DepartmentId = staff.DepartmentId != null ? staff.DepartmentId : entity.DepartmentId;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.StaffRepository.GetByIdToModel(staffId);
            return modelToReturn;
        }
    }
}
