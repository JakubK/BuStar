<template>
<div id="main">
<navbar></navbar>
<start_page></start_page>
<search_engine></search_engine>
<foot></foot>
</div>
</template>

<script>
  import navbar from './navbar'
  import start_page from './start_page'
  import search_engine from'./search_engine'
  import foot from './foot'

  export default {
    name: 'bustar',
    components: {
      'navbar': navbar,
      'start_page': start_page,
	  'search_engine': search_engine,
      'foot': foot
      }
    }
//     new Vue({
//       el: '#app',
//       components: {
//         'navbar': navbar,
//         'start_page': start_page,
//         'foot': foot
//         }
//       })
  </script>
