<template>
  <div class="login-form">
    <h2>Вход в систему</h2>
    <form @submit.prevent="handleLogin">
      <input v-model="credentials.Name" type="text" placeholder="Имя пользователя" required>
      <input v-model="credentials.Email" type="email" placeholder="Email" required>
      <input v-model="credentials.Password" type="password" placeholder="Пароль" required>
      <button type="submit" :disabled="loading">
        {{ loading ? 'Вход...' : 'Войти' }}
      </button>
    </form>
    
    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>

    <p class="register-link">
      Нет аккаунта? <router-link to="/register">Зарегистрироваться</router-link>
    </p>
    <router-link class="register-link" to="/">Назад</router-link>
  </div>
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
      errorMessage: '',
      loading: false
    };
  },
  methods: {
    async handleLogin() {
      try {
        this.loading = true;
        this.errorMessage = '';
        
        console.log('Пытаюсь войти с:', this.credentials);
        
        // Пробуем разные URL
        const API_URL = 'https://vacya.onrender.com';
        // const API_URL = 'http://localhost:5046';
        
        const response = await axios.post(`${API_URL}/api/user/login`, this.credentials);
        console.log('Ответ сервера:', response.data);
        
        const token = response.data.token;
        
        if (!token) {
          throw new Error('Токен не получен от сервера');
        }

        // Сохраняем токен
        localStorage.setItem('authToken', token);
        console.log('Токен сохранен');
        
        // Сохраняем основные данные пользователя
        localStorage.setItem('user', JSON.stringify({
          Name: this.credentials.Name,
          loginTime: new Date().toISOString()
        }));
        
        // Перенаправляем в профиль
        this.$router.push('/profile');
        
      } catch (error) {
        console.error('Ошибка входа:', error);
        
        if (error.response?.status === 401) {
          this.errorMessage = 'Неверное имя пользователя или пароль';
        } else if (error.response?.status === 404) {
          this.errorMessage = 'Сервер не доступен. Попробуйте позже.';
        } else if (error.code === 'NETWORK_ERROR') {
          this.errorMessage = 'Нет соединения с сервером';
        } else {
          this.errorMessage = error.response?.data || error.message || 'Ошибка входа';
        }
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style scoped>
.login-form {
  max-width: 400px;
  margin: 50px auto;
  padding: 30px;
  border: 1px solid #ddd;
  border-radius: 10px;
  background: white;
  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
}

h2 {
  text-align: center;
  margin-bottom: 30px;
  color: #333;
}

input {
  width: 100%;
  padding: 12px;
  margin: 10px 0;
  border: 1px solid #ddd;
  border-radius: 5px;
  font-size: 16px;
  box-sizing: border-box;
}

input:focus {
  outline: none;
  border-color: #4CAF50;
}

button {
  width: 100%;
  padding: 12px;
  background: #4CAF50;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover:not(:disabled) {
  background: #45a049;
}

button:disabled {
  background: #ccc;
  cursor: not-allowed;
}

.error-message {
  color: #d32f2f;
  background: #ffebee;
  padding: 12px;
  border-radius: 5px;
  margin: 15px 0;
  border: 1px solid #ffcdd2;
}

.register-link {
  text-align: center;
  margin-top: 20px;
  color: #666;
}

.register-link a {
  color: #4CAF50;
  text-decoration: none;
}

.register-link a:hover {
  text-decoration: underline;
}
</style>