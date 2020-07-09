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
        :color="color"
        :max-height="icon ? 90 : undefined"
        :width="icon ? 'auto' : '100%'"
        elevation="6"
        class="text-start v-card--material__heading mb-n6"
        dark
      >
        <slot v-if="$slots.heading" name="heading" />

        <slot v-else-if="$slots.image" name="image" />

        <div v-else-if="title && !icon" class="display-1 font-weight-light" v-text="title" />

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
      <v-data-table :headers="headerTable" :items="dataTable" :items-per-page="5" :search="search">
          <template v-slot:item.dueDateTime="{ item }">
           {{ item.dueDateTime | formatDatetime}}
        </template>
        <template v-slot:item.status="{ item }">
          <v-chip :color="getColor(item.status)" dark>
            {{
            item.status
            }}
          </v-chip>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="clickToEditRequest(item)">mdi-pencil</v-icon>
        </template>
      </v-data-table>
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
export default {
  name: 'MaterialCard',
  data () {
    return {
      search: ''
    }
  },
  methods: {
    getColor (status) {
      if (status === 'Finished') return 'green'
      else if (status === 'Rejected') return 'red'
      else if (status === 'Expired') return 'orange'
      else if (status === 'Waiting') return '#f39c12'
      else return 'blue'
    },
    clickToEditRequest (request) {
      this.$router.push({ path: '/request', query: { ticketId: request.ticketId }, params: { ticketId: request.ticketId } })
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
