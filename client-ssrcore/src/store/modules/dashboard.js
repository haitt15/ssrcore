import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/ServiceRequests'

// const user = JSON.parse(localStorage.getItem('UserInfo'))

export const dashboard = {
  namespaced: true,
  state: {
    _totalFinishedRequest: 0,
    _totalWaitingRequest: 0,
    _totalRejectedRequest: 0,
    _totalInProgressRequest: 0
  },
  getters: {
    _getListTotalTypeRequest (state) {
      return state.listTotalTypeRequest
    },
    _getFinishedRequest (state) {
      return state._totalFinishedRequest
    },
    _getTotalWaitingRequest (state) {
      return state._totalWaitingRequest
    },
    _getTotalRejectedRequest (state) {
      return state._totalRejectedRequest
    },
    _getTotalInProgressRequest (state) {
      return state._totalInProgressRequest
    }
  },
  mutations: {
    _setFinishedRequest (state, _totalFinishedRequest) {
      state._totalFinishedRequest = _totalFinishedRequest
    },
    _setTotalWaitingRequest (state, _totalWaitingRequest) {
      state._totalWaitingRequest = _totalWaitingRequest
    },
    _setTotalRejectedRequest (state, _totalRejectedRequest) {
      state._totalRejectedRequest = _totalRejectedRequest
    },
    _setTotalInProgressRequest (state, _totalInProgressRequest) {
      state._totalInProgressRequest = _totalInProgressRequest
    }
  },
  actions: {
    _getAllRequestOfDepartmentDashboard (context) {
      var departmentId = JSON.parse(localStorage.getItem('UserInfo'))
        .departmentId
      return SSRCore.get(API_URL, {
        departmentId: departmentId
      }).then(
        response => {
          let totalInprogress = 0
          let totalWaiting = 0
          let totalRejected = 0
          let totalFinished = 0
          for (let index = 0; index < response.data.data.length; index++) {
            const element = response.data.data[index]
            switch (element.status) {
              case 'In-Progress': {
                totalInprogress = totalInprogress + 1
                break
              }
              case 'Waiting': {
                totalWaiting = totalWaiting + 1
                break
              }
              case 'Rejected': {
                totalRejected = totalRejected + 1
                break
              }
              case 'Finished': {
                totalFinished = totalFinished + 1
                break
              }
            }
            context.commit('_setTotalWaitingRequest', totalWaiting)
            context.commit('_setTotalRejectedRequest', totalRejected)
            context.commit('_setTotalInProgressRequest', totalInprogress)
            context.commit('_setFinishedRequest', totalFinished)
          }
          return response.data.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
