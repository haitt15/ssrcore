<template>
  <nav>
    <v-app-bar light flat dense app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>Student Service Request</v-toolbar-title>

      <v-spacer></v-spacer>

      <!-- <v-menu
        :open-on-click="openOnClick"
        :close-on-content-click="closeOnContentClick"
        :offset-y="offsetY"
      >
        <template v-slot:activator="{ on }">
          <v-badge :content="numberNoti" :value="numberNoti" color="green" left overlap>
            <v-btn depressed small light v-on="on">
              <i class="fas fa-bell" style="font-size:2.8vmin"></i>
            </v-btn>
          </v-badge>
        </template>
        <v-card class="mx-auto" max-width="400" tile>
          <v-list dense shaped flat rounded two-line>
            <v-list-item-group color="primary">
              <v-list-item v-for="(noti, i) in notifications" :key="i">
                <v-icon>mdi-account-arrow-right</v-icon>
                <v-list-item-content>
                  <v-list-item-title>{{noti.title}}</v-list-item-title>
                  <v-list-item-subtitle>{{noti.message}}</v-list-item-subtitle>
                </v-list-item-content>
                <v-icon>mdi-account</v-icon>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-menu> -->
      <v-menu
        :open-on-click="openOnClick"
        :close-on-content-click="closeOnContentClick"
        :offset-y="offsetY"
        :offset-x="offsetX"
      >
        <template v-slot:activator="{ on }">
          <v-btn depressed small v-on="on">
            <i class="fas fa-user" style="font-size:2.8vmin"></i>
          </v-btn>
        </template>
        <v-card>
          <v-list>
            <v-list-item>
              <v-list-item-avatar>
                <img src="https://cdn.vuetifyjs.com/images/john.jpg" :alt="currentUser.fullName" />
              </v-list-item-avatar>

              <v-list-item-content>
                <v-list-item-title>{{currentUser.fullName}}</v-list-item-title>
                <v-list-item-subtitle>{{currentUser.departmentNm}}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
          <v-divider></v-divider>
          <v-card class="mx-auto" max-width="400" tile>
            <v-list dense shaped flat rounded>
              <v-list-item-group color="primary">
                <v-list-item @click="logout">
                  <v-list-item-title>
                    Logout
                    <v-icon>mdi-logout</v-icon>
                  </v-list-item-title>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-card>
        </v-card>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer app v-model="drawer">
      <v-list dense nav class="py-0">
        <v-list-item two-line class="px-0">
          <v-list-item-avatar>
            <img src="https://randomuser.me/api/portraits/men/81.jpg" />
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>{{currentUser.fullName}}</v-list-item-title>
            <v-list-item-subtitle>{{currentUser.departmentNm}}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list-group value="true">
          <template v-slot:activator>
            <v-list-item-icon>
              <v-icon>mdi-view-dashboard</v-icon>
            </v-list-item-icon>
            <v-list-item-title>
              <router-link :to="{ path: 'dashboard'}">Dashboard</router-link>
            </v-list-item-title>
          </template>

          <v-list-item>
            <v-list-item-title>
              <router-link :to="{ path: 'requestList'}">Request</router-link>
            </v-list-item-title>
               <v-list-item-icon>
              <v-icon>mdi-account-question</v-icon>
            </v-list-item-icon>
          </v-list-item>
          <v-list-item v-if="currentUser.role === 'Manager'">
            <v-list-item-title>
              <router-link :to="{ path: 'service'}">Service</router-link>
            </v-list-item-title>
            <v-list-item-icon>
              <v-icon>mdi-book</v-icon>
            </v-list-item-icon>
          </v-list-item>
          <v-list-item v-if="currentUser.role === 'Manager'">
            <v-list-item-title>
              <router-link :to="{ path: 'staff'}">Staff</router-link>
            </v-list-item-title>
            <v-list-item-icon>
              <v-icon>mdi-account-group</v-icon>
            </v-list-item-icon>
          </v-list-item>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

<script>
import { mapActions } from 'vuex'
export default {
  data () {
    return {
      currentUser: {},
      bars: { dark: true },
      notifications: [
        {
          title: 'Title 1',
          message: 'message 1'
        },
        {
          title: 'Title 2',
          message: 'message 2'
        },
        {
          title: 'Title 3',
          message: 'message 3'
        },
        {
          title: 'Title 4',
          message: 'message 4'
        }
      ],
      openOnClick: true,
      closeOnContentClick: true,
      offsetY: true,
      offsetX: true,
      numberNoti: 0,
      drawer: false,
      right: false,
      permanent: true,
      expandOnHover: true,
      requests: [
        ['XXX', 'mdi-book-open'],
        ['XXX', 'mdi-read']
      ],
      services: [
        ['Create', 'mdi-plus'],
        ['Read', 'mdi-read'],
        ['Update', 'mdi-update'],
        ['Delete', 'mdi-delete']
      ]
    }
  },
  mounted () {
    this.numberNoti = this.notifications.length
    this.currentUser = JSON.parse(localStorage.getItem('UserInfo'))
  },
  methods: {
    ...mapActions('auth', ['_logout']),
    onSidebarChanged () {},
    logout () {
      this._logout()
      this.$router.push('/login')
    }
  }
}
</script>
<style lang="css" scoped>
.v-list-item__content {
  width: 18vw;
}
.v-list-item__icon {
  margin-right: 0px !important;
}
.v-card > .v-card__progress + :not(.v-btn):not(.v-chip),
.v-card > :first-child:not(.v-btn):not(.v-chip) {
  border-radius: 0;
}
</style>
