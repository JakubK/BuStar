<template>
  <div class="main_body">
     <p>BuStar</p>
    <form @submit.prevent="createTable()">
      <input v-model="searchInput" @input="inputChange" autocomplete="off" autocorrect="off" autocapitalize="off"
        spellcheck="false" type="text" autofocus placeholder="Start typing your stop's name">
      <input hidden type="submit">
      <ul v-if="showTips" class="tipsUl">
        <li v-for="stops in busStopsTips" :key="stops">{{ stops }}</li>
      </ul>
      <table class="tableTable" v-if="showTable">
        <tr>
          <th>Bus Line</th>
          <th>Head Sign</th>
          <th>Arrival Time from Time Table</th>
          <th>Estimated Arrival Time</th>
        </tr>
        <template v-for="(stopInfo, index) in stopDatas.stopInfos">
         <tr v-for="(busInfo, index) in stopInfo.busInfos" >
          <td>{{busInfo.routeID}}</td>
          <td>{{busInfo.headsign}}</td>
          <td>{{busInfo.theoreticalTime}}</td>
          <td>{{busInfo.estimatedTime}}</td>
         </tr>
        </template>

      <!-- 
        <tr v-for="(info, index) in stopDatas.stopInfos.busInfos" :key="info">
          <td>{{index}}</td>
        
         <td>{{this.info.stopInfos[0].busInfos[index].routeID}}</td>
          <td>{{this.info.stopInfos[0].busInfos[index].headsign}}</td>
          <td>{{this.info.stopInfos[0].busInfos[index].theoreticalTime}}</td>
          <td>{{this.info.stopInfos[0].busInfos[index].estimatedTime}}</td>-->
          </tr>
      </table>
    </form>
    <!--Not supported in most browsers
  <datalist  id="stopsList">
        <option value="test"></option>
            <option value="tata"></option>
    </datalist>                             -->

  </div>
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'bustar',
    components: {
      axios
    },
    data() {

      return {
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
          this.busStops = response.data,
          console.log(this.busStops)
        })
        .catch(error => {
          console.log(error.response)
        })
    },

    methods: {
      searchSubmit() {
        return axios.get("https://localhost:5001/api/bustar/stopdata/" + this.busStopsTips[0].replace('/', '*'))
          .then((response) => {
            this.stopDatas = response.data;
            return response.data;
          })
      },
      createTable(){
        this.searchSubmit().then(() =>
        {
          console.log(this.stopDatas)
          this.showTable=true;
          console.log(this.stopDatas.stopInfos[0].busInfos)
        })
      },
      inputChange() {
        if (this.searchInput != '') {
          this.showTips = true;
        } else {
          this.showTips = false;
        }
        this.busStopsTips = [];
        for (this.i = 0, this.tipsPostion = 0; this.i < Object.keys(this.busStops).length; this.i++) {
          if (this.busStops[this.i].toLowerCase().includes(this.searchInput.toLowerCase()) && this.tipsPostion < 20) {
            this.busStopsTips[this.tipsPostion++] = this.busStops[this.i];
          }
        }
      }
    }
    
  }

</script>

<style lang="scss" src="./Home.scss" scoped></style>
