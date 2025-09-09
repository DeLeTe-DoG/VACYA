<template v-if="visibleMenu">
  <div class="sidebar">
    <div class="sidebar__wrapper">
      <h4 class="text">ПРОДАЖИ</h4>
      <div
        class="pen"
        :class="{ active: button.href === currentPath }"
        v-for="button in sidebutton"
      >
        <div
          class="pen__heading"
          @click="
            () => {
              $router.push(button.href);
              // currentPath = button.href;
            }
          "
        >
          <svg
            v-if="button.family == 'dashboard'"
            width="26"
            height="26"
            viewBox="0 0 26 26"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M15.166 3.25C15.7407 3.25 16.2918 3.47827 16.6981 3.8846C17.1044 4.29093 17.3327 4.84203 17.3327 5.41667V8.66667H21.666C22.2407 8.66667 22.7918 8.89494 23.1981 9.30127C23.6044 9.7076 23.8327 10.2587 23.8327 10.8333V20.5833C23.8327 21.158 23.6044 21.7091 23.1981 22.1154C22.7918 22.5217 22.2407 22.75 21.666 22.75H4.33268C3.75805 22.75 3.20695 22.5217 2.80062 22.1154C2.39429 21.7091 2.16602 21.158 2.16602 20.5833V14.0833C2.16602 13.5087 2.39429 12.9576 2.80062 12.5513C3.20695 12.1449 3.75805 11.9167 4.33268 11.9167H8.66602V5.41667C8.66602 4.84203 8.89429 4.29093 9.30062 3.8846C9.70695 3.47827 10.258 3.25 10.8327 3.25H15.166ZM15.166 5.41667H10.8327V20.5833H15.166V5.41667ZM21.666 10.8333H17.3327V20.5833H21.666V10.8333ZM8.66602 14.0833H4.33268V20.5833H8.66602V14.0833Z"
              fill="black"
            />
          </svg>
          <svg
            v-if="button.family == 'tests'"
            width="26"
            height="26"
            viewBox="0 0 26 26"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M20.5833 3.25C21.158 3.25 21.7091 3.47827 22.1154 3.8846C22.5217 4.29093 22.75 4.84203 22.75 5.41667V20.5833C22.75 21.158 22.5217 21.7091 22.1154 22.1154C21.7091 22.5217 21.158 22.75 20.5833 22.75H5.41667C4.84203 22.75 4.29093 22.5217 3.8846 22.1154C3.47827 21.7091 3.25 21.158 3.25 20.5833V5.41667C3.25 4.84203 3.47827 4.29093 3.8846 3.8846C4.29093 3.47827 4.84203 3.25 5.41667 3.25H20.5833ZM20.5833 9.75H5.41667V20.5833H20.5833V9.75ZM9.20833 16.25C9.49565 16.25 9.7712 16.3641 9.97437 16.5673C10.1775 16.7705 10.2917 17.046 10.2917 17.3333C10.2917 17.6207 10.1775 17.8962 9.97437 18.0994C9.7712 18.3025 9.49565 18.4167 9.20833 18.4167H8.125C7.83768 18.4167 7.56213 18.3025 7.35897 18.0994C7.1558 17.8962 7.04167 17.6207 7.04167 17.3333C7.04167 17.046 7.1558 16.7705 7.35897 16.5673C7.56213 16.3641 7.83768 16.25 8.125 16.25H9.20833ZM13.5417 16.25C13.8178 16.2503 14.0834 16.356 14.2842 16.5456C14.4849 16.7351 14.6058 16.9942 14.6219 17.2698C14.6381 17.5455 14.5484 17.8169 14.3712 18.0287C14.194 18.2404 13.9426 18.3765 13.6684 18.4091L13.5417 18.4167H12.4583C12.1822 18.4164 11.9166 18.3106 11.7159 18.1211C11.5151 17.9315 11.3942 17.6725 11.3781 17.3968C11.3619 17.1212 11.4516 16.8498 11.6288 16.638C11.806 16.4263 12.0574 16.2902 12.3316 16.2576L12.4583 16.25H13.5417ZM9.20833 11.9167C9.48445 11.917 9.75004 12.0227 9.95082 12.2123C10.1516 12.4018 10.2724 12.6609 10.2886 12.9365C10.3048 13.2122 10.2151 13.4836 10.0379 13.6953C9.86066 13.9071 9.60927 14.0431 9.33508 14.0758L9.20833 14.0833H8.125C7.84888 14.083 7.5833 13.9773 7.38252 13.7877C7.18174 13.5982 7.06091 13.3391 7.04473 13.0635C7.02855 12.7878 7.11823 12.5164 7.29545 12.3047C7.47268 12.0929 7.72406 11.9569 7.99825 11.9243L8.125 11.9167H9.20833ZM13.5417 11.9167C13.829 11.9167 14.1045 12.0308 14.3077 12.234C14.5109 12.4371 14.625 12.7127 14.625 13C14.625 13.2873 14.5109 13.5629 14.3077 13.766C14.1045 13.9692 13.829 14.0833 13.5417 14.0833H12.4583C12.171 14.0833 11.8955 13.9692 11.6923 13.766C11.4891 13.5629 11.375 13.2873 11.375 13C11.375 12.7127 11.4891 12.4371 11.6923 12.234C11.8955 12.0308 12.171 11.9167 12.4583 11.9167H13.5417ZM17.875 11.9167C18.1511 11.917 18.4167 12.0227 18.6175 12.2123C18.8183 12.4018 18.9391 12.6609 18.9553 12.9365C18.9715 13.2122 18.8818 13.4836 18.7045 13.6953C18.5273 13.9071 18.2759 14.0431 18.0018 14.0758L17.875 14.0833H16.7917C16.5155 14.083 16.25 13.9773 16.0492 13.7877C15.8484 13.5982 15.7276 13.3391 15.7114 13.0635C15.6952 12.7878 15.7849 12.5164 15.9621 12.3047C16.1393 12.0929 16.3907 11.9569 16.6649 11.9243L16.7917 11.9167H17.875ZM20.5833 5.41667H5.41667V7.58333H20.5833V5.41667Z"
              fill="black"
            />
          </svg>
          <p>{{ button.title }}</p>
        </div>
        <div class="pen__children" v-if="button.children">
          <ul>
            <li
              @click="
                () => {
                  $router.push(child.href);
                  // currentPath = child.href;
                }
              "
              v-for="child in button.children"
              :class="{ active: child.href === currentPath }"
            >
              <p>{{ child.title }}</p>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div class="sidebar__wrapper">
      <h4 class="text">АККАУНТ</h4>
      <button class="pen" @click="$router.push('/settings')">Настройки</button>
      <button class="pen" @click="$router.push('/exit')">Выйти</button>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { ref } from "vue";

