<template>
  <div class="staff_page">
    <v-row>
      <v-col cols="12">
        <h2>Department: Cộng tác sinh viên</h2>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title>
            Staff list of student collaboration rooms
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Search"
              single-line
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-btn color="primary" outlined @click="clickToCreateStaff"
              >New Staff</v-btn
            >
          </v-card-title>
          <v-divider></v-divider>
          <v-data-table :headers="headers" :items="_staffList" :search="search">
            <template v-slot:item.actions="{ item }">
              <v-icon small class="mr-2" @click="clickToEditStaff(item)"
                >mdi-pencil</v-icon
              >
              <v-icon small @click="clickToDeleteStaff(item)"
                >mdi-delete</v-icon
              >
            </template>
          </v-data-table>
        </v-card>
      </v-col>
    </v-row>
    <crud-staff
      v-if="dialog"
      v-show="dialog"
      :staff="editedItem"
      :type="type"
      @Closed="closeDialog"
      @Update="updateStaff"
      @Create="createStaff"
    ></crud-staff>
  </div>
</template>

<script>
import CRUDStaff from '../components/CRUDStaff'
import { mapState, mapActions } from 'vuex'
export default {
  components: {
    'crud-staff': CRUDStaff
  },
  computed: {
    ...mapState('staff', ['_staffList'])
  },
  data () {
    return {
      dialog: false,
      type: 'Update',
      search: '',
      headers: [
        {
          text: 'Username',
          align: 'end',
          sortable: false,
          value: 'username'
        },
        { text: 'Email', value: 'email' },
        { text: 'Full Name', value: 'fullName' },
        { text: 'Phone Number', value: 'phonenumber' },
        { text: 'Address', value: 'address' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      editedIndex: '',
      editedItem: {}
    }
  },
  mounted () {
    this._getStaffList()
  },
  methods: {
    ...mapActions('staff', ['_getStaffList', '_addStaff', '_updateStaff', '_deleteStaff']),
    clickToCreateStaff () {
      this.type = 'Create'
      this.editedItem = {}
      this.dialog = true
    },
    clickToEditStaff (staff) {
      this.editedItem = staff
      this.type = 'Update'
      this.dialog = true
    },
    clickToDeleteStaff (staff) {
      // const index = this.serviceList.indexOf(service)
      confirm('Are you sure you want to delete this staff?') && this._deleteStaff(staff)
    },
    closeDialog () {
      this.dialog = false
    },
    async updateStaff (obj) {
      // const editObj = await this.serviceList.find(x => x.serviceId === obj.serviceId)
      // editObj.serviceNm = obj.serviceNm
      // editObj.descriptionService = obj.descriptionService
      // editObj.processMaxDay = obj.processMaxDay
      // editObj.formLink = obj.formLink
      // editObj.sheetLink = obj.sheetLink
      this._updateStaff(obj)
      this.closeDialog()
    },
    createStaff (obj) {
      // this.serviceList.push(obj)
      this._addStaff(obj)
      this.closeDialog()
    }
  }
}
</script>

<style>
.staff_page {
  text-align: left;
  margin: 5px;
}
</style>
