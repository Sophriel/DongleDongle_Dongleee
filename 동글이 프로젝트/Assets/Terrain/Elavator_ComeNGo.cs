using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elavator_ComeNGo : MonoBehaviour {

    public float dest1_x;
    public float dest1_y;
    public float dest1_time;

    public float dest2_x;
    public float dest2_y;
    public float dest2_time;

    void OnEnable()
    {
        iTween.MoveAdd(gameObject, iTween.Hash("amount", new Vector3(dest1_x, dest1_y, 0), "time", dest1_time, "oncomplete", "Call1"));
    }

    void Call1()
    {
        iTween.MoveAdd(gameObject, iTween.Hash("amount", new Vector3(dest2_x, dest2_y, 0), "time", dest2_time, "oncomplete", "Call2"));
    }

    void Call2()
    {
        iTween.MoveAdd(gameObject, iTween.Hash("amount", new Vector3(dest1_x, dest1_y, 0), "time", dest1_time, "oncomplete", "Call1"));
    }
}
