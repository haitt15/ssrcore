import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/Users/Staffs'

export const staff = {
  namespaced: true,
  state: {
    _staffList: []
  },
  getters: {},
  mutations: {
    _setStaffList (state, _staffList) {
      state._staffList = _staffList
    },
    _addStaffMutations (state, staff) {
      state._staffList.push(staff)
    },
    async _updateStaffMutations (state, staff) {
      const staffObj = await state._staffList.find(x => x.Id === staff.Id)
      staffObj.fullName = staff.fullName
      staffObj.address = staff.address
      staffObj.phoneNumber = staff.phoneNumber
      staffObj.departmentId = staff.departmentId
      staffObj.departmentNm = staff.departmentNm
    },
    async _deleteStaffMutations (state, Id) {
      await state._staffList.pop(x => x.Id === Id)
    }
  },
  actions: {
    _getStaffList (context, obj) {
      return SSRCore.get(API_URL, obj).then(
        response => {
          context.commit('_setStaffList', response.data.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _addStaff (context, obj) {
      return SSRCore.post(API_URL, obj).then(
        response => {
          context.commit('_addStaffMutations', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _updateStaff (context, obj) {
      return SSRCore.put(API_URL + obj.Id, obj).then(
        response => {
          context.commit('_updateStaffMutations', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _deleteStaff (context, obj) {
      return SSRCore.delete(API_URL + obj.Id).then(
        response => {
          context.commit('_deleteStaffMutations', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
