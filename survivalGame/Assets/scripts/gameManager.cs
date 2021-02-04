using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class gameManager : MonoBehaviour,IGameManage
{
    private child child;
    public GameObject scoreScreen;

    void Awake()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        child = FindObjectOfType<child>();
    }

    // Update is called once per frame
    void Update()
    {
        //if child is death show score screen
        if (child.isChildAlive == false)
        {
            //pause game
            PauseGame();
            //show score screen
            scoreScreen.SetActive(true);
            //start game again
            //show highScore
            //show lastscore
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
 
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        scoreScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
