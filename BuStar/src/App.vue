<template>
  <div id="app">
    <!--<img src="./assets/logo.png">-->
    <Navbar/>
    <router-view/>
    <Footer/>
  </div>
</template>

<script>
import Navbar from './components/Navbar/Navbar'
import Footer from './components/Footer/Footer'


export default {
  name: 'App',
  components:
  {
    Navbar, Footer
  }
}
</script>

<style>
body {
  margin: 0px;
}
</style>
