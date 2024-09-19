using UnityEngine;

public class CloneRemoval : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока.
    public float removalDistanceBehind = 5f; // Расстояние позади игрока, после которого объекты будут удаляться.

    void Update()
    {
        // Найдем все объекты-клоны на сцене.
        GameObject[] clones = GameObject.FindObjectsOfType<GameObject>();

        foreach (var clone in clones)
        {
            // Проверяем, содержит ли имя объекта "(Clone)" и не имеет ли тег "Background".
            if (clone.name.Contains("(Clone)") && clone.tag != "Background")
            {
                // Проверка, находится ли объект позади игрока.
                bool isBehindPlayer = clone.transform.position.x < player.position.x;

                // Вычисляем расстояние от игрока до объекта.
                float distanceToPlayer = Vector3.Distance(player.position, clone.transform.position);

                // Если объект находится позади игрока на заданном расстоянии, удаляем его.
                if (isBehindPlayer && distanceToPlayer > removalDistanceBehind)
                {
                    Destroy(clone);
                }
            }
        }
    }
}
