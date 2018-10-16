<template>
  <div class="main_body">
     <p>BuStar</p>
    <form @submit.prevent="createTable(busStopsTips[0])">
      <input v-bind:class="inputClass" v-model="searchInput" @input="inputChange" autocomplete="off" autocorrect="off" autocapitalize="off"
      spellcheck="false" type="text" autofocus placeholder="Start typing your stop's name">
      <input hidden type="submit">

      <span v-if="showTips" v-on:click="createTable(stops)" v-bind:class="tipsListClass" v-for="stops in busStopsTips" :key="stops">{{ stops }}</span>
      
    </form>
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
  </div>
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'Home',
    components: {
      axios
    },
    data() {

      return {
        tipsListClass: 'tipsList',
        inputClass: 'inputStyle',
        showTips: false,
        showTable: false,
        //clear when api starts working
        busStops: null,
        stopDatas: [],
        busStopsTips: [],
        searchInput: ''
      }
    },
    mounted() {
      axios.get("https://localhost:5001/api/bustar/buses")
        .then((response) => {
          this.busStops = response.data
        })
        .catch(error => {
          console.log(error.response)
        })
    },

    methods: {
      searchSubmit() {
        return axios.get("https://localhost:5001/api/bustar/stopdata/" + this.searchInput.replace('/', '*'))
          .then((response) => {
            this.stopDatas = response.data;
            return response.data;
          })
      },
      createTable(bus){
        this.searchInput=bus;
        this.searchSubmit().then(() =>
        {
          this.showTable=true;
          this.showTips=false;
        })
      },
      inputChange() {
        if (this.searchInput != '') {
          this.showTips = true;
          this.showTable = false;
          this.inputClass = 'inputStyleActive';
        } else {
          this.showTips = false;
          this.inputClass = 'inputStyle';
        }
        this.busStopsTips = [];
        for (this.i = 0, this.tipsPostion = 0; this.i < Object.keys(this.busStops).length; this.i++) {
          if (this.busStops[this.i].toLowerCase().includes(this.searchInput.toLowerCase()) && this.tipsPostion < 5) {
            this.busStopsTips[this.tipsPostion++] = this.busStops[this.i];
          }
        }
      }
    }
    
  }

</script>

<style lang="scss" src="./Home.scss" scoped></style>
