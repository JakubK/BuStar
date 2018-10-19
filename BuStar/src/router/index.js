import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home/Home'
import About from '@/components/About/About'

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
      path: '/:stop',
      component: Home,
      props: true
    },
    {
      path: '/about',
      name: 'about',
      component: About
    }
  ]
})
