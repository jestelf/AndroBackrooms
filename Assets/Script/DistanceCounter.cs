using UnityEngine;
using UnityEngine.UI; // Не забудьте подключить пространство имен для работы с UI

public class DistanceCounter : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока
    public Text distanceText; // UI текст для отображения дистанции
    private float startDistance; // Начальная позиция игрока по оси X

    void Start()
    {
        // Сохраняем начальную позицию игрока
        startDistance = player.position.x;
    }

    void Update()
    {
        // Рассчитываем пройденное расстояние
        float distanceTravelled = player.position.x - startDistance;

        // Переводим расстояние в километры (если игра использует масштаб, где 1 единица Unity соответствует 1 метру)
        float distanceInKilometers = distanceTravelled / 1000;

        // Обновляем UI текст с пройденной дистанцией
        // Используем ToString("F2") для форматирования числа до двух знаков после запятой
        distanceText.text = distanceInKilometers.ToString("F2") + " km";
    }
}
