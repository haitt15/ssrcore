<template>
  <div class="request_page">
    <div class="request_main">
      <div class="request_header">
        <router-link :to="{ name: 'Student'}">
          <span style="font-weight: bold">{{ requestService.ticketId }}</span>
        </router-link>
        <h2>{{requestService.requestTitle}}</h2>
      </div>
      <v-divider></v-divider>
      <div class="request_body">
        <v-row class="row-text" no-gutters>
          <v-col :cols="4">
            <span class="request_title">Service:</span>
          </v-col>
          <v-col :cols="4" :offset="4">
            <span class="request_title">Status:</span>
          </v-col>
          <v-col :cols="4">
            <span class="request_text">{{ requestService.service}}</span>
          </v-col>
          <v-col :cols="4" :offset="4">
            <span
              :class="{  Waiting : requestService.status === 'Waiting',InProgress : requestService.status === 'In Progress',
              Accepted : requestService.status === 'Accepted',Rejected : requestService.status === 'Rejected',
              Expired: requestService.status === 'Expired'}"
            >{{requestService.status}}</span>
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
                  <v-btn color="primary" text @click="clickToChooseStatus">Update</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-col>
        </v-row>
        <v-row class="row-text" no-gutters>
          <v-col :cols="4">
            <span class="request_title">Department:</span>
          </v-col>
          <v-col :cols="4" :offset="4">
            <span class="request_title">Expired Date:</span>
          </v-col>
          <v-col :cols="4">
            <span class="request_text">{{requestService.department}}</span>
          </v-col>
          <v-col :cols="4" :offset="4">
            <span>{{requestService.expiredDate}}</span>
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
              <v-date-picker
                v-if="chooseDate"
                v-model="date"
                :show-current="false"
                :min="minDate"
                @click:date="clickToChooseDate"
              ></v-date-picker>
            </v-menu>
          </v-col>
        </v-row>
        <v-row class="row-text" no-gutters>
          <v-col :cols="12">
            <span class="request_title">Description:</span>
          </v-col>
          <v-col :cols="12">
            <span class="request_text">{{requestService.description}}</span>
          </v-col>
        </v-row>
        <v-divider></v-divider>
        <v-row class="row-text" no-gutters>
          <v-col :cols="12">
            <span class="request_title">Comments:</span>
          </v-col>
        </v-row>
        <div v-for="item in commentList" :key="item.name">
          <Comments :user="item.user" :time="item.time" :content="item.content"></Comments>
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
          <v-btn outlined color="primary" style="float:right" @click="clickToComment">Comment</v-btn>
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
          :items="people"
          item-text="name"
          item-value="name"
          @focus="onKeyPress"
          @keypress="onKeyPress"
          @blur="onBlur"
        >
          <template v-slot:selection="data">
            <v-chip
              v-bind="data.attrs"
              :input-value="data.selected"
              @click="data.select"
              color="white"
            >
              <v-avatar left>
                <v-img :src="data.item.avatar"></v-img>
              </v-avatar>
              {{ data.item.name }}
            </v-chip>
          </template>
          <template v-slot:item="data">
            <template v-if="typeof data.item !== 'object'">
              <v-list-item-content v-text="data.item"></v-list-item-content>
            </template>
            <template v-else>
              <v-list-item-avatar>
                <img :src="data.item.avatar" />
              </v-list-item-avatar>
              <v-list-item-content>
                <v-list-item-title v-html="data.item.name"></v-list-item-title>
                <v-list-item-subtitle v-html="data.item.group"></v-list-item-subtitle>
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
                <v-img :src="reporter.avatar"></v-img>
              </v-avatar>
              {{ reporter.name }}
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
    ...mapState('comment', ['_commentList'])
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
        status: 'Accepted',
        department: 'Thu vien',
        expiredDate: '28/06/2020',
        description:
          'Em muon muon sach Software Design cua thu vien cho mon SWD'
      },
      editStatusDialog: false,
      statusEnum: [
        { text: 'Waiting' },
        { text: 'In Progress' },
        { text: 'Accepted' },
        { text: 'Rejected' },
        { text: 'Expired' }
      ],
      selectedStatus: '',
      menu: false,
      chooseDate: false,
      minDate: '',
      date: '2020-5-5',
      comment: '',
      loading: false,
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
    this.minDate = moment().format('YYYY-MM-DD')
    this.date = moment(this.requestService.expiredDate, 'DD/MM/YYYY').format(
      'YYYY-MM-DD'
    )
  },
  methods: {
    ...mapActions('requestDetails', ['_getRequestService', '_updateRequestService']),
    ...mapActions('comment', ['_getCommentList', '_addComment', '_updateComment', '_deleteComment']),
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
      const valid = this.$refs.form.validate()
      if (valid) {
        this.commentList.push({
          user: 'Son Map',
          time: moment().format('MMMM Do YYYY, h:mm:ss A'),
          content: this.comment
        })
        this.comment = ''
      }
    },
    clickToEditStatus () {
      this.editStatusDialog = true
      this.selectedStatus = this.requestService.status
    },
    clickToChooseStatus () {
      this.editStatusDialog = false
      this.requestService.status = this.selectedStatus
    },
    clickToEditExpiredDate () {
      this.chooseDate = true
    },
    clickToChooseDate () {
      this.chooseDate = false
      this.requestService.expiredDate = moment(this.date).format('DD/MM/YYYY')
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
  background-color: #3498db;
  padding: 4px;
}
.Accepted {
  color: white;
  background-color: #2ecc71;
  padding: 4px;
}
.Rejected {
  color: white;
  background-color: #e74c3c;
  padding: 4px;
}
.Expired {
  color: white;
  background-color: #d35400;
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
