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
    <base-material-table-card
      icon="mdi-clipboard-text"
      title="LIST REQUEST IN JUNE"
      class="px-5 py-3"
      :headerTable="headers"
      :dataTable="_getListOfRequest"
    >
    </base-material-table-card>

    <div class="py-3" />
  </v-container>
</template>
<script>
import MaterialTableCard from '@/views/MaterialTableCard'
import MaterialStatsCard from '@/views/MaterialStatsCard'
import { mapGetters } from 'vuex'
export default {
  components: {
    'base-material-table-card': MaterialTableCard,
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
          text: 'TicketID',
          align: 'start',
          sortable: false,
          value: 'ticketid'
        },
        { text: 'ServiceName', value: 'servicename' },
        { text: 'Content', value: 'content' },
        { text: 'DueDateTime', value: 'duedatetime' },
        { text: 'Status', value: 'status' },
        { text: 'Edit', value: 'actions', sortable: false }
      ]
    }
  }
}
</script>
