<template>
  <v-card v-bind="$attrs" :class="classes" class="v-card--material pa-3">
    <div class="d-flex grow flex-wrap">
      <v-avatar
        v-if="avatar"
        size="128"
        class="mx-auto v-card--material__avatar elevation-6"
        color="grey"
      >
        <v-img :src="avatar" />
      </v-avatar>

      <v-sheet
        v-else
        :class="{
          'pa-7': !$slots.image
        }"
        :color="colorTable"
        :max-height="icon ? 90 : undefined"
        :width="icon ? 'auto' : '100%'"
        elevation="6"
        class="text-start v-card--material__heading mb-n6"
        dark
      >
        <slot v-if="$slots.heading" name="heading" />

        <slot v-else-if="$slots.image" name="image" />

        <div
          v-else-if="title && !icon"
          class="display-1 font-weight-light"
          v-text="title"
        />

        <v-icon v-else-if="icon" size="32" v-text="icon" />

        <div v-if="text" class="headline font-weight-thin" v-text="text" />
      </v-sheet>

      <div v-if="$slots['after-heading']" class="ml-6">
        <slot name="after-heading" />
      </div>

      <div v-else-if="icon && title" class="ml-4">
        <div class="card-title font-weight-light" v-text="title" />
      </div>
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
    </div>
    <v-card>
      <v-tabs
        v-model="tab"
        :background-color="colorTable"
        dark
        icons-and-text
        @change="changeTab()"
      >
        <v-tabs-slider></v-tabs-slider>
        <v-tab v-for="i in tabs" :key="i" :href="`#tab-${i.title}`">
          {{ i.title }}
        </v-tab>
        <v-tab-item v-for="i in tabs" :key="i" :value="'tab-' + i.title">
          <v-card flat>
            <v-data-table
              :headers="headerTable"
              :items="dataTable"
              :items-per-page="5"
              :search="search"
            >
              <template v-slot:item.ticketId="{ item }">
                <router-link
                  :to="{ path: 'request', query: { ticketId: item.ticketId } }"
                  >{{ item.ticketId }}</router-link
                >
              </template>
              <template v-slot:item.beginDateTime="{ item }">
                {{ item.beginDateTime | formatDatetime }}
              </template>
              <template v-slot:item.dueDateTime="{ item }">
                {{ item.dueDateTime | formatDatetime }}
              </template>
              <template v-slot:item.status="{ item }">
                <v-chip :color="getColor(item.status)" dark>
                  {{ item.status }}
                </v-chip>
              </template>
              <template v-slot:item.actions="{ item }">
                <v-icon small class="mr-2" @click="clickToEditRequest(item)"
                  >mdi-pencil</v-icon
                >
              </template>
            </v-data-table>
          </v-card>
        </v-tab-item>
      </v-tabs>
    </v-card>
    <slot />

    <template v-if="$slots.actions">
      <v-divider class="mt-2" />

      <v-card-actions class="pb-0">
        <slot name="actions" />
      </v-card-actions>
    </template>
  </v-card>
</template>

<script>
import moment from 'moment'
import { mapGetters, mapActions } from 'vuex'
export default {
  name: 'MaterialCard',
  data () {
    return {
      search: '',
      colorTable: 'black',
      tab: 'All',
      tabs: [
        {
          title: 'All'
        },
        {
          title: 'Expired'
        },
        {
          title: 'Waiting'
        },
        {
          title: 'Finished'
        },
        {
          title: 'Rejected'
        },
        {
          title: 'In-Progress'
        }
      ]
    }
  },
  methods: {
    ...mapActions('requestlist', ['_getAllRequestOfDepartment']),
    getColor (status) {
      if (status === 'Finished') return 'green'
      else if (status === 'Rejected') return 'red'
      else if (status === 'Expired') return 'orange'
      else if (status === 'Waiting') return 'amber'
      else if (status === 'In-Progress') return 'blue'
      else return 'black'
    },
    clickToEditRequest (request) {
      this.$router.push({
        path: '/request',
        query: { ticketId: request.ticketId },
        params: { ticketId: request.ticketId }
      })
    },
    changeTab () {
      console.log('Duong ne')
      console.log(this.tab)
      console.log(this.tab.split('tab-'))
      var x = this.tab.split('tab-')[1]
      this.colorTable = this.getColor(x)
      switch (this.tab.split('tab-')[1]) {
        case 'All': {
          this._getAllRequestOfDepartment()
          break
        }
        case 'Expired': {
          this._getAllRequestOfDepartment('Expired')
          break
        }
        case 'In-Progress': {
          this._getAllRequestOfDepartment('In-Progress')
          break
        }
        case 'Waiting': {
          this._getAllRequestOfDepartment('Waiting')
          break
        }
        case 'Finished': {
          this._getAllRequestOfDepartment('Finished')
          break
        }
        case 'Rejected': {
          this._getAllRequestOfDepartment('Rejected')
          break
        }
      }
    }
  },
  filters: {
    formatDatetime (value) {
      return moment(value).format('DD/MM/YYYY')
    }
  },
  props: {
    headerTable: {
      type: Array,
      default: undefined
    },
    dataTable: {
      type: Array,
      default: undefined
    },

    avatar: {
      type: String,
      default: ''
    },
    color: {
      type: String,
      default: 'success'
    },
    icon: {
      type: String,
      default: undefined
    },
    image: {
      type: Boolean,
      default: false
    },
    text: {
      type: String,
      default: ''
    },
    title: {
      type: String,
      default: ''
    }
  },

  computed: {
    ...mapGetters('requestlist', ['_getListOfRequest']),
    classes () {
      return {
        'v-card--material--has-heading': this.hasHeading
      }
    },
    hasHeading () {
      return Boolean(this.$slots.heading || this.title || this.icon)
    },
    hasAltHeading () {
      return Boolean(this.$slots.heading || (this.title && this.icon))
    }
  }
}
</script>

<style lang="sass">
.v-card--material
  margin-top: 5vh
  margin-bottom: 5vh
  &__avatar
    position: relative
    top: -64px
    margin-bottom: -32px

  &__heading
    position: relative
    top: -40px
    transition: .3s ease
    z-index: 1
</style>
