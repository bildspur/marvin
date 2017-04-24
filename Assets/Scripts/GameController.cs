using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWaitMin;

    [ReadOnly] public float spawnWaitMinValue;

    public float spawnWaitMinFactor;
    public float spawnWaitMax;
    [ReadOnly] public float spawnWaitMaxValue;
    public float spawnWaitMaxFactor;

    public float enemySizeMin;

    [ReadOnly] public float enemySizeMinValue;

    public float enemySizeMinFactor;

    public float enemySizeMax;

    [ReadOnly] public float enemySizeMaxValue;

    public float enemySizeMaxFactor;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    private int score;

    private int wave = 0;

    HighScoreLogic highScore;

    void Start()
    {
        score = 0;

        // find highscore
        highScore = GameObject.Find("HighScore").GetComponent("HighScoreLogic") as HighScoreLogic;

        UpdateSpawnValues();
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                var enemy = Instantiate(hazard, spawnPosition, spawnRotation);

                // set scale
                var scaleFactor = Random.Range(enemySizeMinValue, enemySizeMaxValue);
                Vector3 localScale = enemy.transform.localScale;
                Vector3 scale = new Vector3(
                    localScale.x * scaleFactor,
                    localScale.y * scaleFactor,
                    localScale.z * scaleFactor);
                enemy.transform.localScale = scale;

                yield return new WaitForSeconds(Random.Range(spawnWaitMinValue, spawnWaitMaxValue));
            }
            yield return new WaitForSeconds(waveWait);
            AddWave();
        }
    }

    void UpdateSpawnValues()
    {
        spawnWaitMinValue = spawnWaitMin - (spawnWaitMinFactor * wave);
        spawnWaitMaxValue = spawnWaitMax - (spawnWaitMaxFactor * wave);

        enemySizeMinValue = enemySizeMin + (enemySizeMinFactor * wave);
        enemySizeMaxValue = enemySizeMax + (enemySizeMaxFactor * wave);
    }

    public void AddWave()
    {
        wave++;
        UpdateSpawnValues();
        UpdateScore();
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Wave: " + wave + " Score: " + score + " \nHigh: " + highScore.highScore;
    }

    void OnDestroy()
    {
        highScore.SetHighScore(score);
    }

    void Update()
    {
    }
}
