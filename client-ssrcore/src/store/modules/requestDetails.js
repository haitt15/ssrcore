import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/ServiceRequests'

export const requestDetails = {
  namespaced: true,
  state: {
    _requestService: {}
  },
  getters: {},
  mutations: {
    _setRequestService (state, _requestService) {
      state._requestService = _requestService
    },
    _updateRequestServiceMutations (state, requestService) {
      state._requestService.status = requestService.status
      state._requestService.expiredDay = requestService.expiredDay
      state._requestService.StaffId = requestService.StaffId
      // tim kiem staff theo username va fullname
    }
  },
  actions: {
    _getRequestService (context, obj) {
      return SSRCore.get(API_URL, obj).then(
        response => {
          context.commit('_setRequestService', response.data.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _updateRequestService (context, obj) {
      return SSRCore.put(API_URL, obj).then(
        response => {
          context.commit('_updateRequestServiceMutations', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
