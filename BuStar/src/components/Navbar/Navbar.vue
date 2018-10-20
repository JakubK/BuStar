 <template>
    <div class="project-nav">
    <div class="project-name-header"><img src="../../assets/BuStar_5.svg"/></div>
      <div class="main-nav" v-bind:class="{navbarToggled : navbarToggled}">
        <router-link to="/" class="nav-item">Home</router-link>
        <router-link to="/about" class="nav-item">About</router-link>
        <router-link to="/stops" class="nav-item">Stops</router-link>
      </div>
      <div class="right"></div>
      <div class="navigation-burger" @click="toggleNavbar">
        <span class="burger-segment"/>
      </div>
    </div>
</template>
<script>
export default {
  name: "Navbar",
  data()
  {
    return{
      navbarToggled: false
    }
  },
  methods:
  {
    toggleNavbar()
    {
      this.navbarToggled = !this.navbarToggled;
    }
  }
}
</script>


<style lang="scss" src="./Navbar.scss" scoped></style>