// Функция для добавления токена в заголовки запросов
export function addAuthHeader(headers = {}) {
    const token = localStorage.getItem('token'); // Получаем токен из localStorage
    if (token) {
        headers['Authorization'] = `Bearer ${token}`; // Добавляем токен в заголовок
    }
    return headers;
}