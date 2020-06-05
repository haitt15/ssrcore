import Vue from 'vue'
import App from './App.vue'
import router from './router/router'
import store from './store/store'
import VeeValidate from 'vee-validate'
import vuetify from '@/plugins/vuetify'
import firebase from 'firebase'
import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/js/all.js'

Vue.use(VeeValidate)

var firebaseConfig = {
  apiKey: 'AIzaSyB1764LaVn8f0PxPTPfQLuc3bf472E6xHI',
  authDomain: 'student-service-request-app.firebaseapp.com',
  databaseURL: 'https://student-service-request-app.firebaseio.com',
  projectId: 'student-service-request-app',
  storageBucket: 'student-service-request-app.appspot.com',
  messagingSenderId: '1013267920598',
  appId: '1:1013267920598:web:c30aedbe313add63e07b77'
}
// Initialize Firebase
firebase.initializeApp(firebaseConfig)

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount('#app')
