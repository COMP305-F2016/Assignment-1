///-----------------------------------------------------------------
///   Namespace:      COMP305-F2016-FrostyFight
///   Class:          BallController
///   Description:    This controls the snowball behavior 
///   Author:         Angela Liu                    Date: Oct 1st,2016
///   Notes:          Snowball controller
///   Revision History: V1.0
///   Name: Angela          Date: Sep 27,2016        Description:
///-----------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    // How long the ball will live
    public float lifetime = 2.0f;
    // How fast will the laser move
    public float speed = 5.0f;
    // How much damage will this laser do if we hit an enemy
    public int damage = 1;

    // Use this for initialization
    void Start()
    {
        // The game object that contains this component will be
        // destroyed after lifetime seconds have passed
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
