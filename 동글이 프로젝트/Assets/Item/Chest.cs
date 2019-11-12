using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    //  아이템
    public List<GameObject> Items;
    int count;

    //  장치
    public GameObject Cover;
    public bool Is_Opened = false;

	void Start ()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        count = Items.Count;
	}

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player" && !Is_Opened)
        {
            Instantiate(Items[Random.Range(0, count)], transform.position, Quaternion.identity);

            iTween.RotateAdd(Cover, new Vector3(-140.0f, 0, 0), 2.0f);
            Is_Opened = true;

            Items.Clear();
        }
    }
}
