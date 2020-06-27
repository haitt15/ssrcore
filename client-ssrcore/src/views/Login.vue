<template>
  <div class="limiter">
    <div
      class="container-login100"
      style="background-image: url('https://daihoc.fpt.edu.vn/media/2020/01/photo-1-1578027492964533493668.jpg');"
    >
      <div class="wrap-login100 p-l-110 p-r-110 p-t-62 p-b-33">
        <div class="login100-form flex-sb flex-w">
          <span class="login100-form-title" style="margin-bottom:5vh">SIGN IN</span>
          <v-form ref="form" lazy-validation>
            <v-text-field
              ref="username"
              v-model="user.username"
              :counter="50"
              :rules="usernameRules"
              label="Username"
              outlined
              clearable
              required
              placeholder="Username.."
            ></v-text-field>
            <v-text-field
              ref="password"
              v-model="user.password"
              :rules="passwordRules"
              label="Password"
              type="password"
              outlined
              clearable
              required
              placeholder="Password.."
            ></v-text-field>
            <v-btn class="mr-4 login100-form-btn" @click="doLogin()">SIGN IN</v-btn>
          </v-form>
          {{ message}}
          <div class="container-login100-form-btn m-t-17">
            <span class="w-full text-center txt2 p-b-20">OR SIGN IN WITH</span>
            <div class="btn-face m-b-20" style="color: #ffff; margin-right:1.2vw">
              <i class="fab fa-facebook"></i> Facebook
            </div>

            <div class="btn-google m-b-20" @click="signInWithGoogle()">
              <img src="../images/icons/icon-google.png" />
              Google
            </div>
          </div>

          <div class="w-full text-center p-t-5">
            <span class="txt2">Not a member?</span>

            <a href="#" class="txt2 bo1">Sign up now</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import firebase from 'firebase/app'
import { mapActions } from 'vuex'
export default {
  data () {
    return {
      user: {
        username: '',
        password: ''
      },
      usernameRules: [
        v => !!v || 'Username is required',
        // v => (v && v.length >= 8) || 'Username must be more than 8 characters',
        v => (v && v.length < 50) || 'Username must be less than 50 characters'
      ],
      passwordRules: [v => !!v || 'Password is required'
        // v => (v && v.length >= 8) || 'Username must be more than 8 characters'
      ],
      message: ''
    }
  },
  mounted () {
    // console.log('this.user', this.user)
  },
  methods: {
    ...mapActions('auth', ['_login', '_loginWithGoogle']),
    doLogin () {
      const valid = this.$refs.form.validate()
      if (valid) {
        if (this.user.username && this.user.password) {
          this._login(this.user).then(
            response => {
              this.$router.push('home')
            },
            error => {
              this.user.username = ''
              this.user.password = ''
              this.message = 'username or password invalid!'
              console.log('error', error)
            }
          )
        } // end this.user.username && this.user.password
      } // end valid
    },
    signInWithGoogle () {
      const self = this
      const provider = new firebase.auth.GoogleAuthProvider()
      console.log(provider)
      firebase
        .auth()
        .signInWithPopup(provider)
        .then(result => {
          firebase.auth().currentUser.getIdToken(true).then(function (idToken) {
            console.log('this.token', idToken)
            // Send token to your backend via HTTPS
            self._loginWithGoogle(idToken).then(
              response => {
                console.log('this.result', result)
                self.$router.push('admin')
              },
              error => {
                console.log('error', error)
              }
            )
            // ...
          }).catch(function (error) {
            // Handle error
            console.log('error: ', error)
          })
        })
        .catch(err => {
          console.log('this.err', err)
        })
    }
  }
}
</script>

<style scoped>
.v-form {
  width: 100%;
}
@import url("../assets/css/main.css");
@import url("../assets/css/util.css");
</style>
