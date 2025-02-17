export async function register(email, fullName, password) {
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

        return true;
    } catch (error) {
        console.error('Ошибка:', error);
        throw error;
    }
}

export async function login(email, password) {
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
        localStorage.setItem('token', data.token);
        return true;
    } catch (error) {
        console.error('Ошибка:', error);
        throw error;
    }
}

export function logout() {
    localStorage.removeItem('token');
}

export function isAuthenticated() {
    return !!localStorage.getItem('token');
}