<template>
  <div class="service_page">
    <v-row>
      <v-col cols="12">
        <h2>Department: Cộng tác sinh viên</h2>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title>
            Service list of student collaboration rooms
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Search"
              single-line
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-btn color="primary" outlined @click="clickToCreateService"
              >New Service</v-btn
            >
          </v-card-title>
          <v-divider></v-divider>
          <v-data-table
            :headers="headers"
            :items="_serviceList"
            :search="search"
          >
            <template v-slot:item.formLink="{ item }">
              <a :href="item.formLink" target="_blank">{{
                item.formLink | subStringValue()
              }}</a>
            </template>
            <template v-slot:item.sheetLink="{ item }">
              <a :href="item.sheetLink" target="_blank">{{ item.sheetLink| subStringValue() }}</a>
            </template>
            <template v-slot:item.actions="{ item }">
              <v-icon small class="mr-2" @click="clickToEditService(item)"
                >mdi-pencil</v-icon
              >
              <v-icon small @click="clickToDeleteService(item)"
                >mdi-delete</v-icon
              >
            </template>
          </v-data-table>
        </v-card>
      </v-col>
    </v-row>
    <crud-service
      v-if="dialog"
      v-show="dialog"
      :service="editedItem"
      :type="type"
      @Closed="closeDialog"
      @Update="updateService"
      @Create="createService"
    ></crud-service>
  </div>
</template>

<script>
import CRUDService from '../components/CRUDService'
import { mapState, mapActions } from 'vuex'
export default {
  components: {
    'crud-service': CRUDService
  },
  computed: {
    ...mapState('service', ['_serviceList'])
  },
  filters: {
    subStringValue (value) {
      if (value.length > 30 && value) {
        value = value.substring(0, 30) + '...'
      }
      return value
    }
  },
  data () {
    return {
      dialog: false,
      type: 'Update',
      search: '',
      headers: [
        {
          text: 'Service ID',
          align: 'end',
          sortable: false,
          value: 'serviceId'
        },
        { text: 'Service Name', value: 'serviceNm' },
        { text: 'Description', value: 'descriptionService' },
        { text: 'Google Form', value: 'formLink' },
        { text: 'Google Sheet', value: 'sheetLink' },
        { text: 'Process Day', align: 'center', value: 'processMaxDay' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      serviceList: [
        {
          serviceId: '1',
          serviceNm: 'Dang ky muon sach thu vien',
          descriptionService:
            'Description Description Description Description Description Description',
          formLink: 'form.google.com',
          sheetLink: 'sheet.google.com',
          processMaxDay: 7
        },
        {
          serviceId: '2',
          serviceNm: 'Dang ky muon sach thu vien',
          descriptionService:
            'Description Description Description Description Description Description',
          formLink: 'form.google.com',
          sheetLink: 'sheet.google.com',
          processMaxDay: 5
        },
        {
          serviceId: '3',
          serviceNm: 'Dang ky muon sach thu vien',
          descriptionService:
            'Description Description Description Description Description Description',
          formLink: 'form.google.com',
          sheetLink: 'sheet.google.com',
          processMaxDay: 2
        },
        {
          serviceId: '4',
          serviceNm: 'Dang ky muon sach thu vien',
          descriptionService:
            'Description Description Description Description Description Description',
          formLink: 'form.google.com',
          sheetLink: 'sheet.google.com',
          processMaxDay: 8
        },
        {
          serviceId: '5',
          serviceNm: 'Dang ky muon sach thu vien',
          descriptionService:
            'Description Description Description Description Description Description',
          formLink: 'form.google.com',
          sheetLink: 'sheet.google.com',
          processMaxDay: 3
        }
      ],
      editedIndex: '',
      editedItem: {}
    }
  },
  mounted () {
    this._getAllService()
  },
  methods: {
    ...mapActions('service', [
      '_getAllService',
      '_addService',
      '_updateService',
      '_deleteService'
    ]),
    clickToCreateService () {
      this.type = 'Create'
      this.editedItem = {}
      this.dialog = true
    },
    clickToEditService (service) {
      this.editedItem = service
      this.type = 'Update'
      this.dialog = true
    },
    clickToDeleteService (service) {
      // const index = this.serviceList.indexOf(service)
      confirm('Are you sure you want to delete this item?') &&
        this._deleteService(service)
    },
    closeDialog () {
      this.dialog = false
    },
    async updateService (obj) {
      // const editObj = await this.serviceList.find(x => x.serviceId === obj.serviceId)
      // editObj.serviceNm = obj.serviceNm
      // editObj.descriptionService = obj.descriptionService
      // editObj.processMaxDay = obj.processMaxDay
      // editObj.formLink = obj.formLink
      // editObj.sheetLink = obj.sheetLink
      this._updateService(obj)
      this.closeDialog()
    },
    createService (obj) {
      // this.serviceList.push(obj)
      this._addService(obj)
      this.closeDialog()
    }
  }
}
</script>

<style>
.service_page {
  text-align: left;
  margin: 5px;
}
</style>
