using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // we will give reference as player
    public GameObject player;

    // camera will follow player
    // only in XZ directions
    // it should not be effected by other transformations
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // get distance
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // the reason for late update
        // it will run after all of the transformations done
        transform.position = player.transform.position + offset;
        // new camera transformations based on player position + offset
    }
}
