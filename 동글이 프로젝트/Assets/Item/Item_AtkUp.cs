using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_AtkUp : MonoBehaviour
{
    public int atk = 1;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().atk += atk;
            Destroy(gameObject);
        }
    }
}
