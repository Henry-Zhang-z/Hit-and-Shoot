using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData_SO", menuName = "Data/LevelData_SO")]
public class LevelData_SO : ScriptableObject
{
    [Header("��ǰ�ؿ��Ƿ�������")]
    public bool canShoot;
    [Header("��ǰ�ؿ���ʼ���������")]
    public int shootCount;
    //��ǰ�ؿ���ש��
    public List<SingleBrickData> bricks = new List<SingleBrickData>();
   
}
[System.Serializable]
public class SingleBrickData
{

    public GameObject brick;
    public BrickData data;
    public Vector2 pos;

}
