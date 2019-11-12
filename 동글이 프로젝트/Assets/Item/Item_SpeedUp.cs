using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SpeedUp : MonoBehaviour {

    public int speed = 5;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().speed += speed;
            Destroy(gameObject);
        }
    }
}
