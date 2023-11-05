using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera.Follow = player.transform;
    }
}
