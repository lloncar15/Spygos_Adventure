using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SpawnController : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;

    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int numberOfBunnies=1;
    public int numberOfTrees=1;
    public int numberOfWasps=1;

    private bool hasDefeatedEnemies = false;

    public GameObject videoPlayer;

    public Transform walkableGroundTransform;

    private int currentNumberOfEnemies = 0;

    public Animator circleExit;

    // Start is called before the first frame update
    void Start()
    {
        prefabList.Add(Prefab1);
        prefabList.Add(Prefab2);
        prefabList.Add(Prefab3);

        int index = GameController.Instance._currentLevel;
        numberOfBunnies = (int)GameController.Instance.enemies[index].x;
        numberOfTrees = (int)GameController.Instance.enemies[index].y;
        numberOfWasps = (int)GameController.Instance.enemies[index].z;

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Coroutine used to spawn enemies
    IEnumerator SpawnEnemies()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop
        while(true)
        {
            // Don't spawn anything new until all the previous
            // wave's enemies are dead
            if(currentNumberOfEnemies <= 0 && hasDefeatedEnemies == false) 
            {
                currentNumberOfEnemies = numberOfBunnies + numberOfTrees + numberOfWasps;
                float randPosX;
                float randPosY;

                for (int j = 0; j < numberOfBunnies; j++) 
                {
                    randPosX = Random.Range(7.0f, 8.0f);
                    randPosY = Random.Range(0.0f, -3.0f);
                    GameObject spawnedObject = Instantiate(prefabList[0], new Vector3 (randPosX, randPosY, 0), this.transform.rotation);
                    spawnedObject.GetComponent<CharacterMovement>().walkableGroundTransform = walkableGroundTransform;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }

                for (int j = 0; j < numberOfTrees; j++) 
                {
                    randPosX = Random.Range(7.0f, 8.0f);
                    randPosY = Random.Range(0.0f, -3.0f);
                    GameObject spawnedObject = Instantiate(prefabList[1], new Vector3 (randPosX, randPosY, 0), this.transform.rotation);
                    spawnedObject.GetComponent<CharacterMovement>().walkableGroundTransform = walkableGroundTransform;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }

                for (int j = 0; j < numberOfWasps; j++) 
                {
                    randPosX = Random.Range(7.0f, 8.0f);
                    randPosY = Random.Range(0.0f, -3.0f);
                    GameObject spawnedObject = Instantiate(prefabList[2], new Vector3 (randPosX, randPosY, 0), this.transform.rotation);
                    spawnedObject.GetComponent<CharacterMovement>().walkableGroundTransform = walkableGroundTransform;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
        // How much time to wait before checking if we need to 
        // spawn another wave
        yield return new WaitForSeconds(timeBeforeWaves);
        }
    }
    
    // Allows classes outside of GameController to say when we killed 
    // an enemy.
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
        if(currentNumberOfEnemies <= 0) {
            hasDefeatedEnemies = true;
            GameController.Instance._currentLevel++;
            if(GameController.Instance._currentLevel == 5)
            {
                // Interstitial video that was part of an in-house joke
                // videoPlayer.GetComponent<VideoPlayerScript>().playVideo();
            }
            else
            {
                StartCoroutine(LoadNewScene());
            }
        }
    }

    IEnumerator LoadNewScene()
    {
        circleExit.SetTrigger("circleExit");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainGameScene");
    }
}