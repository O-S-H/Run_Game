using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public bool isGameover = false;
    public Text scoreText;
    public GameObject gameoverUI;

    public int socre = 0;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
     public void AddScore (int newScore)
    {
        if (newScore >=-1) 
        {
            
            socre += newScore;
            scoreText.text = "Score : " + socre;   // string으로  형변환을 해주어야함.
        } 


    }
    
    
    public void OnplayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
    
}
