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
            <a href="#" class="btn-face m-b-20" style="color: #ffff">
              <i class="fab fa-facebook"></i> Facebook
            </a>

            <a href="#" class="btn-google m-b-20" style="margin-left: 1.2vw">
              <img src="../images/icons/icon-google.png" />
              Google
            </a>
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
        v => (v && v.length < 50) || 'Username must be less than 50 characters'
      ],
      passwordRules: [v => !!v || 'Password is required'],
      message: ''
    }
  },
  mounted () {
    console.log('this.user', this.user)
    // this.$refs.form.focus()
  },
  methods: {
    ...mapActions('auth', ['login']),
    doLogin () {
      const valid = this.$refs.form.validate()
      console.log('this.x', valid)
      if (valid) {
        if (this.user.username && this.user.password) {
          this.login(this.user).then(
            () => {
              this.$router.push('home')
            },
            error => {
              // this.message = + error;
              this.user.username = ''
              this.user.password = ''
              this.message = 'username or password invalid!'
              console.log('error', error)
            }
          )
        } // end this.user.username && this.user.password
      } // end valid
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
