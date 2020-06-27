import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import AdminPage from '../views/AdminPage.vue'
import StaffPage from '../views/StaffPage.vue'
import StudentPage from '../views/StudentPage.vue'
// import RequestDetails from '../views/RequestDetails.vue'
import Service from '../views/Service.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/home',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/admin',
    name: 'Admin',
    component: AdminPage
  },
  {
    path: '/staff',
    name: 'Staff',
    component: StaffPage
  },
  {
    path: '/student',
    name: 'Student',
    component: StudentPage
  },
  // {
  //   path: '/request',
  //   name: 'RequestDetails',
  //   component: RequestDetails
  // },
  {
    path: '/service',
    name: 'Service',
    component: Service
  }
]
const router = new VueRouter({
  routes
})

// router.beforeEach((to, from, next) => {
//   const publicPages = ['Login', 'Register']
//   const ManagerPages = ['Home', 'About', 'Admin']
//   const StaffPages = ['Home', 'About', 'Staff']
//   const StudentPages = ['Home', 'About', 'Student']
//   const authRequired = !publicPages.includes(to.name)
//   const user = JSON.parse(localStorage.getItem('UserInfo'))
//   console.log('this.user', user)
//   // trying to access a restricted page + not logged in
//   // redirect to login page
//   if (user === null) {
//     // chưa đăng nhập
//     if (authRequired) {
//       next('/login')
//     } else {
//       next()
//     }
//   } else {
//     // đã đăng nhập
//     if (user.roles === 'Manager' && ManagerPages.includes(to.name)) {
//       next()
//     } else if (user.roles === 'Staff' && StaffPages.includes(to.name)) {
//       next()
//     } else if (user.roles === 'Student' && StudentPages.includes(to.name)) {
//       next()
//     } else {
//       next('/home')
//     }
//   }
// })

export default router
