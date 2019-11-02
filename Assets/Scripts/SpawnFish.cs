using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class SpawnFish : MonoBehaviour {

    public Transform fishHolder;//存放生成的预制体
    public Transform[] spawnPositions;//存放生成的位置
    public GameObject[] fishPrefabs;//存放预制体
    int indexPos;//  位置索引
    int indexFish;//预制体索引
    int maxNum;//生成最大的数量
    int maxSpeed;// 最快速度
    int num;// 一次生成的数量
    int speed;//速度
    int moveType;//判断鱼的行走路线
    int angOffset;//转弯的倾斜角,仅直走生效
    int angSpeed;//转弯的角速度，仅转弯生效
    float waitTime = 0.6f;
    public HotFix HotFix;
	// Use this for initialization
    [LuaCallCSharp]
    void Start()
    {
        InvokeRepeating("Spawn",0,0.6f);
    }
    [LuaCallCSharp]
    void Spawn()
    {
        indexPos = Random.Range(0, spawnPositions.Length);
        indexFish = Random.Range(0, fishPrefabs.Length);
        maxNum = fishPrefabs[indexFish].GetComponent<FishAttr>().maxNum;
        maxSpeed = fishPrefabs[indexFish].GetComponent<FishAttr>().maxSpeed;
        num = Random.Range((maxNum / 2 + 1), maxNum);
        speed = Random.Range(maxSpeed / 2, maxSpeed);
        int moveType = Random.Range(0, 2);//0代表直走，1代表转弯走
        if (0 == moveType)
        {
            //  直走, 生成直走的鱼群
            angOffset = Random.Range(-22, 22);
            StartCoroutine(SpawnStraightFish(indexPos, indexFish, num, speed, angOffset));
        }
        else
        {
            //转弯，生成转弯的鱼群
            if (Random.Range(0, 2) == 0)
            {
                angSpeed=Random.Range(-15, -9);
            }
            else
            {
                angSpeed=Random.Range(9, 15);
            }
            StartCoroutine(SpawnTurnFish(indexPos, indexFish, num, speed, angSpeed));
        }
    }
    [LuaCallCSharp]
    IEnumerator SpawnStraightFish(int indexPos,int indexFish,int num,int speed,int angOffset)//生成直线行走的鱼群
    {
        for (int i=0; i <num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[indexFish]);
            fish.transform.SetParent(fishHolder, false);//false 表示生成的物体 transform 属性不和父物体一致；
            fish.transform.localPosition = spawnPositions[indexPos].transform.localPosition;
            fish.transform.localRotation = spawnPositions[indexPos].transform.localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += (i+1);// 为什么加1不可以！！！！

            //Debug.Log(fish.GetComponent<SpriteRenderer>().sortingOrder );
            fish.transform.Rotate(0,0,angOffset);
            fish.AddComponent<Ef_fishMove>().speed = speed;
            yield return new WaitForSeconds(waitTime);
        }
    }
    [LuaCallCSharp]
    IEnumerator SpawnTurnFish(int indexPos, int indexFish, int num, int speed, int angSpeed)//生成转弯的鱼群
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[indexFish]);
            fish.transform.SetParent(fishHolder, false);//false 表示生成的物体 transform 属性不和父物体一致；
            fish.transform.localPosition = spawnPositions[indexPos].transform.localPosition;
            fish.transform.localRotation = spawnPositions[indexPos].transform.localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += (i + 1);// 为什么加1不可以！！！
            fish.AddComponent<Ef_fishMove>().speed = speed;
            fish.AddComponent<Ef_fishRotate>().speed = angSpeed;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
