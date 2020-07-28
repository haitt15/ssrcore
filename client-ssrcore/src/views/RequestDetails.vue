<template>
  <div class="request_page">
    <div class="request_main">
      <div class="request_header">
        Ticket:
        <router-link :to="{ name: 'Student'}">
          <span style="font-weight: bold">{{ _requestService.ticketId }}</span>
        </router-link>
        <h2>{{_requestService.serviceNm}}</h2>
      </div>
      <v-divider></v-divider>
      <div class="request_body">
        <v-row class="row-text" no-gutters>
          <v-col :cols="6">
            <span class="request_title">Service:</span>
          </v-col>
          <v-col :cols="4" :offset="2">
            <span class="request_title">Status:</span>
          </v-col>
          <v-col :cols="6">
            <span class="request_text">{{ _requestService.serviceNm}}</span>
          </v-col>
          <v-col :cols="4" :offset="2">
            <v-chip :color="getColor(_requestService.status)" dark>
              {{
              _requestService.status
              }}
            </v-chip>
            <!-- <span
              :class="{  Waiting : _requestService.status === 'Waiting',InProgress : _requestService.status === 'In-Progress',
              Finished : _requestService.status === 'Finished',Rejected : _requestService.status === 'Rejected',
              Expired: _requestService.status === 'Expired'}"
            >{{_requestService.status}}</span>-->
            <v-icon @click="clickToEditStatus" class="pen-edit">mdi-pencil-outline</v-icon>
            <v-dialog v-model="editStatusDialog" max-width="500px">
              <v-card>
                <v-card-title>Edit Status</v-card-title>
                <v-card-text>
                  <v-select
                    v-model="selectedStatus"
                    :items="statusEnum"
                    label="Status"
                    item-value="text"
                  ></v-select>
                </v-card-text>
                <v-card-actions>
                  <v-btn color="primary" text @click="editStatusDialog = false">Close</v-btn>
                  <v-btn color="primary" text @click="clickToChooseStatus" :loading="loading">Update</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-col>
        </v-row>
        <v-row class="row-text" no-gutters>
          <v-col :cols="6">
            <span class="request_title">Department:</span>
          </v-col>
          <v-col :cols="4" :offset="2">
            <span class="request_title">Expired Date:</span>
          </v-col>
          <v-col :cols="6">
            <span class="request_text">{{_requestService.departmentNm}}</span>
          </v-col>
          <v-col :cols="4" :offset="2">
            <span>{{_requestService.dueDateTime | formatDatetime}}</span>
            <v-menu
              ref="menu"
              v-model="menu"
              :close-on-content-click="false"
              transition="scale-transition"
              offset-y
              max-width="290px"
              min-width="290px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-icon
                  @click="clickToEditExpiredDate"
                  class="pen-edit"
                  v-bind="attrs"
                  v-on="on"
                >mdi-pencil-outline</v-icon>
              </template>
              <v-date-picker v-if="chooseDate" v-model="date" :show-current="false" :min="minDate">
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="chooseDate = false">Cancel</v-btn>
                <v-btn text color="primary" @click="clickToChooseDate" :loading="loading">Update</v-btn>
              </v-date-picker>
            </v-menu>
          </v-col>
        </v-row>
        <v-row class="row-text" no-gutters>
          <v-col :cols="12">
            <span class="request_title">Description:</span>
          </v-col>
          <v-col :cols="12">
            <span class="request_text">{{_requestService.content}}</span>
          </v-col>
        </v-row>
        <v-row class="row-text" v-if="jsonList.length > 0">
          <v-col :cols="12">
            <span class="request_title">Table:</span>
          </v-col>
          <v-col :cols="12">
            <v-data-table
              :headers="headers"
              :items="jsonList"
              hide-default-header
              hide-default-footer
              class="elevation-1"
            ></v-data-table>
          </v-col>
        </v-row>
        <v-divider></v-divider>
        <v-row class="row-text" no-gutters>
          <v-col :cols="12">
            <span class="request_title">Comments:</span>
          </v-col>
        </v-row>
        <div v-for="item in _commentList" :key="item.id">
          <Comments :user="item.fullName" :time="item.insDatetime" :content="item.content"></Comments>
        </div>
        <div id="comment-textarea">
          <v-form ref="form" lazy-validation>
            <v-textarea
              v-model="comment"
              clearable
              clear-icon="mdi-close"
              label="Comments"
              auto-grow
              outlined
              rows="3"
              :rules="commentRules"
              :counter="COUNTER"
              required
            ></v-textarea>
          </v-form>
          <v-btn outlined color="primary" style="float:right" @click="clickToComment" :loading="loading">Comment</v-btn>
        </div>
      </div>
    </div>
    <div class="request_nav">
      <v-divider></v-divider>
      <v-row class="row-text" no-gutters>
        <v-col :cols="12">
          <span class="request_title">Assignee:</span>
        </v-col>
        <v-autocomplete
          v-model="asignee"
          :items="_staffList"
          item-text="username"
          item-value="username"
          @focus="onKeyPress"
          @keypress="onKeyPress"
          @blur="onBlur"
          @change="changeStaff"
          :loading="loading"
        >
          <template v-slot:selection="data">
            <v-chip
              v-bind="data.attrs"
              :input-value="data.selected"
              @click="data.select"
              color="white"
            >
              <v-avatar left>
                <v-img :src="data.item.photo !== null ? data.item.photo : avatarDefault"></v-img>
              </v-avatar>
              {{ `${data.item.fullName} (${data.item.username})` }}
            </v-chip>
          </template>
          <template v-slot:item="data">
            <template v-if="typeof data.item !== 'object'">
              <v-list-item-content v-text="data.item"></v-list-item-content>
            </template>
            <template v-else>
              <v-list-item-avatar>
                <img :src="data.item.photo !== null ? data.item.photo : avatarDefault" />
              </v-list-item-avatar>
              <v-list-item-content>
                <v-list-item-title v-html="`${data.item.fullName} (${data.item.username})`"></v-list-item-title>
              </v-list-item-content>
            </template>
          </template>
        </v-autocomplete>
      </v-row>
      <v-row class="row-text" no-gutters>
        <v-col :cols="12">
          <span class="request_title">Reporter:</span>
        </v-col>
        <v-text-field readonly>
          <template slot="prepend-inner">
            <v-chip color="white">
              <v-avatar left>
                <v-img
                  :src="_requestService.studentPhoto !== null ? _requestService.studentPhoto : avatarDefault"
                ></v-img>
              </v-avatar>
              {{ `${_requestService.fullName} (${_requestService.username})` }}
            </v-chip>
          </template>
        </v-text-field>
      </v-row>
    </div>
  </div>
