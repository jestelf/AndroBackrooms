using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class LockCinemachineCameraY : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;
    public float lockedYPosition = 0f; // Позиция Y, которая будет зафиксирована

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (framingTransposer != null)
        {
            // Установить вертикальную мертвую зону на 1, чтобы камера не следовала за игроком по вертикали
            framingTransposer.m_DeadZoneHeight = 1f;
        }
    }

    void LateUpdate()
    {
        if (virtualCamera != null && framingTransposer == null)
        {
            // Если компонент Framing Transposer не используется, принудительно фиксируем положение камеры
            Vector3 position = virtualCamera.transform.position;
            virtualCamera.transform.position = new Vector3(position.x, lockedYPosition, position.z);
        }
    }
}
