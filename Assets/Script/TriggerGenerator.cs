using UnityEngine;

public class TriggerGenerator : MonoBehaviour
{
    public GameObject[] triggerPrefabs; // Массив префабов триггеров
    public float[] triggerYPositions; // Массив для хранения фиксированных значений Y для каждого префаба
    public Camera mainCamera; // Главная камера в сцене
    public float generationIntervalX = 20f; // Минимальное расстояние за пределами видимости камеры для генерации
    public float minOffsetX = 2f; // Минимальное смещение для следующего объекта
    public float maxOffsetX = 10f; // Максимальное смещение для следующего объекта

    private float lastGeneratedXPosition; // Позиция последнего сгенерированного объекта по оси X
    private Transform player; // Ссылка на трансформ игрока

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
        lastGeneratedXPosition = player.position.x;
    }

    void Update()
    {
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.aspect * mainCamera.orthographicSize;

        if (cameraRightEdge + generationIntervalX > lastGeneratedXPosition)
        {
            GenerateTrigger(cameraRightEdge);
        }
    }

    void GenerateTrigger(float startGenerationX)
    {
        int prefabIndex = Random.Range(0, triggerPrefabs.Length);
        GameObject prefabToGenerate = triggerPrefabs[prefabIndex];
        float yPosition = triggerYPositions.Length > prefabIndex ? triggerYPositions[prefabIndex] : 0;
        // Генерируем случайное смещение в заданном диапазоне
        float offset = Random.Range(minOffsetX, maxOffsetX);
        float generatedXPosition = lastGeneratedXPosition + generationIntervalX + offset;

        Instantiate(prefabToGenerate, new Vector3(generatedXPosition, yPosition, 0), Quaternion.identity);

        lastGeneratedXPosition = generatedXPosition; // Обновляем последнюю позицию, учитывая новое смещение
    }
}
