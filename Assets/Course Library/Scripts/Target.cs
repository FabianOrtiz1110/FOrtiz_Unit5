using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    private const float minForce = 10.0f;
    private const float maxForce = 15.0f;
    private const float minTorque = -10.0f;
    private const float maxTorque = 10.0f;
    private const float minXPos = -3.0f;
    private const float maxXPos = 3.0f;
    private const float ySpawnPos = -2.0f;

    private Rigidbody targetRB;
    private GameManager gameManager;
    
    public int pointValue;
    public ParticleSystem expParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        RandomForce();
        RandomTorque();
        RandomSpawnPos();
    }

    void RandomForce()
    {
        targetRB.AddForce(Vector3.up * Random.Range(minForce, maxForce),
         ForceMode.Impulse);
    }

    void RandomTorque()
    {
        targetRB.AddTorque(Random.Range(minTorque, maxTorque),
         Random.Range(minTorque, maxTorque), 
         Random.Range(minTorque, maxTorque),ForceMode.Impulse);
    }

    void RandomSpawnPos()
    {
        transform.position = new Vector3(Random.Range(minXPos, maxXPos), 
         ySpawnPos);
    }

    private void OnMouseDown()
    {
        if(gameManager.gameActive)
        {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Instantiate(expParticle, transform.position, expParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Hazard"))
        {
            gameManager.GameOver();
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
