using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public PlayerManager player;

    public CinemachineCamera cinemachineCam;

    public void Start()
    {
        cinemachineCam.Follow = player.transform;
        cinemachineCam.LookAt = player.transform;
    }
}
