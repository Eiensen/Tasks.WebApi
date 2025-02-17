import { register, login, logout, isAuthenticated } from './authService.js';

document.getElementById('registerForm')?.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('regEmail').value;
    const fullName = document.getElementById('regFullName').value;
    const password = document.getElementById('regPassword').value;

    try {
        const success = await register(email, fullName, password);
        if (success) {
            alert('Регистрация прошла успешно!');
            window.location.href = 'tasks.html';
        }
    } catch (error) {
        alert(error.message);
    }
});

document.getElementById('loginForm')?.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;

    try {
        const success = await login(email, password);
        if (success) {
            alert('Вход выполнен успешно!');
            window.location.href = 'tasks.html';
        }
    } catch (error) {
        alert(error.message);
    }
});

document.getElementById('logoutButton')?.addEventListener('click', () => {
    logout();
    alert('Вы вышли из системы.');
    window.location.href = 'index.html';
});

if (isAuthenticated()) {
    window.location.href = 'tasks.html';
} else {
    document.getElementById('auth-forms').style.display = 'block';
    document.getElementById('logoutButton').style.display = 'none';
}