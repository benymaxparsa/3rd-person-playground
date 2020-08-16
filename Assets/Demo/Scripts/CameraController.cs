using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;

    private CinemachineComposer composer;

    private void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -10, 10);
    }
}
