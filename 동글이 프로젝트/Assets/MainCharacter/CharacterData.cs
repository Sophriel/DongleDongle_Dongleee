using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  해당 코드는 플레이어가 씬을 이동하면서 가져가야하는 필수 요소들을 저장하기위해 만들어짐.
//  아래의 수치들은 씬이 시작될 때 불러오는 Default값.

public class CharacterData : MonoBehaviour {

    //  물리
    static public int Jump = 1500;
    static public int JumpCount_Max = 1;

    static public int speed = 60;

    //  HP
    static public int HP = 100;
    static public int Onhit_Max = 60;

    //  총알
    static public int atk = 1;
    static public float bullet_size = 1.0f;


    static public List<Texture> items;
    static public List<int> items_stack;

}
