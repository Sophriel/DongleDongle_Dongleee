using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up = 1, Down, Left, Right }

public class MainCharacter : MonoBehaviour
{
    //  물리
    Rigidbody rb;
    public int Jump = 1500;
    public int JumpCount = 0;
    public int JumpCount_Max = 1;
    bool Is_Jump = false;
    int Jump_time = 0;
    int Jump_Max = 7;

    public int speed = 60;
    public int torque = 60;

    //  카메라
    public Camera MainCamera;
    public Direction sight = Direction.Right;

    //  HP
    public GameObject HP_txt;
    public int HP = 100;
    public bool Is_Onhit = false;
    int Onhit_time = 0;
    public int Onhit_Max = 60;

    public ParticleSystem death;

    //  총알
    public GameObject bullet_prefab;
    public int atk = 1;
    public float bullet_size = 1.0f;

    //  ESC UI
    public bool Is_UI_On = false;
    public GameObject EscapeUI;


    void OnEnable()
    {
        //  씬 이동 후 기본 정보를 가져옴.
        Jump = CharacterData.Jump;
        JumpCount_Max =  CharacterData.JumpCount_Max;
        speed = CharacterData.speed;
        HP = CharacterData.HP;
        Onhit_Max = CharacterData.Onhit_Max;
        atk =  CharacterData.atk;
        bullet_size =  CharacterData.bullet_size;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        MainCamera = FindObjectOfType<Camera>();

        HP_txt = Instantiate(HP_txt);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (!HP_Manager())  //  Is_Alive
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        if (!Is_UI_On)
            Fire_Input();

        UI_Input();
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Enemy")  //  점프 초기화
            JumpCount = 0;
    }

    void OnCollisionStay(Collision obj)
    {
        //if (obj.gameObject.tag == "terrain_top")  //  점프 초기화
        //    JumpCount = 0;

        if (obj.gameObject.tag == "Enemy" && !Is_Onhit)  //  피격 시
        {
            HP -= obj.gameObject.GetComponent<Enemy>().atk;
            Is_Onhit = true;
        }
    }

    void OnCollisionExit(Collision obj)
    {
        if (obj.gameObject.tag == "terrain_top" && !Is_Jump)  //  공중
            JumpCount++;
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "terrain_top")  //  점프 초기화
            JumpCount = 0;
    }

    //  HP 출력 및 관리, 피격
    bool HP_Manager()
    {
        //  HP에 따른 캐릭터 변화
        if (HP < 1)  //  사망
        {
            Instantiate<ParticleSystem>(death, new Vector3(transform.position.x, transform.position.y), new Quaternion());  //  이펙트
            gameObject.SetActive(false);
            HP_txt.SetActive(false);
            return false;
        }

        //   HP에 따른 동글이의 크기
        if (HP < 101)
            transform.localScale = new Vector3(Mathf.Pow(0.5f, 1 - (HP / 100.0f)), Mathf.Pow(0.5f, 1 - (HP / 100.0f)), Mathf.Pow(0.5f, 1 - (HP / 100.0f)));  //  Default = HP

        else
            transform.localScale = new Vector3(HP / 100.0f, HP / 100.0f, HP / 100.0f);

        Jump_Max = 7 + HP / 50;

        //  HP 출력
        Vector3 HP_pos = new Vector3(transform.position.x, transform.position.y + transform.localScale.y - 0.2f, 0);
        float HP_speed = 0.3f * transform.localScale.x;

        HP_txt.GetComponent<TextMesh>().text = HP.ToString();
        HP_txt.GetComponent<TextMesh>().fontSize = (int)(transform.localScale.x * 60);  //  Default = 60 

        if (sight == Direction.Left)  //  시선이 왼쪽
        {
            HP_pos.x += transform.localScale.x;
            iTween.MoveUpdate(HP_txt, HP_pos, HP_speed);
        }

        else  //  시선이 오른쪽
        {
            HP_pos.x -= transform.localScale.x;
            iTween.MoveUpdate(HP_txt, HP_pos, HP_speed);
        }


        //  피격 시
        if (Is_Onhit && Onhit_time < Onhit_Max)  //  피격 상태
            Onhit_time++;

        else  //  피격 상태 종료
        {
            Is_Onhit = false;
            Onhit_time = 0;
        }

        return true;
    }

    //  이동
    void Move()
    {
        //  점프
        if (Is_Jump && Jump_time < Jump_Max)
            Jump_time++;

        else
        {
            Is_Jump = false;
            Jump_time = 0;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("MainCharacter"), LayerMask.NameToLayer("Terrain"), false);
        }

        //  키 입력x
        if (!Input.anyKey)
            return;

        torque = speed;  //  쓸데없는 회전

        //  wasd 이동 조작

        //  키 다운, 점프 한 횟수가 0보다 크고 Max보다 작다. 즉, 한번 이상 점프 한 상태
        if (Input.GetKeyDown("w") && 0 < JumpCount && JumpCount < JumpCount_Max)
        {
            rb.AddForce(0, Jump, 0);
            Jump_Manager();
        }

        //  처음 점프할 경우
        if (Input.GetKey("w") && JumpCount < 1)
        {
            rb.AddForce(0, Jump, 0);
            Jump_Manager();
        }

        else if (Input.GetKey("s"))
        {
            Jump_Manager();
        }

        if (Input.GetKey("a"))
        {
            sight = Direction.Left;
            rb.AddForce(-speed, 0, 0);
            rb.AddTorque(0, 0, torque);
        }

        else if (Input.GetKey("d"))
        {
            sight = Direction.Right;
            rb.AddForce(speed, 0, 0);
            rb.AddTorque(0, 0, -torque);
        }

    }

    //  점프 후 처리
    void Jump_Manager()
    {
        Is_Jump = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("MainCharacter"), LayerMask.NameToLayer("Terrain"));
        JumpCount++;
    }

    //  발사 입력
    void Fire_Input()
    {
        if (!Input.anyKey)
            return;

        //  방향키 공격 조작
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Fire(Direction.Up, Direction.Left);  //  UL

            else if (Input.GetKeyDown(KeyCode.RightArrow))  //  UR
                Fire(Direction.Up, Direction.Right);

            else
                Fire(Direction.Up, Direction.Up);  //  UU
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Fire(Direction.Down, Direction.Left);  //  DL

            else if (Input.GetKeyDown(KeyCode.RightArrow))
                Fire(Direction.Down, Direction.Right);  //  DR

            else
                Fire(Direction.Down, Direction.Down);  //  DD
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Fire(Direction.Left, Direction.Up);  //  LU

            else if (Input.GetKeyDown(KeyCode.DownArrow))
                Fire(Direction.Left, Direction.Down);  //  LD

            else
                Fire(Direction.Left, Direction.Left);  //  LL
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Fire(Direction.Right, Direction.Up);  //  RU

            else if (Input.GetKeyDown(KeyCode.DownArrow))
                Fire(Direction.Right, Direction.Down);  //  RD

            else
                Fire(Direction.Right, Direction.Right);  //  RR
        }
    }

    //  총알 발사
    void Fire(Direction dir, Direction sub_dir)
    {
        HP--;
        float size = (0.2f * transform.localScale.x) * bullet_size;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);  //   y축 보정 필요함
        Instantiate(bullet_prefab, pos, Quaternion.identity).GetComponent<Bullet>().Init(dir, sub_dir, atk, size);  //  prefab 인스턴스화 동시에 Initialize
    }

    //  UI 호출
    void UI_Input()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !Is_UI_On)
        {
            Instantiate(EscapeUI, transform.position, new Quaternion());
            Is_UI_On = true;
        }
    }
}
