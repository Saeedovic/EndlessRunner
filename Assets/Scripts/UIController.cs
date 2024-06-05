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
    public bool ready = false;

    GameObject results;
    GameObject instructions;
    Text finalDistanceText;
    TMP_Text finalCoinsText;

    Slider jumpBoostSlider;
    float jumpBoostRemainingTime = 0f;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        finalCoinsText = GameObject.Find("FinalCoinsText").GetComponent<TMP_Text>();
        coinText = GameObject.Find("CoinText").GetComponent<Text>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        jumpBoostSlider = GameObject.Find("JumpBoostBar").GetComponent<Slider>();

        results = GameObject.Find("Results");
        results.SetActive(false);

        instructions = GameObject.Find("Instructions");
        instructions.SetActive(true);

        jumpBoostSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!ready)
            return;

        coinText.text = " Coins : " + player.coinCount;
        int distance = (int)player.distanceTraveled;
        distanceText.text = distance + " m ";

        if (player.isDead)
        {
            finalCoinsText.text = " : " + player.coinCount;
            finalDistanceText.text = " Distance : " + player.distanceTraveled + " m ";
            Debug.Log("distance traveled = " + player.distanceTraveled);
            results.SetActive(true);
        }

        if (jumpBoostRemainingTime > 0)
        {
            jumpBoostRemainingTime -= Time.deltaTime;
            jumpBoostSlider.value = jumpBoostRemainingTime / player.jumpBoostDuration;
        }
        else
        {
            jumpBoostSlider.gameObject.SetActive(false);
        }
    }

    public void StartJumpBoostUI(float duration)
    {
        jumpBoostRemainingTime = duration;
        jumpBoostSlider.gameObject.SetActive(true);
        jumpBoostSlider.maxValue = 1;
        jumpBoostSlider.value = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Ready()
    {
        ready = true;
        instructions.SetActive(false);

    }

    public void Retry()
    {
        SceneManager.LoadScene("Endless Runner");
    }
}