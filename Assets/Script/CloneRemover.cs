using UnityEngine;

public class CloneRemover : MonoBehaviour
{
    void Update()
    {
        CheckAndRemoveClones();
    }

    private void CheckAndRemoveClones()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int backroomsCount = 0;

        foreach (GameObject obj in allObjects)
        {
            // Подсчет количества объектов "backrooms-yellow(Clone)"
            if (obj.name.Contains("backrooms-yellow(Clone)"))
            {
                backroomsCount++;
            }

            // Проверка условий для удаления
            if (backroomsCount > 4 && obj.name.Contains("(Clone)") && !obj.CompareTag("Background"))
            {
                Destroy(obj);
            }
        }
    }
}
