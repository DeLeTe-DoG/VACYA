<template>
  <div class="register-form">
    <form @submit.prevent="handleRegister">
      <input v-model="userData.Name" type="text" placeholder="Name" required>
      <input v-model="userData.Email" type="email" placeholder="Email" required>
      <input v-model="userData.Password" type="password" placeholder="Password" required>
      <button type="submit">Зарегистрироваться</button>
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
      userData: {
        Name: '',
        Email: '',
        Password: '' 
      },
      errorMessage: ''
    };
  },
  methods: {
    async handleRegister() {
      try {
        this.errorMessage = '';
        
        console.log('Отправляемые данные:', this.userData);
        
        const response = await axios.post('https://vacya.onrender.com/api/user/register', this.userData);
        
        console.log('Response:', response);
        console.log('Response data:', response.data);

        const token = response.data.token;
        
        if (!token) {
          throw new Error('Токен не получен от сервера');
        }

        localStorage.setItem('authToken', token);
        
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        
        await this.fetchUserData();
        
        this.$router.push('/profile');
        
      } catch (error) {
        this.handleError(error);
      }
    },
    
    async fetchUserData() {
      try {
        console.log('Попытка получения данных пользователя после регистрации...');
        
        const token = localStorage.getItem('authToken');
        const response = await axios.get('https://vacya.onrender.com/api/user/me', {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        
        const user = response.data;
        localStorage.setItem('user', JSON.stringify(user));
        console.log('Данные пользователя получены после регистрации:', user);
        
      } catch (error) {
        console.warn('Не удалось получить данные пользователя после регистрации:', error);
        
        localStorage.setItem('user', JSON.stringify({
          Name: this.userData.Name,
          Email: this.userData.Email,
          isFallback: true,
          registrationTime: new Date().toISOString()
        }));
      }
    },
    
    handleError(error) {
      console.error('Ошибка регистрации:', error);
      
      if (error.response?.data) {
        const serverError = error.response.data;
        this.errorMessage = serverError.message || 
                           serverError.error ||
                           JSON.stringify(serverError);
      } else {
        this.errorMessage = error.message || 'Ошибка при регистрации';
      }
    }
  }
};
</script>