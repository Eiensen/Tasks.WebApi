import { logout, isAuthenticated } from './authService.js';
import { addAuthHeader } from './utils.js';

// Если пользователь не авторизован, перенаправляем на страницу авторизации
if (!isAuthenticated()) {
    console.log('Пользователь не авторизован, перенаправление...');
    window.location.href = 'index.html';
}

// Обработчик кнопки выхода
document.getElementById('logoutButton').addEventListener('click', () => {
    console.log('Выход выполнен');
    logout();
});

// Функция для загрузки задач с сервера
const loadTasks = async () => {
    try {
        console.log('Загрузка задач...');
        const response = await fetch('http://localhost:5278/api/tasks', {
            headers: addAuthHeader() // Добавляем токен в заголовки
        });
        console.log(response.json());
        if (!response.ok) 
            console.log('Ошибка при загрузке задач');
        const tasks = await response.json();
        console.log('Задачи:', tasks);

        const taskTableBody = document.querySelector('#taskTable tbody');
        const totalTimeElement = document.getElementById('totalTime');

        taskTableBody.innerHTML = '';
        let totalTime = 0;

        tasks.forEach(task => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${new Date(task.taskDate).toLocaleDateString()}</td>
                <td>${task.description}</td>
                <td>${task.timeSpent}</td>
                <td>${task.executor}</td>
            `;
            taskTableBody.appendChild(row);

            const [hours, minutes] = task.timeSpent.split(':');
            totalTime += parseInt(hours) * 60 + parseInt(minutes);
        });

        const hours = Math.floor(totalTime / 60);
        const minutes = totalTime % 60;
        totalTimeElement.textContent = `${hours}:${minutes.toString().padStart(2, '0')}`;
    } catch (error) {
        console.error('Ошибка:', error);        
    }
};

// Обработчик отправки формы
document.getElementById('taskForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    console.log('Форма отправлена');

    const description = document.getElementById('description').value.trim();
    const timeSpent = document.getElementById('timeSpent').value.trim();

    if (!description || !timeSpent) {
        alert('Пожалуйста, заполните все поля.');
        return;
    }

    const task = {
        taskDate: new Date().toISOString().split('T')[0],
        description,
        timeSpent,
        executor: 'Текущий пользователь'
    };

    try {
        const response = await fetch('http://localhost:5278/api/tasks', {
            method: 'POST',
            headers: addAuthHeader({ 'Content-Type': 'application/json' }),
            body: JSON.stringify(task)
        });

        if (!response.ok) throw new Error('Ошибка при добавлении задачи');
        await loadTasks();
        document.getElementById('taskForm').reset();
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Не удалось добавить задачу. Пожалуйста, попробуйте позже.');
    }
});

loadTasks();