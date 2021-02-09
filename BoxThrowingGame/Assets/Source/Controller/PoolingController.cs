using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class PoolingController : MonoBehaviour
{
    public List<Box> items;
    public static PoolingController PoolingManager;
    public void Initialize()
    {
        if (PoolingManager == null)
        {
            PoolingManager = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Box obj in items)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public Box activate()
    {
        Box newBox = getItem();
        // make it active
        newBox.gameObject.SetActive(true);
        
        return newBox;
    }

    public Box getItem()
    {
        // check if item is active or deactive
        // for pooling choose items that are deactive in scene
        // gameObject.activeInHierarchy

        return items.Find(x=>x.gameObject.activeInHierarchy==false);
    }
}
