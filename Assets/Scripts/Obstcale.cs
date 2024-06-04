    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Obstcale : MonoBehaviour
    {  

        public ObstacleGenerator obstacleGenerator;



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

           


        }

    }
