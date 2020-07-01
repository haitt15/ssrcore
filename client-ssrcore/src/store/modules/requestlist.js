import axios from 'axios'
const API_URL = 'https://localhost:44312/api/v1/Dashboard'

const user = JSON.parse(localStorage.getItem('UserInfo'))
const initialState = user
  ? {
    status: {
      loggedIn: true
    },
    user
  }
  : {
    status: {
      loggedIn: false
    },
    user: null
  }

export const requestlist = {
  namespaced: true,
  state: {
    initialState,
    listOfRequest: [
      {
        ticketid: 'SSR001',
        servicename: 'Muon sach',
        content: 'muonsachswd',
        duedatetime: '30/6/2020',
        status: 'Success'
      },
      {
        ticketid: 'SSR002',
        servicename: 'Muon phong hoc',
        content: 'muon phong hoc swd',
        duedatetime: '30/6/2020',
        status: 'Success'
      },
      {
        ticketid: 'SSR003',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130086',
        duedatetime: '15/7/2020',
        status: 'Failed'
      },
      {
        ticketid: 'SSR004',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Failed'
      },
      {
        ticketid: 'SSR005',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Success'
      },
      {
        ticketid: 'SSR006',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Failed'
      },
      {
        ticketid: 'SSR007',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Failed'
      },
      {
        ticketid: 'SSR008',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Failed'
      },
      {
        ticketid: 'SSR009',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Pending'
      },
      {
        ticketid: 'SSR010',
        servicename: 'Cap lai the sinh vien',
        content: 'the sinh vien se130088',
        duedatetime: '15/7/2020',
        status: 'Pending'
      }
    ]
  },
  getters: {
    _getListOfRequest (state) {
      return state.listOfRequest
    }
  },
  mutations: {
    _loginSuccess (state, user) {
      state.status.loggedIn = true
      state.user = user
    },
    _loginFailure (state) {
      state.status.loggedIn = false
      state.user = null
    },
    _logout (state) {
      state.status.loggedIn = false
      state.user = null
    },
    _registerSuccess (state) {
      state.status.loggedIn = false
    },
    _registerFailure (state) {
      state.status.loggedIn = false
    }
  },
  actions: {
    _login (context, user) {
      return axios
        .post(API_URL, {
          username: user.username,
          password: user.password
        })
        .then(
          response => {
            if (response.data.token) {
              localStorage.setItem('UserInfo', JSON.stringify(response.data))
              context.commit('_loginSuccess', user)
            }
            return response.data
          },
          error => {
            context.commit('_loginFailure')
            return Promise.reject(error)
          }
        )
    },
    _loginWithGoogle (context, idToken) {
      return axios
        .post(API_URL + '/Google', {
          IdToken: idToken
        })
        .then(
          response => {
            if (response.data.token) {
              localStorage.setItem('UserInfo', JSON.stringify(response.data))
              context.commit('_loginSuccess', user)
            }
            return response.data
          },
          error => {
            context.commit('_loginFailure')
            return Promise.reject(error)
          }
        )
    },
    _logout ({ commit }) {
      localStorage.removeItem('UserInfo')
      commit('_logout')
    },
    _register (context, user) {
      return axios
        .post(API_URL + '/Register', {
          username: user.username,
          email: user.email,
          password: user.password
        })
        .then(
          response => {
            context.commit('_registerSuccess')
            return Promise.resolve(response.data)
          },
          error => {
            context.commit('_registerFailure')
            return Promise.reject(error)
          }
        )
    }
  }
}
