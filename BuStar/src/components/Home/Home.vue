<template>
  <div class="main_body">
    <p>BuStar</p>
    <form @submit.prevent="createTable(busStopsTips[0])">

      <input v-bind:class="inputClass" v-model="searchInput" @input="inputChange" autocomplete="off" autocorrect="off"
        autocapitalize="off" spellcheck="false" type="text" autofocus placeholder="Start typing your stop name">
      <input hidden type="submit">
      <span v-if="showTips" v-on:click="createTable(stops)" v-bind:class="tipsListClass" v-for="stops in busStopsTips"
        :key="stops">{{ stops }}</span>
      <p v-if="showTable">{{ requestTime }}</p>
      <table v-if="showTable">
        <template v-for="(stopInfo, index) in stopDatas.stopInfos"> 
         <tr :key="index">
          <th>Bus Line</th>
          <th>Head Sign</th>
          <th>Arrival Time from Time Table</th>
          <th>Estimated Arrival Time</th>
        </tr>
          <tr :key="busInfo.routeID + busInfo.headsign + busInfo.theoreticalTime" v-for="(busInfo) in stopInfo.busInfos">
            <td>{{busInfo.routeID}}</td>
            <td>{{busInfo.headsign}}</td>
            <td>{{busInfo.theoreticalTime}}</td>
            <td>{{busInfo.estimatedTime}}</td>
          </tr>
        </template>
      </table>
      <div class="preloader" v-if="searching">
        Searching for buses...
        <three-dots/>
      </div>
      <div class="preloader" v-else-if="loading">
        Fetching stops...
        <three-dots/>
      </div>
      <div class="preloader" v-else-if="empty">
        There is no stop with this name.
      </div>
      <div class="preloader" v-if="fetchError">
        Sorry, stops could not be fetched from the Server. Please refresh this page or try later.
      </div>
    </form>
  </div>
</template>

<script>
  import axios from 'axios'
  import connections from '../../api/connections.js'
  import globals from '../../globals/globals.js'

  import {ThreeDots} from 'vue-loading-spinner'
  export default {
    name: 'Home',
    props: ['stop'],
    components: {
      axios,
      ThreeDots
    },
    data() {
      return {
        tipsListClass: 'tipsList',
        inputClass: 'inputStyle',
        showTips: false,
        showTable: false,
        //clear when api starts working
        stopDatas: [],
        busStopsTips: [],
        searchInput: '',

        searching: false,
        loading: false,
        fetchError: false,
        empty: false,

        reCall:''
      }
    },
    mounted() {
      this.loading = true;
      if(globals.stops.length == 0)
      {
        axios.get( connections.api + "/buses")
          .then((response) => {
            globals.stops = response.data,
            this.loading = false
          })
          .catch(error => {
            console.log(error.response)
            this.fetchError = true;
            this.loading = false;
          })
      }
    },

    methods: {
      searchSubmit() {
        if(this.showTable==false)
           this.searching = true;
           
        return axios.get(connections.api + "/stopdata/" + this.searchInput.replace('/', '*'))
          .then((response) => {
            this.stopDatas = response.data;
            return response.data;
          })        
      },
      createTable(stop) {
        if(Object.keys(this.busStopsTips).length==0)
        {
            this.empty=true;
            return 0;
        }
        else
        this.empty=false;
        this.inputClass = 'inputStyle';
        this.showTips = false;
        this.searchInput = stop;
        this.searchSubmit().then(() => {
          this.showTable = true;
          this.searching = false;
          this.reCall = setInterval(() => {this.searchSubmit(); }, 60000)
        })
      },
      inputChange() {
         this.busStopsTips = [];
        for (this.i = 0, this.tipsPostion = 0; this.i < Object.keys(globals.stops).length; this.i++) {
          if (globals.stops[this.i].toLowerCase().includes(this.searchInput.toLowerCase()) && this.tipsPostion < 5) {
            this.busStopsTips[this.tipsPostion++] = globals.stops[this.i];
          }
        }
        this.showTable = false;
        clearInterval(this.reCall)
         if (this.searchInput != '') {
            this.showTips = true;
            if (this.tipsPostion > 0) {
              this.inputClass = 'inputStyleActive';
            } else {
              this.inputClass = 'inputStyle';
            }
          } else {
            this.busStopsTips=[];
            this.showTips = false;
            this.inputClass = 'inputStyle';
          }

      }
    },
    computed:
    {
      requestTime()
      {
        return this.stopDatas.responseTime;
      }
    }

  }

</script>

<style lang="scss" src="./Home.scss" scoped></style>