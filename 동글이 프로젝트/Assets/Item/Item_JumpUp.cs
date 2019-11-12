using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_JumpUp : MonoBehaviour {

    public int jump = 100;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().Jump += jump;
            Destroy(gameObject);
        }
    }
}
