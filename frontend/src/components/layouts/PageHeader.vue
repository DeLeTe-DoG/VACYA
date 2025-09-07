<template>
  <header class="header">
    <div class="side">
      <!-- <a class="btn_menu" @click="openMenu"></a> 
        <a class="btn_search" ></a> -->
      <img
        @click.stop="toggleMenu"
        class="btn_search"
        src="/src/assets/images/icons/menu-icon.svg"
        onmouseover="this.src='/src/assets/images/icons/active-menu-icon.svg';"
        onmouseout="this.src='/src/assets/images/icons/menu-icon.svg';"
      />
      <img
        class="btn_search"
        src="/src/assets/images/icons/search-icon.svg"
        onmouseover="this.src='/src/assets/images/icons/active-search-icon.svg';"
        onmouseout="this.src='/src/assets/images/icons/search-icon.svg';"
      />
    </div>
    <div class="side">
      <img
        class="btn_notifications"
        src="/src/assets/images/icons/notifications-icon.svg"
        onmouseover="this.src='/src/assets/images/icons/active-notifications-icon.svg';"
        onmouseout="this.src='/src/assets/images/icons/notifications-icon.svg';"
      />
      <img
        class="btn_settings"
        src="/src/assets/images/icons/settings-icon.svg"
        onmouseover="this.src='/src/assets/images/icons/active-settings-icon.svg';"
        onmouseout="this.src='/src/assets/images/icons/settings-icon.svg';"
      />
      <!-- <a class="btn_notifications"></a> 
        <a class="btn_settings"></a> -->
      <div class="btn_logo">А</div>
    </div>
  </header>
</template>

<script>
import { openBlock } from "vue";
import { mapState } from "vuex";
import { mapActions, mapMutations } from "vuex/dist/vuex.cjs.js";
// import openMenu from "/src/store/index.js";

export default {
  computed: {
    ...mapState({
      visibleMenu: (state) => state.visibleMenu,
    }),
  },
  methods: {
    ...mapMutations({
        toggleMenu: 'toggleMenu',
    }),
  }
};
</script>


<style lang="scss" scoped>
.header {
  height: 70px;
  background-color: rgb(255, 255, 255);
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  filter: drop-shadow(0px 4px 4px rgba(0, 0, 0, 0.25));
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

.rightside {
  display: flex;
  align-items: center;
  margin-right: 25px;
  gap: 25px;
}

.side {
  display: flex;
  align-items: center;
  margin-inline: 25px;
  gap: 25px;
}

.btn_logo {
  width: 40px;
  height: 40px;
  cursor: pointer;
  background-color: rgba(65, 140, 255, 0.5);
  border-radius: 100px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 17px;
}
//я честно не бубу как по-другому эти ховеры мутить, сори. попробую сделать через js как-нибудь...
.btn_search {
  width: 28px;
  height: 28px;
  cursor: pointer;
  // background-image: url(/src/assets/images/icons/search-icon.svg);
}
// :hover.btn_search{
//         background-image: url(/src/assets/images/icons/active-search-icon.svg);
//         transition:cubic-bezier(1, 0, 0, 1), 200ms;
//     }

.btn_menu {
  width: 28px;
  height: 28px;
  cursor: pointer;
  // background-image: url(/src/assets/images/icons/menu-icon.svg)
}
// :hover.btn_menu{
//     background-image: url(/src/assets/images/icons/active-menu-icon.svg);
//     transition:cubic-bezier(1, 0, 0, 1), 200ms;
// }

.btn_notifications {
  width: 28px;
  height: 28px;
  cursor: pointer;
  // background-image: url(/src/assets/images/icons/notifications-icon.svg)
}
// :hover.btn_notifications{
//     background-image: url(/src/assets/images/icons/active-notifications-icon.svg);
//     transition:cubic-bezier(1, 0, 0, 1), 200ms;
// }

.btn_settings {
  width: 28px;
  height: 28px;
  cursor: pointer;
  // background-image: url(/src/assets/images/icons/settings-icon.svg)
}
// :hover.btn_settings{
//     background-image: url(/src/assets/images/icons/active-settings-icon.svg);
//     transition:cubic-bezier(1, 0, 0, 1), 200ms;
// }
</style>
