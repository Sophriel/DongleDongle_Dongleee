using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Common : MonoBehaviour {

    //  아이템 박자
    public bool On_beat = false;

    //  UI
    public PlayerCamera cam;


    void OnEnable()
    {
        iTween.MoveAdd(gameObject, new Vector3(0, 1.2f, 0), 3.0f);  //  상자 개봉시 위로 튀어나오는 모션

        cam = FindObjectOfType<Camera>().GetComponent<PlayerCamera>();
    }

    void OnDisable()
    {
        //  카메라에 아이콘 띄우기
        if (GetComponent<GUITexture>() != null)
        {
            if (cam.items.Contains(GetComponent<GUITexture>().texture))  //  이미 한번 획득한 아이템
            {
                int i = cam.items.IndexOf(GetComponent<GUITexture>().texture);  //  아이템 획득 수 ++
                cam.items_stack[i]++;
            }

            else
            {
                cam.items.Add(GetComponent<GUITexture>().texture);  //  처음 먹은 아이템은 리스트에 추가
                cam.items_stack.Add(1);
            }
        }
    }

    public void Beat()
    {
        iTween.RotateTo(gameObject, iTween.Hash("y", transform.rotation.eulerAngles.y + 90, "time", 0.5f));
    }
}
