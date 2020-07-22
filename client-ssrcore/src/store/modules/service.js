import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/Services'

export const service = {
  namespaced: true,
  state: {
    _serviceList: []
  },
  getters: {},
  mutations: {
    _setServiceList (state, _serviceList) {
      state._serviceList = _serviceList
    },
    _addServiceMutation (state, service) {
      state._serviceList.push(service)
    },
    async _updateServiceMutation (state, service) {
      const editObj = await state._serviceList.find(x => x.serviceId === service.serviceId)
      editObj.serviceNm = service.serviceNm
      editObj.description = service.description
      editObj.processMaxDay = service.processMaxDay
      editObj.formLink = service.formLink
      editObj.sheetLink = service.sheetLink
    },
    async _deleteServiceMutation (state, serviceId) {
      await state._serviceList.pop(x => x.serviceId === serviceId)
    }
  },
  actions: {
    _getAllService (context) {
      return SSRCore.get(API_URL, {
      }).then(
        response => {
          context.commit('_setServiceList', response.data.data)
          return response.data.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _addService (context, obj) {
      debugger
      return SSRCore.post(API_URL, obj).then(
        response => {
          context.commit('_addServiceMutation', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _updateService (context, obj) {
      return SSRCore.put(API_URL + '/' + obj.serviceId, obj).then(
        response => {
          context.commit('_updateServiceMutation', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _deleteService (context, obj) {
      return SSRCore.delete(API_URL + '/' + obj.serviceId).then(
        response => {
          context.commit('_deleteServiceMutation', response.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
