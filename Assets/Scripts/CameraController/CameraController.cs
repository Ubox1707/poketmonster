using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    [SerializeField] GameObject player;
    [Range(0, 1)]
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 positionOffset;

    
    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        if (player == null) return;
        Vector3 targetPos = player.transform.position + positionOffset;
        targetPos = new Vector3(targetPos.x, targetPos.y, -10);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
