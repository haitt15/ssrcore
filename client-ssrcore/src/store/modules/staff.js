import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/Users/Staffs'
const API_URL_1 = '/api/v1/Users/'

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
    async _deleteStaffMutations (state, username) {
      const index = state._staffList.findIndex(x => x.username === username)
      await state._staffList.splice(index, 1)
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
      return SSRCore.put(API_URL_1 + obj.username, obj).then(
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
      return SSRCore.delete(API_URL_1 + obj.username).then(
        response => {
          context.commit('_deleteStaffMutations', obj.username)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
