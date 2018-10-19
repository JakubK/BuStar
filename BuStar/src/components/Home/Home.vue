<template>
  <div class="main_body">
    <p>BuStar</p>
    <form @submit.prevent="createTable(busStopsTips[0])">

      <input v-bind:class="inputClass" v-model="searchInput" @input="inputChange" autocomplete="off" autocorrect="off"
        autocapitalize="off" spellcheck="false" type="text" autofocus placeholder="Start typing your stop name">
      <input hidden type="submit">
      <!-- <ul v-if="showTips" class="tipsUl">
        <li v-on:mouseover="searchInput=stops" v-on:click="createTable" class="tipsLi" v-for="stops in busStopsTips" :key="stops">{{ stops }}</li>
      </ul>-->
      <span v-if="showTips" v-on:click="createTable(stops)" v-bind:class="tipsListClass" v-for="stops in busStopsTips"
        :key="stops">{{ stops }}</span>
      <table v-if="showTable">
        <tr>
          <th>Bus Line</th>
          <th>Head Sign</th>
          <th>Arrival Time from Time Table</th>
          <th>Estimated Arrival Time</th>
        </tr>
        <template v-for="(stopInfo) in stopDatas.stopInfos">
          <tr :key="busInfo.routeID + busInfo.headsign + busInfo.theoreticalTime" v-for="(busInfo) in stopInfo.busInfos">
            <td>{{busInfo.routeID}}</td>
            <td>{{busInfo.headsign}}</td>
            <td>{{busInfo.theoreticalTime}}</td>
            <td>{{busInfo.estimatedTime}}</td>
          </tr>
        </template>
      </table>
      <div class="preloader" v-if="searching == 1">
        Searching for a buses...
      </div>
    </form>
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
        searchInput: '',
        searching: -1,
        reCall:''
      }
    },
    mounted() {
      axios.get("https://localhost:5001/api/bustar/buses")
        .then((response) => {
          this.busStops = response.data,
            console.log(this.busStops)
        })
        .catch(error => {
          console.log(error.response)
        })
    },

    methods: {
      searchSubmit() {
        if(this.showTable==false)
        {
        this.searching = 1;
        }
        return axios.get("https://localhost:5001/api/bustar/stopdata/" + this.searchInput.replace('/', '*'))
          .then((response) => {
            this.stopDatas = response.data;
            return response.data;
          })
      },
      createTable(stop) {
        this.inputClass = 'inputStyle';
        this.showTips = false;
        this.searchInput = stop;
        this.searchSubmit().then(() => {
          this.showTable = true;
          this.searching = 0;
          this.reCall = setInterval(() => {this.searchSubmit(); }, 30000)
        })
      },
      inputChange() {
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
