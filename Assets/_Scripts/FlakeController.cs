///-----------------------------------------------------------------
///   Namespace:      COMP305-F2016-FrostyFight
///   Class:          SnowmanController
///   Description:    This controls the snowflake behavior 
///   Author:         Angela Liu                    Date: September 30,2016
///   Notes:          Snowflake controller
///   Revision History: V1.0
///   Name: Angela          Date: Sep 27,2016        Description:
///-----------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class FlakeController : MonoBehaviour {
    // PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++++
    private int _speed;
    private int _drift;
    private Transform _transform;

    // PUBLIC PROPERTIES
    public int Speed
    {
        get
        {
            return this._speed;
        }
        set
        {
            this._speed = value;
        }
    }

    public int Drift
    {
        get
        {
            return this._drift;
        }
        set
        {
            this._drift = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        this._transform = this.GetComponent<Transform>();
        this._reset();
    }

    // Update is called once per frame
    void Update()
    {
        this._move();
        this._checkBounds();
    }

    /**
	 * this method moves the game object down the screen by _speed px every frame
	 */
    private void _move()
    {
        Vector2 newPosition = this._transform.position;
        newPosition.y -= this.Speed*.005f;
        newPosition.x += this.Drift*.005f;
        this._transform.position = newPosition;
    }

    /**
	 * this method checks to see if the game object meets the top-border of the screen
	 */
    private void _checkBounds()
    {
        if (this._transform.position.y <= -6.0f)
        {
            this._reset();
        }
     }

    /**
	 * this method resets the game object to the original position
	 */
    private void _reset()
    {
        this.Speed = Random.Range(5, 10);        
        this.Drift = Random.Range(-2, 2);
        this._transform.position = new Vector2(Random.Range(-5.0f, 5.0f),5.0f);       
    }
}
