import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/ServiceRequests'

export const requestlist = {
  namespaced: true,
  state: {
    _listOfRequest: [
      {
        ticketid: 'SSR001',
        servicename: 'Muon sach',
        content: 'muonsachswd',
        duedatetime: '30/6/2020',
        status: 'Finished'
      },
      {
        ticketid: 'SSR002',
        servicename: 'Muon phong hoc',
        content: 'muon phong hoc swd',
        duedatetime: '30/6/2020',
        status: 'Expired'
      },
      {
        ticketid: 'SSR003',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130086',
        duedatetime: '15/7/2020',
        status: 'Rejected'
      },
      {
        ticketid: 'SSR004',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'In Progress'
      },
      {
        ticketid: 'SSR005',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Waiting'
      },
      {
        ticketid: 'SSR006',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Expired'
      },
      {
        ticketid: 'SSR007',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Finished'
      },
      {
        ticketid: 'SSR008',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Finished'
      },
      {
        ticketid: 'SSR009',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Waiting'
      },
      {
        ticketid: 'SSR010',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Waiting'
      }
    ]
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
    _getAllRequestOfDepartment (context) {
      var departmentId = JSON.parse(localStorage.getItem('UserInfo')).departmentId
      return SSRCore.get(API_URL, {
        departmentId: departmentId
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
