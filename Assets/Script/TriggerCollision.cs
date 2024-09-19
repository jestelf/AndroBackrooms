using UnityEngine;
using UnityEngine.SceneManagement; // Для перезагрузки сцены или остановки игры

public class TriggerCollision : MonoBehaviour
{
    public GameObject restartCanvas; // Добавьте ссылку на Canvas через редактор Unity

    private void Start()
    {
        // Убедитесь, что restartCanvas выключен при старте игры
        if (restartCanvas != null)
            restartCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trigger")) // Предполагается, что у триггеров есть тег "Trigger"
        {
            // Останавливаем игру
            Time.timeScale = 0;
            
            // Показываем restartCanvas, предлагая игроку перезапустить уровень
            if (restartCanvas != null)
                restartCanvas.SetActive(true);
        }
    }
}
