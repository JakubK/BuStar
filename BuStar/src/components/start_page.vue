 <template>
 <div class="main_body">
 <search_engine></search_engine>
 </div>
</template>

<script>
import search_engine from'./search_engine'
export default {
  name: 'bustar',
  components: {
    'search_engine': search_engine
    }
  }
</script>

<style lang="scss" src="./start_page.scss" scoped></style>
