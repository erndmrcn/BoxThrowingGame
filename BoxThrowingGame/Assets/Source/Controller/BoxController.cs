using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public PoolingController poolingController;
    public static BoxController Manager;
    public Vector2 min_spawn_pos, max_spawn_pos;
    public int spawn_count;

    public void Initialize()
    {
        if (Manager == null)
        {
            Manager = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        poolingController.Initialize();
    }

    public void spawn()
    {
        for (int i = 0; i < spawn_count; i++)
        {
            Box box = PoolingController.PoolingManager.getItem();
            box.setActiveWithPos(new Vector3(Random.Range(min_spawn_pos.x, max_spawn_pos.x), 10, Random.Range(min_spawn_pos.y, max_spawn_pos.y)));
            box.startPosition = box.gameObject.transform.position;
            box.startPosition.y = 0.0f;
        }
    }

    private void ResetPhysic(Box box)
    {
        box.GetComponent<Rigidbody>().velocity = Vector3.zero;
        box.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void dropBox(Box box)
    {
        ResetPhysic(box);
        box.setActiveWithPos(new Vector3(Random.Range(min_spawn_pos.x, max_spawn_pos.x), 10, Random.Range(min_spawn_pos.y, max_spawn_pos.y)));
        box.startPosition = box.gameObject.transform.position;
        box.startPosition.y = 0.0f;
    }

    private void Update()
    {
        for (int i = 0; i < PoolingController.PoolingManager.items.Count; i++)
        {
            Box box = PoolingController.PoolingManager.items[i];
            if (box.gameObject.activeInHierarchy)
            {
                box.DistanceTextUpdate();
            }
        }    
    }
}
