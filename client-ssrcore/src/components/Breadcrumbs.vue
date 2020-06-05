<template>
  <v-breadcrumbs :items="items" class="margin-top: 5vh">
    <template v-slot:item="{ item }">
      <v-breadcrumbs-item :href="item.href" :disabled="item.disabled">{{ item.text.toUpperCase() }}</v-breadcrumbs-item>
    </template>
  </v-breadcrumbs>
</template>

<script>
export default {
  props: {
    items: Array
  },
  data () {
    return {
    }
  }
}
</script>

<style>
</style>
