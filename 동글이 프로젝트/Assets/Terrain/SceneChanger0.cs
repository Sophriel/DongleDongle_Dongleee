using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger0 : MonoBehaviour
{
    bool on = false;
    public float time = 0;

    MainCharacter player;
    PlayerCamera cam;


    void Update()
    {
        if (on)
            time += Time.deltaTime;  //  FadeOut 연출 후 이동

        if (time > 3.0f)
        {
            //  캐릭터 현재 데이터 저장
            CharacterData.Jump = player.Jump;
            CharacterData.JumpCount_Max = player.JumpCount_Max;
            CharacterData.speed = player.speed;
            CharacterData.HP = player.HP;
            CharacterData.Onhit_Max = player.Onhit_Max;
            CharacterData.atk = player.atk;
            CharacterData.bullet_size = player.bullet_size;

            CharacterData.items = cam.items;
            CharacterData.items_stack = cam.items_stack;

            //  다음 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            player = obj.gameObject.GetComponent<MainCharacter>();
            cam = FindObjectOfType<Camera>().GetComponent<PlayerCamera>();

            on = true;
        }
    }
}
