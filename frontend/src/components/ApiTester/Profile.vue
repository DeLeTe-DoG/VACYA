<template>
  <div class="user-profile">
    <h1>Добро пожаловать, {{ user.name }}!</h1>
    <p>Email: {{ user.email }}</p>
    <button @click="handleLogout">Выйти</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      user: {}
    };
  },
  mounted() {
    this.loadUserData();
  },
  methods: {
    loadUserData() {
      const userData = localStorage.getItem('user');
      if (userData) {
        this.user = JSON.parse(userData);
      }
    },
    handleLogout() {
      localStorage.removeItem('authToken');
      localStorage.removeItem('user');
      this.$router.push('/login');
    }
  }
};
</script>