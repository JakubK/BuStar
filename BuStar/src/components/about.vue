<template>
<div id="main">
<navbar></navbar>
<about_page></about_page>
<foot></foot>
</div>
</template>

<script>
import navbar from './navbar'
import start_page from './start_page'
import foot from './foot'
import about_page from './about_page'

  export default {
    name: 'bustar',
    components: {
      'navbar': navbar,
      'about_page': about_page,
      'foot': foot
      }
    }
</script>
