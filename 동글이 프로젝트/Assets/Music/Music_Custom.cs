using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Music_Custom : MonoBehaviour
{
    //  BGM
    FileInfo file;
    public int[] beats;

    int progress = 0;
    public int count = 0;

    public int sync = -60;
    public int bpm = 15;

    //  지형 색
    public float color_min = 0.0f;
    public float color_max = 0.3f;

    //  컴포넌트들
    List<Enemy> enemies = new List<Enemy>();
    List<Terrain_Common> terrains = new List<Terrain_Common>();
    List<Elavator_Loop> Elev_loops = new List<Elavator_Loop>();

    void Start()
    {
        //  요소들 로드
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
            enemies.Add(i.GetComponent<Enemy>());

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("terrain"))
            terrains.Add(i.GetComponent<Terrain_Common>());

        foreach (Elavator_Loop i in GameObject.FindObjectsOfType<Elavator_Loop>())
            Elev_loops.Add(i.GetComponent<Elavator_Loop>());

        //  음악 파일 로드
        file = new FileInfo("Assets/Music/MusicTaebo" + ".txt");

        if (file == null)
        {
            Debug.LogError("txt not found");
            Application.Quit();
        }

        StreamReader sr = file.OpenText();

        string data = sr.ReadToEnd();
        string[] values = new string[500];
        values = data.Split('\n');

        beats = new int[values.Length - 1];  //  비트 저장

        for (int i = 0; i < values.Length; i++)
            beats[i] = System.Convert.ToInt32(System.Convert.ToDouble(values[i]) * 900.0f);
    }

    void Update()
    {
        for (int i = 0; i < bpm; i++)
        {
            //  음악 종료시 반복
            if (count >= beats.Length)
            {
                count = 0;
                progress = 0;
            }

            //  박자
            if (progress + sync == beats[count])
            {
                count++;
                Drop_the_Beat();
            }

            progress++;
        }
    }

    void Drop_the_Beat()
    {
        //  네몬스 비트
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].On_beat = true;

        //  지형 비트     
        Color col = new Color(Random.Range(color_min, color_max), Random.Range(color_min, color_max), Random.Range(color_min, color_max));

        for (int i = 0; i < terrains.Count; i++)
        {
            terrains[i].col = col;
            terrains[i].Beat();
        }

        //  엘레베이터 비트
        for (int i = 0; i < Elev_loops.Count; i++)
            Elev_loops[i].On_beat = true;

        //  아이템 비트
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Item"))
            i.GetComponent<Item_Common>().Beat();
    }
}
