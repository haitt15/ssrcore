import Vue from 'vue'
import App from './App.vue'
import router from './router/router'
import store from './store/store'
import VeeValidate from 'vee-validate'
import vuetify from '@/plugins/vuetify'

import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/js/all.js'

Vue.use(VeeValidate)

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount('#app')
