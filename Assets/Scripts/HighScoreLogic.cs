using UnityEngine;

public class HighScoreLogic : MonoBehaviour
{
    [ReadOnly] public int highScore = 0;

    // Use this for initialization
    void Start()
    {
        // if highscore already exists
        var obj = GameObject.Find(this.gameObject.name);
        if (obj != null && obj != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetHighScore(int value)
    {
        highScore = value > highScore ? value : highScore;
        return value > highScore;
    }
}
