import {api} from "../api";
import axios from "axios";
import { router } from "../router";

export const authModule = {
  namespaced: true,

  state: {
    loading: false,
    errorMessage: ''
  },
  mutations: {},
  actions: {
    async handleRegister({state, commit}, data) {
      try {
        state.loading = true;
        state.errorMessage = "";

        console.log("Регистрирую:", data);

        const response = await axios.post(
          `${api}/api/user/register`,
          data
        );
        console.log("Регистрация успешна:", response.data);

        const token = response.data.token;

        if (!token) {
          throw new Error("Токен не получен");
        }

        localStorage.setItem("token", token);

        // Сохраняем данные пользователя
        localStorage.setItem(
          "user",
          JSON.stringify({
            Name: this.userData.Name,
            Email: this.userData.Email,
            registrationTime: new Date().toISOString(),
          })
        );

        $router.path = '/'
      } catch (error) {
        console.error("Ошибка регистрации:", error);

        if (error.response?.status === 409) {
          this.errorMessage = "Пользователь уже существует";
        } else if (error.response?.status === 400) {
          this.errorMessage = "Неверные данные";
        } else {
          this.errorMessage =
            error.response?.data || error.message || "Ошибка регистрации";
        }
      } finally {
        this.loading = false;
      }
    },
  },
};
