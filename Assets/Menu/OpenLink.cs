using UnityEngine;

public class OpenLink : MonoBehaviour
{
    // Это поле появится в инспекторе Unity, позволяя вам ввести URL напрямую в Unity Editor.
    public string url = "http://yourlink.com";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
