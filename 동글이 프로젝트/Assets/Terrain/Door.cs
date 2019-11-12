using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
            iTween.MoveTo(gameObject, new Vector3(transform.position.x, transform.position.y + 20.0f), 30.0f);
    }
}
