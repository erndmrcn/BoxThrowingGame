using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // camera will follow player
    // only in XZ directions
    // it should not be effected by other transformations
    private Vector3 offset;

    void Start()
    {
        // get distance
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // the reason for late update
        // it will run after all of the transformations done
        transform.position = player.transform.position + offset;
        // new camera transformations based on player position + offset
    }
}
