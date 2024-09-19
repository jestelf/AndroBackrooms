using UnityEngine;

public class AdjacentCloneRemover : MonoBehaviour
{
    void Update()
    {
        RemoveAdjacentClones();
    }

    private void RemoveAdjacentClones()
    {
        // Получаем все объекты на сцене
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Ищем пары соседних объектов для удаления
        for (int i = 0; i < allObjects.Length - 1; i++)
        {
            GameObject currentObject = allObjects[i];
            GameObject nextObject = allObjects[i + 1];

            // Проверяем, соответствуют ли имена объектов условиям
            if (IsCloneToRemove(currentObject.name) && currentObject.name == nextObject.name)
            {
                // Удаляем оба объекта
                DestroyImmediate(currentObject);
                DestroyImmediate(nextObject);

                // Поскольку мы удалили объекты, сдвигаем индекс назад, чтобы не пропустить следующий объект
                i--;
            }
        }
    }

    // Проверка имени объекта на соответствие условиям удаления
    private bool IsCloneToRemove(string objectName)
    {
        return objectName.Contains("dog(Clone)") || objectName.Contains("Smile(Clone)");
    }
}
