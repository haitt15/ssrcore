<template>
  <v-container id="regular-tables" fluid tag="section">
    <v-row>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="primary"
          icon="mdi-poll"
          title="Total Request"
          :value="_getListTotalTypeRequest[0]"
          sub-icon="mdi-tag"
          sub-text="Tracked from Google Analytics"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="info"
          icon="mdi-twitter"
          title="Total On Waiting Request"
          :value="_getListTotalTypeRequest[1]"
          sub-icon="mdi-clock"
          sub-text="Just Updated"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="success"
          icon="mdi-store"
          title="Total Rejected Request"
          :value="_getListTotalTypeRequest[2]"
          sub-icon="mdi-calendar"
          sub-text="Last 24 Hours"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="orange"
          icon="mdi-sofa"
          title="Total In-Progress Request"
          :value="_getListTotalTypeRequest[3]"
          sub-icon="mdi-alert"
          sub-icon-color="red"
          sub-text="Get More Space..."
          class="subCard"
        />
      </v-col>
    </v-row>
    <request-grid
      icon="mdi-clipboard-text"
      title="LIST REQUEST IN JUNE"
      class="px-5 py-3"
      :headerTable="headers"
      :dataTable="_getListOfRequest"
    >
    </request-grid>

    <div class="py-3" />
  </v-container>
</template>
<script>
import RequestGrid from '@/views/RequestGrid'
import MaterialStatsCard from '@/views/MaterialStatsCard'
import { mapGetters, mapActions } from 'vuex'
export default {
  components: {
    'request-grid': RequestGrid,
    'base-material-stats-card': MaterialStatsCard
  },
  computed: {
    ...mapGetters('requestlist', ['_getListOfRequest']),
    ...mapGetters('dashboard', ['_getListTotalTypeRequest'])
  },
  data () {
    return {
      headers: [
        {
          text: 'Ticket',
          align: 'start',
          sortable: false,
          value: 'ticketId'
        },
        { text: 'ServiceName', value: 'serviceNm' },
        { text: 'Content', value: 'content' },
        { text: 'DueDateTime', value: 'dueDateTime' },
        { text: 'Status', value: 'status' },
        { text: 'Edit', value: 'actions', sortable: false }
      ]
    }
  },
  mounted () {
    this._getAllRequestOfDepartment()
  },
  methods: {
    ...mapActions('requestlist', ['_getAllRequestOfDepartment'])
  }
}
</script>
