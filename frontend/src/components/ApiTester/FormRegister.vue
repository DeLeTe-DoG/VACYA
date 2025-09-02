<template>
  <div class='main'>
    <div class="register-form">
      <h2>Регистрация</h2>
      <form @submit.prevent="handleRegister">
        <input v-model="userData.Name" type="text" placeholder="Имя" required>
        <input v-model="userData.Email" type="email" placeholder="Email" required>
        <input v-model="userData.Password" type="password" placeholder="Пароль" required>
        <button type="submit" :disabled="loading">
          {{ loading ? 'Регистрация...' : 'Зарегистрироваться' }}
        </button>
      </form>
      
      <div v-if="errorMessage" class="error-message">
        {{ errorMessage }}
      </div>
    
      <p class="login-link">
        Есть аккаунт? <router-link to="/login">Войти</router-link>
      </p>
      <router-link class="register-link" to="/">Назад</router-link>
    </div>
    
    <img class="picture" src="/src/assets/images/cool-image-from-behind.png">
  </div>
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
      errorMessage: '',
      loading: false
    };
  },
  methods: {
    async handleRegister() {
      try {
        this.loading = true;
        this.errorMessage = '';
        
        console.log('Регистрирую:', this.userData);
        
        const API_URL = 'https://vacya.onrender.com';
        
        const response = await axios.post(`${API_URL}/api/user/register`, this.userData);
        console.log('Регистрация успешна:', response.data);
        
        const token = response.data.token;
        
        if (!token) {
          throw new Error('Токен не получен');
        }

        localStorage.setItem('authToken', token);
        
        // Сохраняем данные пользователя
        localStorage.setItem('user', JSON.stringify({
          Name: this.userData.Name,
          Email: this.userData.Email,
          registrationTime: new Date().toISOString()
        }));
        
        this.$router.push('/profile');
        
      } catch (error) {
        console.error('Ошибка регистрации:', error);
        
        if (error.response?.status === 409) {
          this.errorMessage = 'Пользователь уже существует';
        } else if (error.response?.status === 400) {
          this.errorMessage = 'Неверные данные';
        } else {
          this.errorMessage = error.response?.data || error.message || 'Ошибка регистрации';
        }
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style scoped>
.main{
  display: flex;
  flex-direction: row;
}

.picture{
  width: 27vw;
  height: 96vh;
  border-radius:30px;
  margin-block: 2vh;
  margin-right: 2vh;

}

.register-form {
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
  border-color: #2196F3;
}

button {
  width: 100%;
  padding: 12px;
  background: #2196F3;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover:not(:disabled) {
  background: #1976D2;
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

.login-link {
  text-align: center;
  margin-top: 20px;
  color: #666;
}

.login-link a {
  color: #2196F3;
  text-decoration: none;
}

.login-link a:hover {
  text-decoration: underline;
}
</style>