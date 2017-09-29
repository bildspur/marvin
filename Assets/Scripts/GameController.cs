using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;
using Kino;

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

    public int noteNumber;

    private volatile bool startGame = true;

    private GameObject infoText;

    void Start()
    {
        score = 0;

        // find highscore
        highScore = GameObject.Find("HighScore").GetComponent("HighScoreLogic") as HighScoreLogic;
        infoText = GameObject.Find("InfoText");

        UpdateSpawnValues();
        UpdateScore();
        StartCoroutine(SpawnWaves());

        // auto run
        infoText.GetComponent<Text>().text = "KDAMDAN!";
        infoText.GetComponent<Animator>().SetTrigger("RunOutro");
    }

    IEnumerator SpawnWaves()
    {
        // wait till start pressed
        while (!startGame)
        {
            yield return new WaitForSeconds(1);
        }

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
        // wait for start
        if (Input.GetKeyDown("s"))
            StartGame();

        if (MidiMaster.GetKeyDown(noteNumber))
            StartGame();

        // switch is on
        if (MidiMaster.GetKeyUp(1))
            SetGlitch(true);

        // switch is off
        if (MidiMaster.GetKeyDown(1))
            SetGlitch(false);
    }

    void SetGlitch(bool enabled)
    {
        var syphon = GameObject.Find("Syphon Camera");
        var glitch = syphon.GetComponent<AnalogGlitch>();
        glitch.enabled = enabled;
    }

    public void StartGame()
    {
        startGame = true;
        infoText.GetComponent<Animator>().SetTrigger("RunOutro");
    }

    public void ShowGameOver()
    {
        infoText.GetComponent<Text>().text = "Game Over";
        infoText.GetComponent<Animator>().SetTrigger("RunIntro");
    }
}
