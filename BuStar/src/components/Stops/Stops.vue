<template>
  <div class="main_body">
      <router-link :to="{ name: 'homestop', params: { stopName: stop }}" class="tipsList"
       v-for="stop in stopDatas" :key="stop" >{{stop}}</router-link>
      <div class="preloader" v-if="loading">
        Fetching stops...
        <three-dots />
      </div>
      <div class="preloader" v-if="fetchError">
        Sorry, stops could not be fetched from the Server. Please refresh this page or try later.
      </div>
  </div>
</template>

<script>
  import axios from 'axios'
  import connections from '../../api/connections.js'
  import globals from '../../globals/globals.js'

  import {
    ThreeDots
  } from 'vue-loading-spinner'
  export default {
    name: 'Stops',
    components: {
      axios,
      ThreeDots
    },
    data() {
      return {
        stopDatas: [],
        loading: false,
        fetchError: false,
      }
    },
    mounted() {
      this.loading = true;
      if (globals.stops.length == 0) {
        axios.get(connections.api + "/buses")
          .then((response) => {
            globals.stops = response.data,
            this.stopDatas=globals.stops,
              this.loading = false;
          })
          .catch(error => {
            console.log(error.response)
            this.fetchError = true;
            this.loading = false;
          })
      }
      else
      {
        this.stopDatas=globals.stops;
        this.loading = false;
      }
      
  }
}
</script>

<style lang="scss" src="./Stops.scss" scoped></style>
