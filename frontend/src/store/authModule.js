import { api } from "../api";
import axios from "axios";
import { router } from "../router";

export const authModule = {
  namespaced: true,

  state: {
    loading: false,
    errorMessage: "",
    userData: null,
  },
  mutations: {
    setLoading(state, bool) {
      state.loading = bool
    },
    setErrorMessage(state, data) {
      state.errorMessage = data
    },
    setUserData(state, data) {
      state.userData = data
    }
  },
  actions: {
    async handleRegister({ state, commit }, data) {
      try {
        commit('setLoading', true)
        commit('setErrorMessage', '')

        console.log("Регистрирую:", data);

        const response = await axios.post(`${api}/api/user/register`, data);
        console.log("Регистрация успешна:", response.data);

        const token = response.data.token;

        if (!token) {
          throw new Error("Токен не получен");
        }

        localStorage.setItem("token", token);

        // Сохраняем данные пользователя
        commit('setUserData', data)

        router.replace({ path: "/" });
      } catch (error) {
        console.error("Ошибка регистрации:", error);

        if (error.response?.status === 409) {
          commit('setErrorMessage', "Пользователь уже существует")
        } else if (error.response?.status === 400) {
          commit('setErrorMessage', "Неверные данные")
        } else {
          commit('setErrorMessage', error.response?.data || error.message || "Ошибка регистрации")
        }
      } finally {
        commit('setLoading', false)
      }
    },
    async handleLogin({ state, commit }, data) {
      try {
        commit('setLoading', true)
        commit('setErrorMessage', '')

        console.log("Пытаюсь войти с:", data);

        // const API_URL = 'http://localhost:5046';

        const response = await axios.post(
          `${api}/api/user/login`,
          data
        );
        console.log("Ответ сервера:", response.data);

        const token = response.data.token;

        if (!token) {
          throw new Error("Токен не получен от сервера");
        }

        // Сохраняем токен
        localStorage.setItem("token", token);
        console.log("Токен сохранен");

        // Сохраняем основные данные пользователя
        commit('setUserData', data)

        // Перенаправляем в профиль
        router.replace({path: "/"});
      } catch (error) {
        console.error("Ошибка входа:", error);

        if (error.response?.status === 401) {
          commit('setErrorMessage', "Неверное имя пользователя или пароль")
        } else if (error.response?.status === 404) {
          commit('setErrorMessage', "Сервер не доступен. Попробуйте позже.")
        } else if (error.code === "NETWORK_ERROR") {
          commit('setErrorMessage', "Нет соединения с сервером")
        } else {
          commit('setErrorMessage', "error.response?.data || error.message || 'Ошибка входа'")            
        }
      } finally {
        commit('setLoading', false)
      }
    },
  },
};
