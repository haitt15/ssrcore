import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/ServiceRequests'

export const requestlist = {
  namespaced: true,
  state: {
    _listOfRequest: [
      {
        ticketId: 'SSR001',
        serviceNm: 'Muon sach',
        content: 'muonsachswd',
        duedatetime: '30/6/2020',
        status: 'Finished'
      },
      {
        ticketId: 'SSR002',
        serviceNm: 'Muon phong hoc',
        content: 'muon phong hoc swd',
        duedatetime: '30/6/2020',
        status: 'Expired'
      },
      {
        ticketId: 'SSR003',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130086',
        duedatetime: '15/7/2020',
        status: 'Rejected'
      },
      {
        ticketId: 'SSR004',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'In Progress'
      },
      {
        ticketId: 'SSR005',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Waiting'
      },
      {
        ticketId: 'SSR006',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Expired'
      },
      {
        ticketId: 'SSR007',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Finished'
      },
      {
        ticketId: 'SSR008',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Finished'
      },
      {
        ticketId: 'SSR009',
        serviceNm: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Waiting'
      },
      {
        ticketId: 'SSR010',
        serviceNm: 'Cap lai the sinh vien',
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
