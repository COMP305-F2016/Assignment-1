using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    //PRIVATE INSTANCE VARIABLES +++++++
    // The values are to be printing
    private int score;
    private int waveNumber;
    private int lives;

    // Our enemy to spawn
    public Transform enemy;

    //PUBLIC INSTANCE VARIABLES
    public int flakeNumber = 3;
    public GameObject snowflake;

    [Header("Wave Properties")]
    // We want to delay our code at certain times
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;
    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    [Header("User Interface")]
    // The actual GUI text objects
    public Text scoreText;
    public Text waveText;
    public Text livesText;
 
    [Header("Game Objects")]
    public GameObject snowman;

    //PUBLIC PROPERTIES +++++++
    public int LivesValue {
        get {
            return this.lives;
        }
        set {
            this.lives = value;
            if (this.lives <= 0)
            {
                //this._endGame();
            }
            else {
                this.livesText.text = "Lives: " + this.LivesValue;

            }

        }
    }

    public int ScoreValue
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            this.scoreText.text = "Score: " + this.ScoreValue;  
        }
    }

    public int WaveValue
    {
        get
        {
            return this.waveNumber;
        }
        set
        {
            this.waveNumber = value;
            this.waveText.text = "Wave: " + this.WaveValue;
        }
    }

    // Use this for initialization
    void Start()
    {
        this.lives = 5;
        this.score = 0;
        this.waveNumber = 0;
        StartCoroutine(SpawnEnemies());

        //instantiate snowflake
        for (int flakeNumber = 0; flakeNumber < this.flakeNumber; flakeNumber++) {
            Instantiate(this.snowflake);
        }
    }

    // Coroutine used to spawn enemies
    IEnumerator SpawnEnemies()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop
        while (true)
        {
            // Don't spawn anything new until all of the previous wave's enemies are dead
            if (currentNumberOfEnemies <= 10)
            {
                waveNumber++;
                waveText.text = "Wave: " + waveNumber;

                //Spawn 10 enemies in a random position
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // We want the enemies to be off screen
                    float randDistance = Random.Range(10, 25);

                    // Enemies can come from any direction
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;

                    // Using the distance and direction we set the position
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;

                    // Spawn the enemy and increment the number of enemies spawned
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // How much time to wait before checking if we need to spawn another wave
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Allows classes outside of GameController to say when we
    // killed an enemy.
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Score: " + score;
        Debug.Log("Score: " + score);
    }

    public void DecreaseLives(int decrease)
    {
        lives -= decrease;
        livesText.text = "Lives: " + lives;
    }


}

