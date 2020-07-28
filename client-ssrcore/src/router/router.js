import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import AdminPage from '../views/AdminPage.vue'
import StaffPage from '../views/StaffPage.vue'
import StudentPage from '../views/StudentPage.vue'
import Dashboard from '../views/Dashboard'
import Requestlist from '@/views/Requestlist'
import Service from '../views/Service.vue'
import RequestDetails from '../views/RequestDetails.vue'

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
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard
  },
  {
    path: '/requestlist',
    name: 'Requestlist',
    component: Requestlist
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
  },
  {
    path: '/request',
    name: 'RequestDetails',
    component: RequestDetails
  }
]
const router = new VueRouter({
  routes
})

router.beforeEach((to, from, next) => {
  const publicPages = ['Login']
  const ManagerPages = ['Staff', 'Dashboard', 'Requestlist', 'RequestDetails', 'Service']
  const StaffPages = ['Requestlist', 'Dashboard', 'RequestDetails']
  const authRequired = !publicPages.includes(to.name)
  const user = JSON.parse(localStorage.getItem('UserInfo'))
  // trying to access a restricted page + not logged in
  // redirect to login page
  if (user === null) {
    // chưa đăng nhập
    if (authRequired) {
      next('/login')
    } else {
      next()
    }
  } else {
    // đã đăng nhập
    if (user.role === 'Manager' && ManagerPages.includes(to.name)) {
      next()
    } else if (user.role === 'Staff' && StaffPages.includes(to.name)) {
      next()
    } else {
      next('/dashboard')
    }
  }
})

export default router
