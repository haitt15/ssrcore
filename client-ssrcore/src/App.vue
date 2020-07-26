<template>
  <v-app>
    <navigation v-if="!['Login', 'Register'].includes($route.name)"></navigation>
    <router-view v-else></router-view>

     <!-- Sizes your content based upon application components -->
  <v-content v-if="!['Login', 'Register'].includes($route.name)">
    <!-- Provides the application the proper gutter -->
    <v-container fluid>
      <!-- <breadcrumbs :items="_getBreadcrumbs" ></breadcrumbs> -->
      <!-- If using vue-router -->
      <router-view></router-view>
    </v-container>
  </v-content>

    <!-- <ssrcore-footer> </ssrcore-footer> -->
  </v-app>
</template>
<script>
import navigation from './components/Navigation'
// import breadcrumbs from './components/Breadcrumbs'
// import Footer from '@/components/Footer'
import { mapGetters } from 'vuex'
export default {
  computed: {
    ...mapGetters([''])
  },
  components: {
    navigation
    // 'ssrcore-footer': Footer
    // breadcrumbs
  },
  data () {
    return {
    }
  },
  mounted () {
    // this.initApp()
  },
  methods: {
    // ...mapMutations(['_addBreadcrubs']),
    // initApp () {
    //   this._addBreadcrubs({
    //     text: 'Dashboard',
    //     disabled: false,
    //     href: '#/student'
    //   })
    // }
  }
}
</script>
<style>

/* Change Autocomplete styles in Chrome */
input:-webkit-autofill,
input:-webkit-autofill:hover,
input:-webkit-autofill:focus,
textarea:-webkit-autofill,
textarea:-webkit-autofill:hover,
textarea:-webkit-autofill:focus,
select:-webkit-autofill,
select:-webkit-autofill:hover,
select:-webkit-autofill:focus {
  -webkit-text-fill-color: #000000;
  -webkit-box-shadow: 0 0 0px 1000px #ffffff inset;
  transition: background-color 5000s ease-in-out 0s;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-bottom: 5%;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
/* .container {
  margin: 0 !important;
  padding: 0 !important;
  position: relative;
  top: 7vh;
  height: fit-content;
} */
.v-application a {
  text-decoration: none;
}
</style>
