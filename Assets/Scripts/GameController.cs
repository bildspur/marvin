using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWaitMin;
    public float spawnWaitMax;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    private int score;

    private int wave = 1;

    void Start()
    {
        score = 0;
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
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(Random.Range(spawnWaitMin, spawnWaitMax));
                AddScore(1);
            }
            yield return new WaitForSeconds(waveWait);
            AddWave();
        }
    }

    public void AddWave()
    {
        wave ++;
        UpdateScore();
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Wave: " + wave + " Score: " + score;
    }

    void Update()
    {
    }
}
