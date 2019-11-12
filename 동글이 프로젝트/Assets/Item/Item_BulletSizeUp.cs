using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BulletSizeUp : MonoBehaviour {

    public float size = 0.5f;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().bullet_size += size;
            Destroy(gameObject);
        }
    }
}
