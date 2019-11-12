using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : MonoBehaviour {

    public int heal = 20;

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.GetComponent<MainCharacter>().HP += heal;
            Destroy(gameObject);
        }
    }
}
