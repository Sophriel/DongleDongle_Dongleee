using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_MoreJump : MonoBehaviour {

    public int count = 1;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().JumpCount_Max += count;
            Destroy(gameObject);
        }
    }
}
