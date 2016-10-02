using UnityEngine;
using System.Collections;

public class VirusMoveController : MonoBehaviour {

    private Transform player;
    public float speed = 4.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Snowman").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }
}

