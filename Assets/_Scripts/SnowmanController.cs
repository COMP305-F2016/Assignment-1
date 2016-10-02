using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnowmanController : MonoBehaviour {
    // Movement modifier applied to directional movement.
    public float playerSpeed = 4.0f;
    // What the current speed of our player is
    private float currentSpeed = 0.0f;
    // The last movement that we've made
    private Vector3 lastMovement = new Vector3();
    // The ball we will be shooting
    public Transform ball;
    // How far from the center of the ship should the ball be
    public float ballDistance = .2f;
    // How much time (in seconds) we should wait before
    // we can fire again
    public float timeBetweenFires = .3f;
    // If value is less than or equal 0, we can fire
    private float timeTilNextFire = 0.0f;
    // The buttons that we can use to shoot balls
    public List<KeyCode> shootButton;

    // What sound to play when we're shooting
    public AudioClip shootSound;

    // PUBLIC INSTANCE VARIABLES 
    public GameController gameController;

    [Header("Sounds")]
    public AudioSource thunderSound;
    public AudioSource yaySound;

    // Reference to our AudioSource component
    private AudioSource audioSource;

    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate player to face mouse
        Rotation();
        // Move the player's body
        Movement();
        //Check Bounds
        checkBounds();

        // a foreach loop will go through each item inside of
        // shootButton and do whatever we placed in {}s using the
        // element variable to hold the item
        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                timeTilNextFire = timeBetweenFires;
                ShootBall();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;


    }

    // Will rotate the ship to face the mouse.
    void Rotation()
    {
        // We need to tell where the mouse is relative to the
        // player
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        /*
		* Get the differences from each axis (stands for
		* deltaX and deltaY)
		*/
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // Get the angle between the two objects
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        /*
		* The transform's rotation property uses a Quaternion,
		* so we need to convert the angle in a Vector
		* (The Z axis is for rotation for 2D).
		*/
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        // Assign the ship's rotation
        this.transform.rotation = rot;
    }

    // Will move the player based off of keys pressed
    void Movement()
    {
        // The movement that needs to occur this frame
        Vector3 movement = new Vector3();
        // Check for input
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");
        /*
		* If we pressed multiple buttons, make sure we're only
		* moving the same length.
		*/
        movement.Normalize();
        // Check if we pressed anything
        if (movement.magnitude > 0)
        {
            // If we did, move in that direction
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;

            //mainCamera.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
        }
        else
        {
            // Otherwise, move in the direction we were going
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);

            //mainCamera.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);

            // Slow down over time
            currentSpeed *= .9f;
        }
    }

    private void checkBounds()
    {
        if (this.transform.position.y <= -6.0f || this.transform.position.y >= 6.0f)
        {
            this.reset();
        }

        if (this.transform.position.x <= -6.0f || this.transform.position.x >= 6.0f)
        {
            this.reset();
        }
    }

    /**
	 * this method resets the game object to the original position
	 */
    private void reset()
    {
         this.transform.position = new Vector2(Random.Range(-5.0f, 5.0f), 0f);
    }

    // Creates a ball and gives it an initial position in
    // front of the ship.
    void ShootBall()
    {
        audioSource.PlayOneShot(shootSound, 1.0f);

        // We want to position the ball in relation to
        // our player's location
        Vector3 ballPos = this.transform.position;
        // The angle the ball will move away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        // Calculate the position right in front of the ship's
        // position ballDistance units away
        ballPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -ballDistance);
        ballPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -ballDistance);
        Instantiate(ball, ballPos, this.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("SnowFlake"))
        {
            //this.yaySound.Play();
            this.gameController.IncreaseScore(10);
            Debug.Log("SnowFlake hit"+this.gameController.scoreText);
        }    

    }
}
