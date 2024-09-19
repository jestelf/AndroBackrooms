using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Для определения кликов по объекту

public class SceneTeleportButton : MonoBehaviour, IPointerDownHandler
{
    public string[] sceneNames; // Массив имен сцен, на которые можно перейти

    public void OnPointerDown(PointerEventData eventData)
    {
        // Выбираем случайное имя сцены из массива
        int randomIndex = Random.Range(0, sceneNames.Length);
        string sceneName = sceneNames[randomIndex];

        // Загружаем выбранную сцену
        SceneManager.LoadScene(sceneName);
    }
}
