// Функция для регистрации пользователя
async function register(email, fullName, password) {
    try {
        const response = await fetch('http://localhost:5278/api/auth/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, fullName, password })
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Ошибка при регистрации');
        }

        alert('Регистрация прошла успешно!');
        return true;
    } catch (error) {
        console.error('Ошибка:', error);
        alert(error.message);
        return false;
    }
}

// Функция для входа пользователя
async function login(email, password) {
    try {
        const response = await fetch('http://localhost:5278/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Ошибка при входе');
        }

        const data = await response.json();
        localStorage.setItem('token', data.token); // Сохраняем токен в localStorage
        alert('Вход выполнен успешно!');
        return true;
    } catch (error) {
        console.error('Ошибка:', error);
        alert(error.message);
        return false;
    }
}

// Функция для выхода пользователя
export function logout() {
    localStorage.removeItem('token'); // Удаляем токен из localStorage
    alert('Вы вышли из системы.');
    window.location.href = 'index.html'; // Перенаправляем на страницу авторизации
}

// Функция для проверки авторизации
export function isAuthenticated() {
    return !!localStorage.getItem('token'); // Проверяем наличие токена
}

// Обработчик формы регистрации
document.getElementById('registerForm')?.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('regEmail').value;
    const fullName = document.getElementById('regFullName').value;
    const password = document.getElementById('regPassword').value;

    const success = await register(email, fullName, password);
    if (success) {
        window.location.href = 'tasks.html'; // Перенаправляем на страницу задач
    }
});

// Обработчик формы входа
document.getElementById('loginForm')?.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;

    const success = await login(email, password);
    if (success) {
        window.location.href = 'tasks.html'; // Перенаправляем на страницу задач
    }
});

// Обработчик кнопки выхода
document.getElementById('logoutButton')?.addEventListener('click', () => {
    logout();
});

// Показываем/скрываем формы в зависимости от авторизации
if (isAuthenticated()) {
    window.location.href = 'tasks.html'; // Если пользователь уже авторизован, перенаправляем на tasks.html
} else {
    document.getElementById('auth-forms').style.display = 'block';
    document.getElementById('logoutButton').style.display = 'none';
}