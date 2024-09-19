using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectCleanup : MonoBehaviour
{
    // Время задержки перед началом проверки условий удаления
    public float delayBeforeCleanup = 1f;
    // Флаг, отслеживающий, началась ли уже задержка
    private bool delayStarted = false;

    void Update()
    {
        // Если задержка уже началась, выходим из Update
        if (delayStarted) return;

        // Начинаем задержку перед удалением
        StartCoroutine(DelayedCleanup());
    }

    IEnumerator DelayedCleanup()
    {
        delayStarted = true;
        yield return new WaitForSeconds(delayBeforeCleanup);

        // Получаем все объекты с тегом "Background"
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background").OrderBy(go => go.transform.position.y).ToArray();
        // Получаем все объекты с тегом "Trigger"
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");

        // Проверяем каждый объект "Trigger" на условие удаления
        foreach (var trigger in triggers)
        {
            if (IsAboveBackgroundObjects(trigger, backgrounds))
            {
                Destroy(trigger); // Удаляем объект "Trigger", если он удовлетворяет условию
            }
        }

        // После завершения задержки и выполнения операций разрешаем начать новую задержку
        delayStarted = false;
    }

    bool IsAboveBackgroundObjects(GameObject trigger, GameObject[] backgrounds)
    {
        List<float> backgroundHeightsBelowTrigger = new List<float>();

        foreach (var background in backgrounds)
        {
            if (background.transform.position.y < trigger.transform.position.y)
            {
                backgroundHeightsBelowTrigger.Add(background.transform.position.y);
            }
        }

        // Проверяем, есть ли четыре и более объектов "Background" непрерывно ниже "Trigger"
        int consecutiveCount = 0;
        for (int i = 0; i < backgroundHeightsBelowTrigger.Count - 1; i++)
        {
            if (Mathf.Abs(backgroundHeightsBelowTrigger[i] - backgroundHeightsBelowTrigger[i + 1]) < 0.1f) // Предполагаем небольшой диапазон для "подряд идущих" объектов
            {
                consecutiveCount++;
                if (consecutiveCount >= 3) // Проверяем 3, так как сравнение между соседними элементами в итоге даст на 1 меньше
                {
                    return true;
                }
            }
            else
            {
                consecutiveCount = 0; // Сброс счетчика, если объекты не идут подряд
            }
        }

        return false;
    }
}
