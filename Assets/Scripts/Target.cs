using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public int pointValue;
    float minSpeed = 12;
    float maxSpeed = 16;
    float maxTorque = 10;
    private Rigidbody targetRb;
    float xRange = 4;
    float ySpawnPos = 0;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
            Destroy(gameObject);
    }
    Vector3 RandomForce() => Vector3.up * Random.Range(minSpeed, maxSpeed);
    Vector3 RandomSpawnPosition() => new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    float RandomTorque() => Random.Range(-maxTorque, maxTorque);
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            if (gameObject.CompareTag("bad"))
                gameManager.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("bad"))
            gameManager.GameOver();
    }
}
