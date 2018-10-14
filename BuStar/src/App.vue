<template>
  <div id="app">
    <Navbar/>
      <transition name="fade">
        <router-view/>
      </transition>
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

.fade-enter-active, .fade-leave-active {
    transition: opacity .5s
}
.fade-enter, .fade-leave-to  
{
    opacity: 0
}

</style>
