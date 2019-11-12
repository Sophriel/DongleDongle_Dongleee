using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elavator_Loop : MonoBehaviour
{

    public bool On_beat = false;
    int beat_cur = 0;
    public int beat_max = 10;

    public float start_x;
    public float start_y;

    public float move_x;
    public float move_y;

    public float speed;

    void OnEnable()
    {
        //start_x = transform.position.x;
        //start_y = transform.position.y;

        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(start_x + move_x, start_y + move_y, 0), "speed", speed, "easetype", iTween.EaseType.linear, "oncomplete", "Recall"));
    }

    void FixedUpdate()
    {
        if (On_beat)
        {
            iTween.Resume(gameObject, "MoveTo");
            beat_cur++;

            if (beat_cur > beat_max)
            {
                On_beat = false;
                beat_cur = 0;
                iTween.Pause(gameObject, "MoveTo");
           }
        }
    }

    void Recall()
    {
        transform.position = new Vector3(start_x, start_y, 0);
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(start_x + move_x, start_y + move_y, 0), "speed", speed, "easetype", iTween.EaseType.linear, "oncomplete", "Recall"));
    }
}
