using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class UIController : MonoBehaviour
{
    PlayerScript player;
    Text distanceText;
    Text coinText;

    GameObject results;
    Text finalDistanceText;
    TMP_Text finalCoinsText;
    public void Awake()
    {
        player =  GameObject.Find("Player").GetComponent<PlayerScript>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        finalCoinsText = GameObject.Find("FinalCoinsText").GetComponent<TMP_Text>();    
        coinText = GameObject.Find("CoinText").GetComponent<Text>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();

        results = GameObject.Find("Results");
        results.SetActive(false);


    }

    void Update()
    {
        coinText.text = " Coins : " + player.coinCount; 
        int distance = (int) player.distanceTraveled;
        distanceText.text = distance + " m ";



        if (player.isDead)
        {
            finalCoinsText.text = " : " + player.coinCount;
            finalDistanceText.text = " Distance : " + player.distanceTraveled + " m ";
            results.SetActive(true);
           
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Endless Runner");
    }
}
