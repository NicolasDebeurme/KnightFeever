using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public GameObject[] prefabsEnnemies;
    public GameObject player;
    public Tilemap[] Rooms;
    
    public GameObject EndGameGem;
    public LevelLoader levelLoader;
    public GameObject PauseMenu;
    [SerializeField] private int MinNumberOfEnnemies = 3;
    [SerializeField] private int MaxNumberOfEnnemies = 15;
    [SerializeField] private int MaxNumberOfWaves = 3;
    [HideInInspector] public int actualnbennemies = 0;
    [HideInInspector]public bool isGameEnded = false;
    int nbennemiestospawn;
    int actualwave = 1;
    
    bool isSpawning = false;

    public static LevelManager Instance;

    GameObject GameMusic;
    private void Awake()
    {
        GameMusic= AudioManager.Instance.PlayMusic("battleThemeA");
        Instance = this;
    }

    private void Update()
    {
        if (playerData.inRoom)
        {
            if (actualnbennemies == 0 && actualwave <= MaxNumberOfWaves && !isSpawning)
            {
                isSpawning = true;
                StartCoroutine(StartSpawning());
                actualwave++;
            }
            //win ?
            if (actualwave > MaxNumberOfWaves && actualnbennemies == 0 && !isSpawning)
            {
                EndGameGem.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = PauseMenu.activeSelf? 0 : 1;
        }

        if(isGameEnded)
        {
            
            Destroy(GameMusic);
            if(Input.anyKeyDown)
            {
                levelLoader.LoadNextLevel();
            }
        }


    }
    private void SpawnEnnemy(int actualwave)
    {
        nbennemiestospawn = Random.Range(MinNumberOfEnnemies, MaxNumberOfEnnemies + 1);
        while (nbennemiestospawn > 0)
        {
            actualnbennemies++;

            int largeur = Random.Range(Rooms[0].cellBounds.xMin + 1, Rooms[0].cellBounds.xMax - 1);
            int hauteur = Random.Range(Rooms[0].cellBounds.yMin + 1, Rooms[0].cellBounds.yMax - 1);
            int typeOfEnnemy = Random.Range(0, prefabsEnnemies.Length);

            Vector3Int pos = new Vector3Int(largeur, hauteur, 0);

            Instantiate(prefabsEnnemies[typeOfEnnemy], Rooms[0].GetCellCenterWorld(pos), Quaternion.identity);

            nbennemiestospawn--;
        }
        isSpawning = false;
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(2);
        SpawnEnnemy(actualwave);
    }

    public void OnClickResume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1;
        levelLoader.LoadNextLevel();
    }

}
