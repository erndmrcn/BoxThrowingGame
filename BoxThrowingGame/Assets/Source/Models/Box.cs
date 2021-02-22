using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float weight;
    private Vector3 position;
    public Vector3 startPosition;
    public TextMesh[] txt_distances;

    [SerializeField] private Rigidbody rb;
    public float getDistance()
    {
        return Vector3.Distance(transform.position, startPosition);
    }

    public void KickBox(Vector3 direction, float power)
    {
        rb.AddExplosionForce(power, direction, 10);
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
            if (GameController.Manager.CurrentState == GameController.Gamestates.game || GameController.Manager.CurrentState == GameController.Gamestates.resume)
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
    
    public void DistanceTextUpdate()
    {
        float distance = getDistance();
        for (int i = 0; i < txt_distances.Length; i++)
        {
            txt_distances[i].text = distance.ToString("0.0");
        }
    }
}
