///-----------------------------------------------------------------
///   Namespace:      COMP305-F2016-FrostyFight
///   Class:          VirusController
///   Description:    This controls the Virus behavior 
///   Author:         Angela Liu                    Date: September 30,2016
///   Notes:          Virus controller
///   Revision History: V1.0
///   Name: Angela          Date: Sep 27,2016        Description:
///-----------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class VirusController : MonoBehaviour {

    // How many times should I be hit before I die
    public int health = 2;

    // When the enemy dies, we play an explosion
    public Transform explosion;

    // What sound to play when hit
    public AudioClip hitSound;

    // create an AudioSource variable
    private AudioSource audio1;

    private GameController controller;

    // Use this for initialization
    void Start()
    {
        audio1 = GetComponent<AudioSource>();
        controller =
            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        Debug.Log("Hit"+ theCollision.gameObject.name);
        // this line looks for "ball" in the names of
        // anything collided.
        if (theCollision.gameObject.name.Contains("Ball"))
        {
            BallController ball =
                theCollision.gameObject.GetComponent
                ("BallController") as BallController;
            health -= ball.damage;
            Destroy(theCollision.gameObject);

            // Plays a sound from this object's AudioSource
            audio1.PlayOneShot(hitSound, 1.0f);
        }

        if (theCollision.gameObject.name.Contains("Snowman"))
        {
            controller.DecreaseLives(1);
        }

        if (health <= 0)
        {
            // Check if explosion was set
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.
                    transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }

            controller.KilledEnemy();
            controller.IncreaseScore(10);


            Destroy(this.gameObject);
        }
    }
}

