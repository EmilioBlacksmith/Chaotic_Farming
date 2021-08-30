using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int ordersDelivered;
    public int ordersCanceled;
    public float globalTimer;
    public TextMeshProUGUI timerText;

    [Header("Game Over.")]
    public int GameOverCount = 10;
    public bool gameOver = false;
    public bool finished = false;
    public static GameManager instance;
    public MusicScript soundManager;

    [Header("Game Over Title.")]
    public GameObject gameOverTitle;
    public TextMeshProUGUI timeSurvivedTMP;
    public TextMeshProUGUI ordersDeliveredTMP;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(globalTimer >=  0 && !gameOver)
        {
            globalTimer += Time.deltaTime;
            timerText.text = "" + Mathf.Round(globalTimer);
        }
        
        if(ordersCanceled >= GameOverCount && !finished)
        {
            //GameOver
            gameOver = true;
            soundManager.gameOverMusic();
            gameOverTitle.SetActive(true);
            timeSurvivedTMP.text = "" + globalTimer;
            ordersDeliveredTMP.text = "" + ordersDelivered;

            finished = true;
        }
    }
    
    public void SB_OrderDelivered()
    {
        ordersDelivered++;
    }

    public void SB_OrderCanceled()
    {
        ordersCanceled++;
    }
}
