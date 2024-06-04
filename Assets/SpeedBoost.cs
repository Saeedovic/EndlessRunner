/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostDuration = 5f;
    public float speedMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ActivateSpeedBoost(collision));
        }
    }

    private IEnumerator ActivateSpeedBoost(Collider2D player)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.currentSpeed *= speedMultiplier;
            yield return new WaitForSeconds(boostDuration);
            playerScript.*//**//*currentSpeed /= speedMultiplier;
        }
        Destroy(gameObject);
    }
}
*/