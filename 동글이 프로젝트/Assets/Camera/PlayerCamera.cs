using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    //  카메라
    Vector3 offset;
    public GameObject Player;
    float size = 7;

    public float sight = 3.0f;
    public float FOV = 8.0f;
    public float speed = 1.0f;

    //  개발용
    float delta = 0.0f;

    // Item UI
    public List<Texture> items;
    public List<int> items_stack;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void OnEnable()
    {
        if (CharacterData.items != null)
        {
            items = CharacterData.items;
            items_stack = CharacterData.items_stack;
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        offset = new Vector3(0, 2.5f, -9);
    }

    void Update ()
    {
        // 카메라 포지션을 플레이어 위주로
        Vector3 pos = Player.transform.position + offset + new Vector3(0, -(1 / Player.transform.localScale.y), 1 - Player.transform.localScale.z);
        //  오프셋 (0, 2.5f, -9) + (0, 캐릭터 크기에 맞춰 살짝 위로, 살짝 멀리)

        if (Player.GetComponent<MainCharacter>().sight == Direction.Left)  //  동글이 시선이 왼쪽
        {
            pos.x -= sight;
            iTween.MoveUpdate(gameObject, pos, speed);
        }

        else  //  시선이 오른쪽
        {
            pos.x += sight;
            iTween.MoveUpdate(gameObject, pos, speed);
        }

        //  FOV 업데이트
        size = iTween.FloatUpdate(size, Player.transform.localScale.x * FOV, 2);
        GetComponent<Camera>().orthographicSize = size;  //  Default = 4


        ////  fps 업데이트
        //delta += (Time.deltaTime - delta) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.LowerRight;
        style.fontSize = h / 20;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 255.0f);

        ////  FPS
        //float fps = 1.0f / delta;

        //string text = string.Format("({0: 0.0} fps )", fps);  //  프레임레이트 카메라에 표시. 디버그용

        //GUI.Label(new Rect(0, 0, w, h * 2 / 10), text, style);

        //  아이템 UI
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].width / 5;
            GUI.Label(new Rect(w - w / 40 - sum, h / 20, items[i].width / 5, items[i].height / 5), items[i]);
        }

        sum = 0;
        for (int i = 0; i < items_stack.Count; i++)
        {
            sum += items[i].width / 5;
            GUI.Label(new Rect(w - w / 40 - sum, h / 20, items[i].width / 5, items[i].height / 5), items_stack[i].ToString(), style);
        }
    }
}
