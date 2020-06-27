<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Service</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form" lazy-validation>
              <v-row>
                <v-col cols="4">
                  <v-text-field
                    label="Service ID"
                    v-model="service.serviceId"
                    :rules="serviceIdRules"
                    :counter="10"
                    :disabled="type === 'Update' ? true : false "
                    clearable
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="8">
                  <v-text-field
                    label="Service Name"
                    v-model="service.serviceNm"
                    :rules="serviceNmRules"
                    :counter="50"
                    clearable
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    v-model="service.descriptionService"
                    clearable
                    clear-icon="mdi-close"
                    label="Description"
                    auto-grow
                    rows="1"
                    :rules="descriptionRules"
                    :counter="500"
                    required
                  ></v-textarea>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Process Day"
                    v-model="service.processMaxDay"
                    required
                    :rules="processMaxDayRules"
                    type="number"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field label="Form Google Link" v-model="service.formLink" clearable></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field label="Sheet Google Link" v-model="service.sheetLink" clearable></v-text-field>
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
    type: String
    // serviceNm: String,
    // descriptionService: String,
    // formLink: String,
    // sheetLink: String,
    // processMaxDay: Number
  },
  data: () => ({
    dialog: true,
    serviceIdRules: [
      v => !!v || 'Service ID is required',
      v =>
        (v && v.length < 10) || 'Service ID must be less than 500 characters'
    ],
    serviceNmRules: [
      v => !!v || 'Service Name is required',
      v =>
        (v && v.length < 50) || 'Service Name must be less than 500 characters'
    ],
    descriptionRules: [
      v => !!v || 'Description is required',
      v =>
        (v && v.length < 500) || 'Description must be less than 500 characters'
    ],
    processMaxDayRules: [
      v => !!v || 'Process Day is required',
      v => (v && v > 0) || 'Process Day must be more than 0'
    ]
  }),
  mounted () {
    console.log('this.sss', this.service)
  },
  methods: {
    clickToClose () {
      let confirm = false
      if (this.type === 'Update') {
        confirm = window.confirm(
          'Are you sure you discard all changes this service?'
        )
      } else {
        confirm = window.confirm(
          'Are you sure you do not want to create a new service?'
        )
      }
      if (confirm) {
        this.$emit('Closed', this.service)
      }
    },
    clickToSave () {
      let confirm = false
      const valid = this.$refs.form.validate()
      if (valid) {
        this.service.processMaxDay = Number.parseInt(this.service.processMaxDay)
        if (this.type === 'Update') {
          confirm = window.confirm('Are you sure you update this service?')
        } else {
          confirm = window.confirm(
            'Are you sure you want to create a new service??'
          )
        }
        if (confirm) {
          if (this.type === 'Update') {
            this.$emit('Update', this.service)
          } else {
            this.$emit('Create', this.service)
          }
        }
      }
    }
  }
}
</script>

<style>
</style>