</template>

<script>
import moment from 'moment'
import Comments from '../components/Comment'
import { mapState, mapActions } from 'vuex'
export default {
  components: {
    Comments
  },
  computed: {
    ...mapState('requestDetails', ['_requestService']),
    ...mapState('comment', ['_commentList']),
    ...mapState('staff', ['_staffList']),
    getJsonObject () {
      // var json = JSON.parse(
      //   '{"Ticket Id":"tauqsaij","Student":"Bui Hai Nam (K13_HCM)","Service":"Đăng ký học block 3 tuần","Status":"Waiting","Staff":"","Department":"Phòng Công Tác Sinh Viên 3"}'
      // )
      if (this._requestService.jsonInformation !== null) {
        debugger
        var json = JSON.parse(
          this._requestService.jsonInformation
        )
        var jsonList = []
        var keys = Object.keys(json)
        for (var key in keys) {
          var x = keys[key]
          jsonList.push({ title: x, value: json[x] })
        }
        return jsonList
      } else {
        return []
      }
    }
  },
  filters: {
    formatDatetime (val) {
      if (val) {
        return moment(val.substr(0, 10)).format('DD/MM/YYYY')
      }
      return ''
    }
  },
  data () {
    const srcs = {
      1: 'https://cdn.vuetifyjs.com/images/lists/1.jpg',
      2: 'https://cdn.vuetifyjs.com/images/lists/2.jpg',
      3: 'https://cdn.vuetifyjs.com/images/lists/3.jpg',
      4: 'https://cdn.vuetifyjs.com/images/lists/4.jpg',
      5: 'https://cdn.vuetifyjs.com/images/lists/5.jpg'
    }
    return {
      jsonList: [],
      headers: [
        { text: 'Title', value: 'title' },
        { text: 'Value', value: 'value' }
      ],
      loading: false,
      avatarDefault:
        'https://thumbs.dreamstime.com/b/bearded-man-s-face-hipster-character-fashion-silhouette-avata-avatar-emblem-icon-label-vector-illustration-105106714.jpg',
      ticketId: 'upchhnwr',
      commentRules: [
        v => !!v || 'Comment is required',
        v =>
          (v && v.length < this.COUNTER) ||
          'Comment must be less than 500 characters'
      ],
      COUNTER: 500,
      asignee: 'Sandra Adams',
      people: [
        { name: 'Sandra Adams', avatar: srcs[1] },
        { name: 'Ali Connors', avatar: srcs[2] },
        { name: 'Trevor Hansen', avatar: srcs[3] },
        { name: 'Tucker Smith', avatar: srcs[2] },
        { name: 'Britta Holt', avatar: srcs[4] },
        { name: 'Jane Smith ', avatar: srcs[5] },
        { name: 'John Smith', avatar: srcs[1] },
        { name: 'Sandra Williams', avatar: srcs[3] }
      ],
      reporter: { name: 'Sandra Adams', avatar: srcs[1] },
      requestService: {
        ticketId: 'SSR-01',
        requestTitle: 'Request Title',
        service: 'Muon sach thu vien',
        status: 'Finished',
        department: 'Thu vien',
        expiredDate: '28/06/2020',
        description:
          'Em muon muon sach Software Design cua thu vien cho mon SWD'
      },
      editStatusDialog: false,
      statusEnum: [
        { text: 'Waiting' },
        { text: 'In-Progress' },
        { text: 'Finished' },
        { text: 'Rejected' },
        { text: 'Expired' }
      ],
      selectedStatus: '',
      menu: false,
      chooseDate: false,
      minDate: '',
      date: '2020-5-5',
      comment: '',
      items: [],
      search: null,
      staffs: ['Son Map', 'Tung Duong', 'Hoang Duy', 'Thanh Hai', 'Hai Nam'],
      commentList: [
        {
          user: 'Staff',
          time: moment().format('MMMM Do YYYY, h:mm:ss A'),
          content:
            'Chao Son, thu vien hien da het sach nay nen chi se reject request cua em nha'
        },
        {
          user: 'Son Map',
          time: moment().format('MMMM Do YYYY, h:mm:ss A'),
          content: 'Oke chi'
        }
      ],
      temp: ''
    }
  },
  mounted () {
    console.log((this.ticketId = this.$route))
    this.initPage()
  },
  methods: {
    ...mapActions('requestDetails', [
      '_getRequestService',
      '_updateRequestService'
    ]),
    ...mapActions('comment', [
      '_getCommentList',
      '_addComment',
      '_updateComment',
      '_deleteComment'
    ]),
    ...mapActions('staff', ['_getStaffList']),
    initPage () {
      this.ticketId = this.$route.query.ticketId
      this.minDate = moment().format('YYYY-MM-DD')
      this._getRequestService(this.ticketId)
        .then(res => {
          this.date = moment(
            this._requestService.dueDateTime.substr(0, 10)
          ).format('YYYY-MM-DD')
          this.asignee = this._requestService.staffUsername
        })
        .then(res => {
          this.jsonList = []
          if (this._requestService.jsonInformation !== null) {
            var json = JSON.parse(
              this._requestService.jsonInformation
            )
            var keys = Object.keys(json)
            for (var key in keys) {
              var x = keys[key]
              this.jsonList.push({ title: x, value: json[x] })
            }
          }
          this._getStaffList({ departmentId: this._requestService.departmentId })
        }).then(res => {
          this._getCommentList({ ticketId: this._requestService.ticketId })
        })
    },
    getColor (status) {
      if (status === 'Finished') return 'green'
      else if (status === 'Rejected') return 'red'
      else if (status === 'Expired') return 'orange'
      else if (status === 'Waiting') return 'amber'
      else return 'blue'
    },
    onKeyPress () {
      if (this.asignee !== null && this.asignee !== '') {
        this.temp = this.asignee
      }
      this.asignee = ''
    },
    onBlur () {
      if (this.asignee === null || this.asignee === '') {
        this.asignee = this.temp
      }
    },
    clickToComment () {
      this.loading = true
      const valid = this.$refs.form.validate()
      if (valid) {
        const user = JSON.parse(localStorage.getItem('UserInfo'))
        this._addComment({
          username: user.username,
          content: this.comment,
          ticketId: this._requestService.ticketId
        }).then(
          res => {
            this.comment = ''
            this.initPage()
            this.loading = false
            this.$refs.form.reset()
          }
        )
      }
    },
    clickToEditStatus () {
      this.editStatusDialog = true
      this.selectedStatus = this._requestService.status
    },
    clickToChooseStatus () {
      this.loading = true
      this._updateRequestService({
        ticketId: this._requestService.ticketId,
        status: this.selectedStatus
      }).then(res => {
        this.editStatusDialog = false
        this.loading = false
      })
    },
    clickToEditExpiredDate () {
      this.chooseDate = true
    },
    clickToChooseDate () {
      this.loading = true
      this._updateRequestService({
        ticketId: this._requestService.ticketId,
        dueDateTime: this.date
      }).then(res => {
        this.chooseDate = false
        this.loading = false
      })
    },
    changeStaff () {
      this.loading = true
      this._updateRequestService({
        ticketId: this._requestService.ticketId,
        staffUsername: this.asignee
      }).then(res => {
        this.loading = false
      })
    }
  }
}
</script>

<style>
.request_page {
  text-align: left;
  margin: 5px;
}
.request_main {
  height: 500;
  width: 80%;
  float: left;
  margin-bottom: 3%;
}
.request_nav {
  margin-top: 60.5px;
  margin-left: 0.8%;
  width: 19%;
  float: right;
}
.request_title {
  color: grey;
  font-weight: 500;
}
.request_text {
  color: black;
  font-weight: bold;
}
.Waiting {
  color: white;
  background-color: #f39c12;
  padding: 4px;
}
.InProgress {
  color: white;
  background-color: blue;
  padding: 4px;
}
.Finished {
  color: white;
  background-color: green;
  padding: 4px;
}
.Rejected {
  color: white;
  background-color: red;
  padding: 4px;
}
.Expired {
  color: white;
  background-color: orange;
  padding: 4px;
}
.row-text {
  margin: 5px;
}
#comment-textarea {
  margin-top: 10px;
}
.pen-edit {
  margin-left: 5px;
}
</style>
