using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spawner", menuName = "new spawner")]
public class SpawnerProperties : ScriptableObject
{
    public GameObject[] spawnObjs;
    public float xBigRange;
    public float zBigRange;
    public float xSmallRange;
    public float zSmallRange;
    public int spawnNumber;
    public float spawnRate;
}
