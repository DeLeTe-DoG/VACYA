<script>
import { mapState } from "vuex";

export default {
  computed: {
    ...mapState({
      visibleMenu: state => state.visibleMenu,
    }),
  },
};
</script>

<template class="sideb" v-if="visibleMenu">
  <div class="sidebar" >
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
    <p>penis</p>
  </div>
</template>

<style lang="scss" scoped>
.sidebar {
  width: 360px;
  height: 100px;
  background-color: rgb(255, 255, 255);
  position: fixed;
  align-self: start;
  top: 0;
  left: 0;
  z-index: 10;
  background-color: #000;
  width: 200px;
  height: 200px;
}
</style>
