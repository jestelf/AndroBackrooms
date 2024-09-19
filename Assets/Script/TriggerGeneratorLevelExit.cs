using UnityEngine;

[System.Serializable]
public class TriggerPrefab
{
    public GameObject prefab; // Сам префаб
    public float spawnChance = 100f; // Шанс спавна в процентах
    public float respawnTimeMinutes = 0.1f; // Таймер респавна в минутах
    public float minDistancePassed = 10f; // Минимальное расстояние, которое должен пройти игрок для спавна
    public float spawnOffsetX = 5f; // Смещение от игрока на момент спавна
    [HideInInspector] public float lastSpawnTime = -Mathf.Infinity; // Когда последний раз спавнился объект
    [HideInInspector] public float lastSpawnPositionX = -Mathf.Infinity; // Позиция игрока при последнем спавне
}



public class TriggerGeneratorLevelExit : MonoBehaviour
{
    public TriggerPrefab[] triggers; // Массив данных о триггерах
    public Transform player; // Ссылка на трансформ игрока
    public Camera mainCamera; // Главная камера в сцене

    void Update()
    {
        foreach (var trigger in triggers)
        {
            // Преобразовываем минуты в секунды для сравнения
            float respawnTimeSeconds = trigger.respawnTimeMinutes * 60;
            
            if (Time.time - trigger.lastSpawnTime >= respawnTimeSeconds &&
                player.position.x - trigger.lastSpawnPositionX >= trigger.minDistancePassed)
            {
                if (Random.Range(0f, 100f) <= trigger.spawnChance)
                {
                    float spawnPosX = player.position.x + trigger.spawnOffsetX;
                    Instantiate(trigger.prefab, new Vector3(spawnPosX, trigger.prefab.transform.position.y, 0), Quaternion.identity);

                    trigger.lastSpawnTime = Time.time;
                    trigger.lastSpawnPositionX = player.position.x;
                }
            }
        }
    }


}
