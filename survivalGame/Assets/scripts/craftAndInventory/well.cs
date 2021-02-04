using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class well : MonoBehaviour
{
    [SerializeField] public List<GameObject> waters;
    [SerializeField] private float refillTime;
    [SerializeField] private int listSize;
    [SerializeField] public GameObject water;
    private float fillTime;

    private void Start()
    {
        for (int i = 0; i < listSize; i++)
        {
            waters.Add(water);
        }
    }

    private void Update()
    {
        //if (waters.Count < listSize)
        //{
        //    StartCoroutine(fillWater(fillTime));
        //}
    }

    //IEnumerator fillWater(float fillTime)
    //{
    //    yield return new WaitForSeconds(fillTime);
    //    waters.Add(water);
    //}

    public GameObject PullWater()
    {
        if (waters.Count > 0)
        {
            waters.RemoveAt(waters.Count - 1);
            return water;
        }
        else
        {
            return null;
        }
        
    }
}
