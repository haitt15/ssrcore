<template>
  <v-row class="comment" no-gutters>
    <v-col :cols="12">
      <v-icon style="color:#3498db">mdi-account-circle</v-icon>
      <span class="comment-title">
        <span style="color:#3498db">{{ user}}</span>
        added a comment - {{ time }}
      </span>
    </v-col>
    <v-col class="comment-body" :cols="12">
      <span>{{content}}</span>
    </v-col>
    <v-divider></v-divider>
  </v-row>
</template>

<script>
export default {
  props: {
    user: String,
    time: String,
    content: String
  }
}
</script>

<style>
.comment {
  font-size: 14px;
  padding-top: 5px;
  padding-left: 10px;
}
.comment-title {
  padding-left: 4px;
  vertical-align: middle;
}
.comment-body {
  margin-top: 10px;
  margin-left: 20px;
  margin-bottom: 10px;
}
</style>
