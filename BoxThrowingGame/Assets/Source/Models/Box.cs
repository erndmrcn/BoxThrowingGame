using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float weight;
    private Vector3 position;
    private Vector3 startPosition;

    public float getDistance()
    {
        return Vector3.Distance(position, startPosition);
    }
   
    public void kick()
    {

    }

    public void setActiveWithPos(Vector3 pos)
    {
        startPosition = pos;
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BoundingPlane"))
        {
            gameObject.SetActive(false);
            // give me box
            // update score 10 points for each box
            Box box = PoolingController.PoolingManager.getItem();
            BoxController.Manager.dropBox(box);
            GameController.Manager.onScoreUpdate(10);
        }
    }
}
