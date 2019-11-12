using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    //  캐릭터
    public GameObject Player;

    //  시간 정지
    bool Is_Paused = false;

    //  플레이어 선택
    public Transform[] kids;

    public int choice = 0;
    int options = 3;


    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Is_Paused = true;

        iTween.ScaleTo(gameObject, iTween.Hash("x", Player.transform.localScale.x, "y", Player.transform.localScale.y, "time", 0.5f, "ignoretimescale", true));
        iTween.FadeFrom(gameObject, iTween.Hash("alpha", 0, "time", 0.5f, "ignoretimescale", true));
        iTween.MoveTo(gameObject, iTween.Hash("y", transform.position.y + 3 * Player.transform.localScale.y, "time", 0.5f, "ignoretimescale", true));

        Time.timeScale = 0;

        kids = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (Is_Paused)
        {
            //  UI 조작
            if (Input.GetKeyDown(KeyCode.Escape))  //  esc
                Continue_Game();

            else if (Input.GetKeyDown(KeyCode.UpArrow))  //  방향키 위
            {
                choice = (choice - 1) % options;

                if (choice < 0)
                    choice = options - 1;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))  //  방향키 아래
                choice = (choice + 1) % options;

            else if (Input.GetKeyDown(KeyCode.Return))  //  엔터
                Functions(choice);

            //  UI 강조
            Emphasize(choice);
        }

        //  UI 닫는 모션 후 게임 진행
        if (GetComponent<iTween>() == null && !Is_Paused)
        {
            Player.GetComponent<MainCharacter>().Is_UI_On = false;
            Destroy(gameObject);
        }
    }

    //  플레이어 선택
    void Functions(int input)
    {
        switch (input)
        {
            case 0:
                Continue_Game();
                break;

            case 1:
                Retry_Game();
                break;

            case 2:
                Quit_Game();
                break;
        }
    }

    //  계속하기
    void Continue_Game()
    {
        Is_Paused = false;
        Time.timeScale = 1;

        iTween.ScaleTo(gameObject, new Vector3(0.3f, 0.3f, 1), 0.5f);
        iTween.MoveTo(gameObject, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), 0.5f);
        iTween.FadeTo(gameObject, 0, 0.2f);
    }

    //  다시하기
    void Retry_Game()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        Is_Paused = false;
        Time.timeScale = 1;
    }

    //  그만하기
    void Quit_Game()
    {
        Application.Quit();
    }

    //  UI 강조
    void Emphasize(int input)
    {
        for (int i = 1; i < kids.Length; i++)
        {
            if (i == input+1)
                iTween.ScaleTo(kids[i].gameObject, iTween.Hash("scale", new Vector3(1.2f, 1.2f), "time", 0.5f, "ignoretimescale", true));

            else
                iTween.ScaleTo(kids[i].gameObject, iTween.Hash("scale", new Vector3(1.0f, 1.0f), "time", 0.5f, "ignoretimescale", true));
        }
    }
}
