using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField]private int resourceSize;
    public Transform groundScale;
    public SpawnerProperties properties;

    // Start is called before the first frame update
    void Awake()
    {
        if (properties.name != "enemySpawnerProperties")
        {
            StartCoroutine(Spawn(properties.spawnObjs,
            groundScale.localScale.x * 4.3f, groundScale.localScale.z * 4.3f, properties.xSmallRange, properties.zSmallRange, properties.spawnNumber, 0f));
        }
        
    }

    private void Start()
    {
        StartCoroutine(Spawn(properties.spawnObjs,
            groundScale.localScale.x * 4.3f, groundScale.localScale.z * 4.3f, properties.xSmallRange, properties.zSmallRange, properties.spawnNumber, properties.spawnRate));
    }
    // Update is called once per frame

    public IEnumerator Spawn(GameObject[] spawnObjs,float xBingRange, float zBigRange, float xSmallRange, float zSmallRange, int spawnNumber, float waitTime)
    {


        while (gameObject.transform.childCount < spawnNumber)
        {
            int index = Random.Range(0, spawnObjs.Length);
            //Debug.Log(spawnObjs[index].name + "spawning...");
            float xpos = Random.Range(-xBingRange, xBingRange);
            float zpos = Random.Range(-zBigRange, zBigRange);
            if (xpos > xSmallRange || xpos < -xSmallRange && zpos > zSmallRange || zpos < -zSmallRange)
            {
                GameObject spawnedObj = Instantiate(spawnObjs[index], new Vector3(xpos, spawnObjs[index].transform.position.y, zpos), Quaternion.Euler(spawnObjs[index].transform.rotation.eulerAngles.x,Random.Range(0,180), spawnObjs[index].transform.rotation.eulerAngles.z)) as GameObject;
                spawnedObj.transform.parent = gameObject.transform;
                yield return new WaitForSeconds(waitTime);
            }

        }
    }
}
