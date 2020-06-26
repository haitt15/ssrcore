﻿using AutoMapper;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DepartmentModel> CreateDepartment(DepartmentModel department)
        {
            var entity = _mapper.Map<Department>(department);
            await _unitOfWork.DepartmentRepository.Create(entity);
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.DepartmentRepository.GetById(entity.DepartmentId);
            return modelToReturn;
        }

        public async Task<bool> DeleteDepartment(string departmentId)
        {
            var department = await _unitOfWork.DepartmentRepository.GetById(departmentId);
            var entity = _mapper.Map<Department>(department);
            if (department != null)
            {
                _unitOfWork.DepartmentRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<object> GetAllDepartment(SearchDepartmentModel model)
        {
            var departments = await _unitOfWork.DepartmentRepository.Get(model);
            dynamic result;
            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var dep in departments)
                {
                    Dictionary<string, object> dictionnary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "DepartmentId":
                                dictionnary.Add("DepartmentId", dep.DepartmentId);
                                break;
                            case "DepartmentNm":
                                dictionnary.Add("DepartmentNm", dep.DepartmentNm);
                                break;
                            case "Hotline":
                                dictionnary.Add("Hotline", dep.Hotline);
                                break;
                            case "ManagerId":
                                dictionnary.Add("ManagerId", dep.ManagerId);
                                break;
                            case "Manager":
                                dictionnary.Add("Manager", dep.Manager);
                                break;
                            case "RoomNum":
                                dictionnary.Add("RoomNum", dep.RoomNum);
                                break;
                        }
                    }
                    listModel.Add(dictionnary);
                }
                result = listModel;
            }
            else
            {
                result = departments;
            }

            return new {
                data = result,
                totalCount = departments.TotalCount,
                totalPages = departments.TotalPages
            };

        }


        public async Task<DepartmentModel> GetDepartment(string departmentId)
        {
            var entity = await _unitOfWork.DepartmentRepository.GetById(departmentId);
            if(entity == null)
            {
                throw new AppException("Cannot find " + departmentId);
            }
            return entity;
        }


        public async Task<DepartmentModel> UpdateDepartment(string departmentId, DepartmentModel department)
        {
            var entity = await _unitOfWork.DepartmentRepository.GetById(departmentId);
            entity.DepartmentNm = department.DepartmentNm != null ? department.DepartmentNm : entity.DepartmentNm;
            entity.Hotline = department.Hotline != null ? department.Hotline : entity.Hotline;
            entity.RoomNum = department.RoomNum != null ? department.RoomNum : entity.RoomNum;
            entity.ManagerId = department.ManagerId != null ? department.ManagerId : entity.ManagerId;
            entity.DelFlg = false;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.DepartmentRepository.GetById(departmentId);
            return modelToReturn;
        }
    }
}
