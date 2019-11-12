using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    bool Is_fading = false;

    public UnityEngine.UI.Image img;
    public float fade = 1.0f;

    void Update()
    {
        if (Is_fading)
        {
            fade = iTween.FloatUpdate(fade, 0.0f, 1.0f);
            img.color = new Color(0, 0, 0, fade);
        }

        if (fade < 0.001f)
        {
            Is_fading = false;
            img.color = new Color(0, 0, 0, 0);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
            Is_fading = true;
    }
}
