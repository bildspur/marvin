using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    // Use this for initialization
    GameController gameController;

    PlayerLogic player;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerLogic") as PlayerLogic;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && player.isAlive)
        {
            gameController.AddScore(1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
