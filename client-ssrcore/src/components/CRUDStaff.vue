<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Staff</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form" lazy-validation>
              <v-row>
                <v-col cols="6">
                  <v-text-field
                    label="Staff's Username"
                    v-model="staff.username"
                    :rules="staffUsernameRules"
                    :counter="100"
                    :disabled="type === 'Update' ? true : false"
                    clearable
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="6">
                  <v-text-field
                    label="Full Name"
                    v-model="staff.fullName"
                    :rules="fullNameRules"
                    :counter="50"
                    clearable
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    v-model="staff.email"
                    clearable
                    clear-icon="mdi-close"
                    label="Email"
                    auto-grow
                    rows="1"
                    :rules="emailRules"
                    :counter="500"
                    required
                  ></v-textarea>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Phone Number"
                    v-model="staff.phonenumber"
                    required
                    :rules="phoneNumberRules"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Address"
                    v-model="staff.address"
                    :rules="addressRules"
                    clearable
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="clickToClose">Close</v-btn>
          <v-btn color="blue darken-1" text @click="clickToSave">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
  props: {
    service: Object,
    type: String,
    staff: Object
    // serviceNm: String,
    // descriptionService: String,
    // formLink: String,
    // sheetLink: String,
    // processMaxDay: Number
  },
  data () {
    var regex = /(09|01[2|6|8|9])+([0-9]{8})\b/g

    return {
      dialog: true,
      staffUsernameRules: [
        v => !!v || 'Username is required',
        v =>
          (v && v.length < 100) || 'Username must be less than 100 characters'
      ],
      fullNameRules: [
        v => !!v || 'Full Name is required',
        v =>
          (v && v.length < 100) || 'Full Name must be less than 100 characters'
      ],
      emailRules: [
        v => !!v || 'Email is required',
        v => (v && v.length < 450) || 'Email must be less than 450 characters',
        v => /.+@.+/.test(v) || 'E-mail must be valid'
      ],
      phoneNumberRules: [
        v => !!v || 'Phone Number is required',
        v =>
          (v && v.match(regex)) || 'Phone Number must be  format phone number'
      ],
      addressRules: [
        v => !!v || 'Address is required',
        v => (v && v.length < 100) || 'Address must be less than 450 characters'
      ]
    }
  },
  mounted () {
    console.log('this.sss', this.staff)
  },
  methods: {
    clickToClose () {
      let confirm = false
      if (this.type === 'Update') {
        confirm = window.confirm(
          'Are you sure you discard all changes this staff?'
        )
      } else {
        confirm = window.confirm(
          'Are you sure you do not want to create a new staff?'
        )
      }
      if (confirm) {
        this.$emit('Closed', this.staff)
      }
    },
    clickToSave () {
      let confirm = false
      const valid = this.$refs.form.validate()
      if (valid) {
        if (this.type === 'Update') {
          confirm = window.confirm('Are you sure you update this staff?')
        } else {
          confirm = window.confirm(
            'Are you sure you want to create a new staff??'
          )
        }
        if (confirm) {
          if (this.type === 'Update') {
            this.$emit('Update', this.staff)
          } else {
            this.$emit('Create', this.staff)
          }
        }
      }
    }
  }
}
</script>

<style></style>
