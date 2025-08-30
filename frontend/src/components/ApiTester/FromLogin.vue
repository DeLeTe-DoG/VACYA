<template>
  <div class="login-form">
    <form @submit.prevent="handleLogin">
      <input v-model="credentials.Name" type="text" placeholder="Name" required>
      <input v-model="credentials.Email" type="email" placeholder="Email" required>
      <input v-model="credentials.Password" type="password" placeholder="Password" required>
      <button type="submit">Войти</button>
    </form>
    
    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>
  </div>
  <router-link to="/">go back to reality</router-link>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      credentials: {
        Email: '',
        Name: '',
        Password: ''
      },
      errorMessage: ''
    };
  },
  methods: {
    async handleLogin() {
      try {
        this.errorMessage = '';
        
        console.log('Отправляемые данные:', this.credentials);
        
        const response = await axios.post('https://vacya.onrender.com/api/user/login', this.credentials);
        console.log('Ответ сервера:', response.data);
        

        const token = response.data.token;
        
        if (!token) {
          throw new Error('Токен не получен от сервера');
        }

        localStorage.setItem('authToken', token);     

        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        
        await this.fetchUserData();
        
        this.$router.push('/profile');
        
      } catch (error) {
        console.error('Ошибка авторизации:', error);
        
        if (error.response?.status === 400) {
          this.errorMessage = 'Неверный формат данных';
        } else if (error.response?.status === 401) {
          this.errorMessage = error.response.data || 'Неверное имя пользователя или пароль';
        } else if (error.message.includes('fetchUserData')) {
          this.errorMessage = 'Ошибка получения данных пользователя';
        } else {
          this.errorMessage = error.message || 'Ошибка сервера';
        }
      }
    },
    
    async fetchUserData() {
      try {
        const response = await axios.get('https://vacya.onrender.com/api/user/me');
        const user = response.data;
        localStorage.setItem('user', JSON.stringify(user));
        console.log('Данные пользователя получены:', user);
      } catch (error) {
        console.warn('Не удалось получить данные пользователя, продолжаем без них:', error);
        const userData = {
          Name: this.credentials.Name,
        };
        localStorage.setItem('user', JSON.stringify(userData));
      }
    }
  }
};
</script>

<style scoped>
.error-message {
  color: red;
  margin-top: 10px;
  padding: 10px;
  border: 1px solid red;
  border-radius: 4px;
}
</style>