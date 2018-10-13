<template>
  <div class="main_div">
    <p>BuStar</p>
    <form>
        <input v-model="searchInput" @input="inputChange" autocomplete="off" autocorrect="off" autocapitalize="off" spellcheck="false" type="text" autofocus
          placeholder="Start typing your stop's name">
        <ul v-if="showTips" class="tipsUl" >
            <li v-for="stops in busStopsTips" :key="stops">{{ stops }}</li>
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
        busStops:null,
        busStopsTips:[],
        searchInput: ''
       }
    },

    mounted(){
    //Unncomment when api starts working
    
  axios.get("https://localhost:5001/api/bustar/buses")
  .then((response) => {
    this.busStops = response.data,
    console.log(this.busStops)
  })
  .catch(error => {
    console.log(error.response)
})
},
    methods:{
      
        inputChange(){
            if(this.searchInput!='')
            {this.showTips=true;}
            else
            {this.showTips=false;}
            this.busStopsTips=[];
            for(this.i=0,this.tipsPostion=0;this.i<Object.keys(this.busStops).length;this.i++)
            {
              if(this.busStops[this.i].toLowerCase().includes(this.searchInput.toLowerCase())&&this.tipsPostion<20)
              {
                this.busStopsTips[this.tipsPostion++]=this.busStops[this.i];
              }
            }
        }
    }
    }
</script>

<style src="./search_engine.scss" scoped>

</style>
