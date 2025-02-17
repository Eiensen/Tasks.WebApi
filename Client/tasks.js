import { logout, isAuthenticated } from "./authService.js";
import { addAuthHeader } from "./utils.js";

if (!isAuthenticated()) {
  console.log("Пользователь не авторизован, перенаправление...");
  window.location.href = "index.html";
}

document.getElementById("logoutButton").addEventListener("click", () => {
  console.log("Выход выполнен");
  logout();
});

// Функция для загрузки задач с сервера
const loadTasks = async () => {
  try {
    console.log("Загрузка задач...");
    const response = await fetch("http://localhost:5278/api/tasks", {
      headers: addAuthHeader(),
    });
    if (!response.ok) console.log("Ошибка при загрузке задач");
    const tasks = await response.json();

    const taskTableBody = document.querySelector("#taskTable tbody");
    const totalTimeElement = document.getElementById("totalTime");

    taskTableBody.innerHTML = "";
    let totalTime = 0;

    tasks.forEach((task) => {
      const row = document.createElement("tr");
      row.innerHTML = `
                <td>${new Date(task.taskDate).toLocaleDateString()}</td>
                <td>${task.description}</td>
                <td>${task.timeSpent}</td>
                <td>${task.assignee}</td>
            `;
      taskTableBody.appendChild(row);

      const [hours, minutes] = task.timeSpent.split(":");
      totalTime += parseInt(hours) * 60 + parseInt(minutes);
    });

    const hours = Math.floor(totalTime / 60);
    const minutes = totalTime % 60;
    totalTimeElement.textContent = `${hours}:${minutes
      .toString()
      .padStart(2, "0")}`;
  } catch (error) {
    console.error("Ошибка:", error);
  }
};

document.getElementById("logoutButton")?.addEventListener("click", () => {
  logout();
  console.log("Вы вышли из системы.");
  window.location.href = "index.html";
});

document
  .getElementById("taskForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();
    console.log("Форма отправлена");

    const description = document.getElementById("description").value.trim();
    const timeSpent = document.getElementById("timeSpent").value.trim();

    if (!description || !timeSpent) {
      alert("Пожалуйста, заполните все поля.");
      return;
    }

    const task = {
      TaskDate: new Date().toISOString().split("T")[0],
      Description: description,
      TimeSpent: timeSpent + ":00",
    };

    try {
      const response = await fetch("http://localhost:5278/api/tasks", {
        method: "POST",
        headers: addAuthHeader({ "Content-Type": "application/json" }),
        body: JSON.stringify(task),
      });

      if (!response.ok) throw new Error("Ошибка при добавлении задачи");
      await loadTasks();
      document.getElementById("taskForm").reset();
    } catch (error) {
      console.error("Ошибка:", error);
      alert("Не удалось добавить задачу. Пожалуйста, попробуйте позже.");
    }
  });

loadTasks();
