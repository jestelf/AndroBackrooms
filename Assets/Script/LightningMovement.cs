using UnityEngine;

public class LightningMovement : MonoBehaviour
{
    public float moveDelay = 2f; // Задержка между перемещениями
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveDelay)
        {
            MoveLightning();
            timer = 0f;
        }
    }

    void MoveLightning()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float x = Random.Range(-screenBounds.x, screenBounds.x);
        float y = Random.Range(-screenBounds.y, screenBounds.y);

        transform.position = new Vector2(x, y);
    }
}
