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
      <div class="window">
        <div class="window__header">
            <h2 class="window__title">Something realy good will be here...</h2>
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
        {
          id: 1,
          indicator: "#418DDF",
          title: "Total time",
          value: "3000",
          measure: "ms",
          description: "Итоговое время загрузки страницы",
        },
        {
          id: 2,
          indicator: "#D741DF",
          title: "Content download",
          value: "2000",
          measure: "ms",
          description: "Время загрузку страницы",
        },
        {
          id: 3,
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
    // ...mapState({
    //     userData: state => state.auth.userData
    // }),
  },
  methods: {
    ...mapActions({
      getUserData: "getUserData",
    }),
  },
  mounted() {
    this.token = localStorage.getItem("token");
    this.userData = localStorage.getItem("user");
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
</style>
