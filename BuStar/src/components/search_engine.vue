<template>
  <div class="main_div">
    <p>BuStar</p>
    <form>
        <input @input="inputChange" autocomplete="off" autocorrect="off" autocapitalize="off" spellcheck="false" type="text" autofocus
          placeholder="Start typing your stop's name">
        <ul v-if="showTips" class="tipsUl" >
            <li v-for="stops in busStops">{{ stops }}</li>
        </ul>
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
    components: {axios},
    data(){
    return{
        showTips:false,
        //clear when api starts working
        busStops:["Wały Piastowskie","Aksamitna","Rybaki Górne","Toruńska","Łagiewniki","Port Lotniczy","Dworzec Główny"]
       }
    },
    mounted(){
    //Unncomment when api starts working
 /* axios.get("https://localhost:5001/api/bustar/buses")
  .then((response) => {
    this.busStops = response.data,
    console.log(this.busStops)
  })*/
},
    methods:{
        inputChange(){
            this.showTips=true;
        }
    }
    }
</script>

<style src="./search_engine.scss" scoped>

</style>
