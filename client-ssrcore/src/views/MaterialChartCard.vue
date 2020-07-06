<template>
  <base-material-card
    class="v-card--material-chart"
    v-bind="$attrs"
    v-on="$listeners"
  >
    <template v-slot:heading>
        <iframe
              height="150"
              :src="urlChart"
              frameborder="0"
              style="border:0"
              allowfullscreen
              class="w-100"
            ></iframe>
    </template>

    <slot
      slot="reveal-actions"
      name="reveal-actions"
    />

    <slot />

    <slot
      slot="actions"
      name="actions"
    />
  </base-material-card>
</template>

<script>
import MaterialCard from '@/views/MaterialCard'
export default {
  components: {
    'base-material-card': MaterialCard
  },
  name: 'MaterialChartCard',

  inheritAttrs: false,

  props: {
    urlChart: {
      type: String,
      default: undefined
    }

  }
}
</script>

<style lang="sass">
  .v-card--material-chart
    p
      color: #999
    .v-card--material__heading
      height:50vh
    .v-card--material__heading
      iframe
        width: inherit
        height: 40vh
</style>