export default {
  computed: {
    ...mapState({
      visibleMenu: (state) => state.visibleMenu,
    }),
    currentPath() {
      return this.$route.path;
    },
  },
  data() {
    return {
      // currentPath: "/",
      sidebutton: [
        {
          id: 1,
          title: "Дашборд",
          href: "/",
          family: "dashboard",
        },
        {
          id: 2,
          title: "Проверки",
          href: "/tests", //тут по базовому адресу будет история проверок
          family: "tests",
          children: [
            {
              id: 21,
              title: "Запланировать проверку",
              href: "/tests/plan-test",
              family: "tests",
            },
            {
              id: 22,
              title: "Отчет о проверке",
              href: "/tests/add-report",
              family: "tests",
            },
          ],
        },
        // {
        //   id: 3,
        //   title: "Что-нибудь ещё",
        // },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 250px;
  height: calc(100vh - 70px);
  background-color: rgb(255, 255, 255);
  padding: 25px 0;
  filter: drop-shadow(0 7px 4px rgba(0, 0, 0, 0.25));
}
.pen {
  background-color: transparent;
  border: none;
  width: 100%;
  height: max-content;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: start;
  padding-inline: 25px;
  font-size: 16px;
  &__heading {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 10px;
    height: 46px;
  }
  &__children ul {
    display: flex;
    flex-direction: column;
    // gap: 10px;
    list-style: none;
    padding-left: 25px;
    font-size: 14px;
    li{
      padding: 10px;
      border-top: 1px solid #f5f5f5;
      border-radius: 8px;
    }
    li:hover {
      p{
        color: #6490cf;
      }
    }
    li.active{
      background-color: rgba(65, 141, 255, 0.2);
      p{
        color: #418cff;
      }
    }
  }
  &.active {
    background-color: rgba(65, 141, 255, 0.2);
  }
  &.active &__heading {
    p {
      color: #418cff;
    }
    svg path{
      fill: #418cff;
    }
  }
  &:hover {
    background-color: rgba(173, 200, 239, 0.2);
    svg path {
      fill: #3d5e8b;
    }
    p{
      color: #3d5e8b;
    }
  }
  &:hover &__heading {
    color: #418cff;
  }
}

.text {
  padding-inline: 25px;
  color: rgba(150, 150, 150, 1);
  padding-bottom: 10px;
}
</style>
