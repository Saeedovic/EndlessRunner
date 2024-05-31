    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Obstcale : MonoBehaviour
    {  

        public ObstacleGenerator obstacleGenerator;
        private int coinCount = 0;


        void Start()
        {

        }

        void Update()
        {
            transform.Translate(Vector2.left * obstacleGenerator.currentSpeed * Time.deltaTime);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {


            if (collision.gameObject.CompareTag("Middle"))
            {

                obstacleGenerator.GenerateObstacleWithGap();
            }

            if (collision.gameObject.CompareTag("Finish"))
            {
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Coin"))
            {
                coinCount++;
                Destroy(collision.gameObject);
                Debug.Log("Coins Collected: " + coinCount);
            }


        }

    }
