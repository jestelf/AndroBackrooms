using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // Необходимо для доступа к EditorApplication
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Загрузите сцену Level1, замените "Level1" на имя вашей сцены
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        // Останавливаем игру в редакторе
        EditorApplication.ExitPlaymode();
        #else
        // Закрываем приложение в собранной версии
        Application.Quit();
        #endif
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        // Перезагрузите текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
