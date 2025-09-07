
<template v-if="visibleMenu" >
  <div class="sidebar" >
    <h4 class="text">ПРОДАЖИ</h4>
    <div class="pen" v-for="button in sidebutton">
      <div class="btn_text"> {{ button.title }} </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { ref } from 'vue';

export default {
  computed: {
    ...mapState({
      visibleMenu: state => state.visibleMenu,
    }),
  },
  data() {
    return {
      sidebutton: [
      {id: 1, title: 'Дашборд'},
      {id: 2, title: 'Отчёты'},
      {id: 3, title: 'Что-нибудь ещё'},
      {id: 4, title: 'Настройки'},
      {id: 5, title: 'Выход'}
    ]
    }
  },
};
</script>

<style lang="scss" scoped>
.sidebar {
  width: 250px;
  height: calc(100vh - 70px);
  background-color: rgb(255, 255, 255);
  align-self: start;
  padding: 25px;
  top: 0;
  left: 0;
  z-index: 10;
  box-sizing: border-box;
  filter: drop-shadow(0 7px 4px rgba(0, 0, 0, 0.25));
}
.pen{
  width: 100%;
  height: 46px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: start;
  margin-bottom: 10px;
  cursor: pointer;
  padding-inline: 25px;
  color: red;
}
:hover.pen{
  background-color: rgba(65, 140, 255, 0.2);
}

.text{
  padding-inline: 25px;
  color: rgba(150, 150, 150, 1);
  padding-bottom: 10px;
}

:hover.btn_text{
  color: rgba(65, 140, 255);
}
</style>
