using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Common : MonoBehaviour {

    //GameObject player;
    public Color col = new Color();

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Beat()
    {
        //if (Vector3.Distance(transform.position, player.transform.position) < 40)
            iTween.ColorTo(gameObject, iTween.Hash("color", col, "time", 0.15f, "includechildren", true, "ignoretimescale", true));
    }
}
