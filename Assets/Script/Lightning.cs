using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Flashlight flashlight; // Ссылка на скрипт фонарика для подзарядки

    private void OnMouseUpAsButton()
    {
        // Подзарядить фонарик и уничтожить молнию
        flashlight.RechargeFlashlight(20); // Значение зарядки на ваше усмотрение
        Destroy(gameObject);
    }
}
