<template>
  <div class="grid-structure">
    <div class="window">
      <div class="window__header">
        <h2 class="window__title">Общие данные</h2>
      </div>
      <div class="window__body">
        <div class="metrics-cards">
          <div class="metric" v-for="metric in metrics">
            <did
              class="metric__marker"
              :style="{ backgroundColor: metric.indicator }"
            ></did>
            <p class="metric__title">{{ metric.title }}</p>
            <h2 class="metric__value">
              {{ metric.value }} {{ metric.measure }}
            </h2>
            <p class="metric__description">{{ metric.description }}</p>
          </div>
        </div>
        <MainChart />
      </div>
    </div>
    <div class="column-wrapper">
      <div class="window" v-if="sites">
        <div class="window__header">
          <span>
            <h2 class="window__title">Отображаемые сайт</h2>
            <p class="window__subtitle">Выберите сайт, график которого хотите увидеть</p>
          </span>
          
          <main-button
            class="add-site-btn"
            @click="$router.push({ path: '/add-site' })"
          >
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M8 1V15M1 8H15"
                stroke="white"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </main-button>
        </div>
        <div class="window__body">
          <div class="choice-row" v-for="site in sites">
            <p class="choice-row__name">{{ site.url }}</p>
            <button class="edit-btn">
              <svg
                width="20"
                height="20"
                viewBox="0 0 20 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M17.2266 2.30078L17.6992 2.77344C18.0664 3.14062 18.0664 3.73438 17.6992 4.09766L16.5625 5.23828L14.7617 3.4375L15.8984 2.30078C16.2656 1.93359 16.8594 1.93359 17.2227 2.30078H17.2266ZM8.19531 10.0078L13.4375 4.76172L15.2383 6.5625L9.99219 11.8047C9.87891 11.918 9.73828 12 9.58594 12.043L7.30078 12.6953L7.95312 10.4102C7.99609 10.2578 8.07812 10.1172 8.19141 10.0039L8.19531 10.0078ZM14.5742 0.976562L6.86719 8.67969C6.52734 9.01953 6.28125 9.4375 6.15234 9.89453L5.03516 13.8008C4.94141 14.1289 5.03125 14.4805 5.27344 14.7227C5.51562 14.9648 5.86719 15.0547 6.19531 14.9609L10.1016 13.8438C10.5625 13.7109 10.9805 13.4648 11.3164 13.1289L19.0234 5.42578C20.1211 4.32812 20.1211 2.54688 19.0234 1.44922L18.5508 0.976562C17.4531 -0.121094 15.6719 -0.121094 14.5742 0.976562ZM3.4375 2.5C1.53906 2.5 0 4.03906 0 5.9375V16.5625C0 18.4609 1.53906 20 3.4375 20H14.0625C15.9609 20 17.5 18.4609 17.5 16.5625V12.1875C17.5 11.668 17.082 11.25 16.5625 11.25C16.043 11.25 15.625 11.668 15.625 12.1875V16.5625C15.625 17.4258 14.9258 18.125 14.0625 18.125H3.4375C2.57422 18.125 1.875 17.4258 1.875 16.5625V5.9375C1.875 5.07422 2.57422 4.375 3.4375 4.375H7.8125C8.33203 4.375 8.75 3.95703 8.75 3.4375C8.75 2.91797 8.33203 2.5 7.8125 2.5H3.4375Z"
                  fill="#969696"
                />
              </svg>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex/dist/vuex.cjs.js";
import MainChart from "../components/layouts/main-page/MainChart.vue";

export default {
  components: { MainChart },
  data() {
    return {
      token: null,
      user_data: null,
      userData: null,
      metrics: [
        // {
        //   id: 1,
        //   indicator: "#418DDF",
        //   title: "Total time",
        //   value: "3000",
        //   measure: "ms",
        //   description: "Итоговое время загрузки страницы",
        // },
        {
          id: 1,
          indicator: "#D741DF",
          title: "Кол-во ошибок",
          value: "2",
          measure: "",
          description: "За последние 24 часа",
        },
        {
          id: 2,
          indicator: "#DF4141",
          title: "DNS Lookup time",
          value: "1000",
          measure: "ms",
          description:
            "Время преобразования доменного имени в IP-адрес через DNS",
        },
      ],
    };
  },
  computed: {
    ...mapState({
      sites: (state) => state.sites.sites,
    }),
  },
  methods: {
    ...mapActions({
      getSites: "sites/getSites",
    }),
  },
  mounted() {
    this.token = localStorage.getItem("token");
    this.getSites();
  },
};
</script>

<style lang="scss" scoped>
.metrics-cards {
  display: flex;
  flex-direction: row;
  gap: 15px;
  border-radius: 16px;
  padding: 10px;
  background-color: #d9d9d9;
  height: 290px;
  .metric {
    width: 100%;
    background-color: #fff;
    padding: 25px;
    border-radius: 12px;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    &__marker {
      width: 70px;
      height: 70px;
      border-radius: 50%;
    }
    &__title {
      font-size: 20px;
    }
    &__value {
      font-size: 36px;
    }
    &__description {
      font-size: 15px;
    }
  }
}
.add-site-btn {
  padding: unset;
  aspect-ratio: 1/1;
}
.choice-row{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding-top: 20px;
  border-top: 1px solid #f5f5f5;
}
</style>
