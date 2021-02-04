using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public enum terms
    {
        morning,
        afternoon,
        evening,
        night
    };

    [SerializeField, Range(0, 480)]public float timeOfDay;
    [SerializeField, Range(0, 360)] public float dayLight;
    
    public terms termsOfDay;
    public int survivedDays;
    public Text daysText;
    public Text ShowDayText;


    public GameObject enemySpawner;
    public GameObject materialSpawner;
    public GameObject treeSpawner;
    public GameObject rockSpawner;
    public GameObject bushSpawner;
    public GameObject animalSpawner;


    // Start is called before the first frame update
    void Start()
    {
        survivedDays = 0;
        timeOfDay = 0.00f;
        dayLight = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            /* 60saniye = 1dakika
             * 
             * 240saniye = 4dk (half day)(morning)
             * 300saniye = 1dk (afternoon)
             * 360saniye = 1dk (evening)------sleep time------
             * 480saniye = 2dk (night)
             *            -----+
             *             8dk (allday)
            */
            timeOfDay += Time.deltaTime ;
            dayLight += Time.deltaTime*0.5f ;
            transform.rotation = Quaternion.Euler(dayLight, 0f, 0f);
            
            if(timeOfDay < 240)
            {
                termsOfDay = terms.morning;
            }
            else if (timeOfDay < 300)
            {
                termsOfDay = terms.afternoon;
            }
            else if (timeOfDay < 360)
            {
                termsOfDay = terms.evening;
            }
            else if(timeOfDay < 480)
            {
                termsOfDay = terms.night;
                enemySpawner.SetActive(true);

                materialSpawner.GetComponent<Spawner>().enabled = false;
                animalSpawner.GetComponent<Spawner>().enabled = false;
                bushSpawner.GetComponent<Spawner>().enabled = false;
                treeSpawner.GetComponent<Spawner>().enabled = false;
                rockSpawner.GetComponent<Spawner>().enabled = false;
            }
            else if(timeOfDay > 480)
            {
                enemySpawner.SetActive(false);

                materialSpawner.GetComponent<Spawner>().enabled = true;
                animalSpawner.GetComponent<Spawner>().enabled = true;
                bushSpawner.GetComponent<Spawner>().enabled = true;
                treeSpawner.GetComponent<Spawner>().enabled = true;
                rockSpawner.GetComponent<Spawner>().enabled = true;

                timeOfDay = 0.00f;
                dayLight = 0.00f;
                termsOfDay = terms.morning;
                survivedDays++;
                daysText.text = survivedDays.ToString();
                StartCoroutine(daysTextShow(3f));
            }
            

        }
    }

    private IEnumerator daysTextShow(float waitTime)
    {
        ShowDayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ShowDayText.gameObject.SetActive(false);
    }
}
