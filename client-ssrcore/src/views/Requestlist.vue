<template>
  <v-container id="regular-tables" fluid tag="section">
    <v-row>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="primary"
          icon="mdi-poll"
          title="Total Finished Request"
          :value="_getFinishedRequest"
          sub-icon="mdi-tag"
          sub-text="Tracked from Google Analytics"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="info"
          icon="mdi-twitter"
          title="Waiting Request"
          :value="_getTotalWaitingRequest"
          sub-icon="mdi-clock"
          sub-text="Just Updated"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="success"
          icon="mdi-store"
          title="Rejected Request"
          :value="_getTotalRejectedRequest"
          sub-icon="mdi-calendar"
          sub-text="Last 24 Hours"
          class="subCard"
        />
      </v-col>
      <v-col cols="12" md="3">
        <base-material-stats-card
          color="orange"
          icon="mdi-sofa"
          title=" In-Progress Request"
          :value="_getTotalInProgressRequest"
          sub-icon="mdi-alert"
          sub-icon-color="red"
          sub-text="Get More Space..."
          class="subCard"
        />
      </v-col>
    </v-row>
    <request-grid
      icon="mdi-clipboard-text"
      title="LIST REQUEST"
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
    ...mapGetters('dashboard', [
      '_getListTotalTypeRequest',
      '_getFinishedRequest',
      '_getTotalWaitingRequest',
      '_getTotalRejectedRequest',
      '_getTotalInProgressRequest'
    ])
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
        { text: 'BeginDateTime', value: 'beginDateTime' },
        { text: 'DueDateTime', value: 'dueDateTime' },
        { text: 'Status', value: 'status' }
        // { text: 'Edit', value: 'actions', sortable: false }
      ]
    }
  },
  mounted () {
    this._getAllRequestOfDepartment()
    this._getAllRequestOfDepartmentDashboard()
  },
  methods: {
    ...mapActions('requestlist', ['_getAllRequestOfDepartment']),
    ...mapActions('dashboard', ['_getAllRequestOfDepartmentDashboard'])
  }
}
</script>
