import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/ServiceRequests'

export const requestlist = {
  namespaced: true,
  state: {
    _listOfRequest: []
  },
  getters: {
    _getListOfRequest (state) {
      return state._listOfRequest
    }
  },
  mutations: {
    _setRequestListOfDepartment (state, _listOfRequest) {
      state._listOfRequest = _listOfRequest
    }
  },
  actions: {
    _getAllRequestOfDepartment (context, status) {
      var departmentId = JSON.parse(localStorage.getItem('UserInfo')).departmentId
      return SSRCore.get(API_URL, {
        departmentId: departmentId,
        status: status
      }).then(
        response => {
          context.commit('_setRequestListOfDepartment', response.data.data)
          return response.data.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
