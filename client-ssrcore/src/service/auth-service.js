import axios from 'axios'

const API_URL = 'https://localhost:44396/api/Authenticate'
const userX = {
  username: 'test',
  password: 'test'
}
class AuthService {
  login (user) {
    return new Promise((resolve, reject) => {
      if (
        userX.username === user.username &&
        userX.password === user.password
      ) {
        resolve()
        return userX
      }
    })
    // return axios
    //   .post(API_URL + '/Login', {
    //     username: user.username,
    //     password: user.password
    //   })
    //   .then(response => {
    //     if (response.data.token) {
    //       localStorage.setItem('UserInfo', JSON.stringify(response.data))
    //     }

    //     return response.data
    //   })
  }

  logout () {
    localStorage.removeItem('UserInfo')
  }

  register (user) {
    return axios.post(API_URL + '/Register', {
      username: user.username,
      email: user.email,
      password: user.password
    })
  }
}

export default new AuthService()
