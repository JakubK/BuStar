import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home/Home'
import About from '@/components/About/About'
import Stops from '@/components/Stops/Stops'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/stop/:stopName',
      name: 'homestop',
      component: Home,
      props: true
    },
    {
      path:'/stops',
      name:'stops',
      component: Stops
    },
    {
      path: '/about',
      name: 'about',
      component: About
    }
  ]
})
