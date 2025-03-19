using Cinemachine;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    public int currentPriority = 5;
    public int activePriority = 20;

    public void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            vcam.Priority = activePriority;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            vcam.Priority = currentPriority;
        }
    }
}
