<template>
  <div>
    <v-row>
      <v-col cols="12" lg="4">
        <base-material-chart-card
          urlChart="https://datastudio.google.com/embed/reporting/85bb2cc3-1f9a-42a7-b303-05920436e3f1/page/HUyVB"
          color="white"
          hover-reveal
          class="subChart"
        >
          <h4 class="card-title font-weight-light mt-2 ml-2">
            LIST OF ACCEPTED REQUESTS
          </h4>
          <p class="d-inline-flex font-weight-light ml-2 mt-1">
            In June
          </p>
          <template v-slot:actions>
            <v-icon class="mr-1" small>
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light"
              >updated 10 minutes ago</span
            >
          </template>
        </base-material-chart-card>
      </v-col>
      <v-col cols="12" lg="4">
        <base-material-chart-card
          urlChart="https://datastudio.google.com/embed/reporting/85bb2cc3-1f9a-42a7-b303-05920436e3f1/page/HUyVB"
          color="white"
          hover-reveal
          class="subChart"
        >
          <h4 class="card-title font-weight-light mt-2 ml-2">
            LIST OF IN-PROGRESSING REQUESTS
          </h4>
          <p class="d-inline-flex font-weight-light ml-2 mt-1">
            In June
          </p>
          <template v-slot:actions>
            <v-icon class="mr-1" small>
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light"
              >updated 10 minutes ago</span
            >
          </template>
        </base-material-chart-card>
      </v-col>
      <v-col cols="12" lg="4">
        <base-material-chart-card
          urlChart="https://datastudio.google.com/embed/reporting/85bb2cc3-1f9a-42a7-b303-05920436e3f1/page/HUyVB"
          color="white"
          hover-reveal
          class="subChart"
        >
          <h4 class="card-title font-weight-light mt-2 ml-2">
            LIST OF REJECTED REQUESTS
          </h4>
          <p class="d-inline-flex font-weight-light ml-2 mt-1">
            In June
          </p>
          <template v-slot:actions>
            <v-icon class="mr-1" small>
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light"
              >updated 10 minutes ago</span
            >
          </template>
        </base-material-chart-card>
      </v-col>
    </v-row>
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
          title="Waiting Request"
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
          title="Rejected Request"
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
          title=" In-Progress Request"
          :value="_getListTotalTypeRequest[3]"
          sub-icon="mdi-alert"
          sub-icon-color="red"
          sub-text="Get More Space..."
          class="subCard"
        />
      </v-col>
    </v-row>
    <v-row class="mt-5">
      <v-col cols="12" md="12">
        <base-material-card color="primary" class="px-5 py-3">
          <template v-slot:heading>
            <div class="display-1 font-weight-light">
              Request Chart of Our Department
            </div>
            <div class="subtitle-1 font-weight-light">
              Total request of June
            </div>
          </template>
          <v-card-text class="cardChartRequest">
            <div class="chartRequest">
              <iframe
                src="https://datastudio.google.com/embed/reporting/d02a117e-2ba6-49de-8fe1-97704d7a96c5/page/HQyVB"
                frameborder="0"
                style="border:0"
                allowfullscreen
              ></iframe>
            </div>
          </v-card-text>
        </base-material-card>
      </v-col>
    </v-row>
  </div>
</template>
<script>
import MaterialStatsCard from '@/views/MaterialStatsCard'
import MaterialCard from '@/views/MaterialCard'
import MaterialChartCard from '@/views/MaterialChartCard'
import { mapGetters } from 'vuex'
export default {
  components: {
    'base-material-stats-card': MaterialStatsCard,
    'base-material-card': MaterialCard,
    'base-material-chart-card': MaterialChartCard
  },
  data () {
    return {
      headers: [
        {
          sortable: false,
          text: 'ID',
          value: 'id'
        },
        {
          sortable: false,
          text: 'Name',
          value: 'name'
        },
        {
          sortable: false,
          text: 'Salary',
          value: 'salary',
          align: 'right'
        },
        {
          sortable: false,
          text: 'Country',
          value: 'country',
          align: 'right'
        },
        {
          sortable: false,
          text: 'City',
          value: 'city',
          align: 'right'
        }
      ],
      items: [
        {
          id: 1,
          name: 'Dakota Rice',
          country: 'Niger',
          city: 'Oud-Tunrhout',
          salary: '$35,738'
        },
        {
          id: 2,
          name: 'Minerva Hooper',
          country: 'Curaçao',
          city: 'Sinaai-Waas',
          salary: '$23,738'
        },
        {
          id: 3,
          name: 'Sage Rodriguez',
          country: 'Netherlands',
          city: 'Overland Park',
          salary: '$56,142'
        },
        {
          id: 4,
          name: 'Philip Chanley',
          country: 'Korea, South',
          city: 'Gloucester',
          salary: '$38,735'
        },
        {
          id: 5,
          name: 'Doris Greene',
          country: 'Malawi',
          city: 'Feldkirchen in Kārnten',
          salary: '$63,542'
        }
      ],
      tabs: 0,
      tasks: {
        0: [
          {
            text:
              'Sign contract for "What are conference organizers afraid of?"',
            value: true
          },
          {
            text:
              'Lines From Great Russian Literature? Or E-mails From My Boss?',
            value: false
          },
          {
            text:
              'Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit',
            value: false
          },
          {
            text: 'Create 4 Invisible User Experiences you Never Knew About',
            value: true
          }
        ],
        1: [
          {
            text:
              'Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit',
            value: true
          },
          {
            text:
              'Sign contract for "What are conference organizers afraid of?"',
            value: false
          }
        ],
        2: [
          {
            text:
              'Lines From Great Russian Literature? Or E-mails From My Boss?',
            value: false
          },
          {
            text:
              'Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit',
            value: true
          },
          {
            text:
              'Sign contract for "What are conference organizers afraid of?"',
            value: true
          }
        ]
      },
      list: {
        0: false,
        1: false,
        2: false
      }
    }
  },
  mounted () {
    this.alertMessage()
  },
  computed: {
    ...mapGetters('dashboard', ['_getListTotalTypeRequest'])
  },
  methods: {
    alertMessage () {
      console.log('Duong ne')
      console.log(this._getListTotalTypeRequest)
    }
  }
}
</script>
<style lang="scss" scoped>
.cardchartrequest {
  height: 70vh;
}
.chartRequest {
  height: 60vh;
}
.chartRequest iframe {
  width: 100%;
  height: 100%;
}
.subCard {
  margin-top: 5vh;
  margin-bottom: 5vh;
}
.subChart {
  margin-top: 5vh;
}
</style>
