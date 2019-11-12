using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float dest_x;
    public float dest_y;
    public float dest_time;

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player")
            iTween.MoveAdd(gameObject, iTween.Hash("amount", new Vector3(dest_x, dest_y, 0), "time", dest_time, "oncomplete", "DestroyThis", "oncompleteparams", gameObject));
    }

    void DestroyThis(GameObject it)
    {
        Destroy(it);
    }
}
