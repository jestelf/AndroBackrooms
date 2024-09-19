using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    public GameObject escapeCanvas; // Ссылка на ваше меню паузы в иерархии

    private bool isPaused = false; // Состояние паузы игры

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused; // Переключаем состояние паузы
        escapeCanvas.SetActive(isPaused); // Активируем или деактивируем меню паузы

        if (isPaused)
        {
            Time.timeScale = 0; // Останавливаем время в игре
        }
        else
        {
            Time.timeScale = 1; // Возобновляем ход времени
        }
    }
}
