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
        spawn();
    }

    public void spawn()
    {
        for (int i = 0; i < spawn_count; i++)
        {
            poolingController.getItem().setActiveWithPos(new Vector3(Random.Range(min_spawn_pos.x, max_spawn_pos.x), 10, Random.Range(min_spawn_pos.y, max_spawn_pos.y)));
        }
    }

    public void dropBox(Box box)
    {
        box.setActiveWithPos(new Vector3(Random.Range(min_spawn_pos.x, max_spawn_pos.x), 10, Random.Range(min_spawn_pos.y, max_spawn_pos.y)));
    }
}
