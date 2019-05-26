using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _platformPrefabs;
    [SerializeField]
    private GameObject[] _level2Platformprefabs;
    [SerializeField]
    private GameObject[] _level3PlatformPrefabs;
    [SerializeField]
    private GameObject Ground;
    [SerializeField]
    private int _zedOffset;
    [SerializeField]
    private List<GameObject> _inactiveObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _destructibleObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> Level2BossObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> Level3BossObjects = new List<GameObject>();

    private int spawnObject;
    private ScoreManager scoreManager;
    private bool moved = false;
    private BossEnemy boss;
    private int scorelimit;
    private int scorelimit2;
    private int scorelimit3;
    public bool level1;
    public bool level2;
    public bool level3;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>();
        ShuffleArray(_platformPrefabs);
        for (int i = 0; i < _platformPrefabs.Length; i++)
        {
            Instantiate(_platformPrefabs[i], new Vector3(0, 0, i * 15),Quaternion.Euler(0,0,0));
            _zedOffset += 15;
        }
        scorelimit = 100;
        scorelimit2 = 200;
        scorelimit3 = 300;
        level1 = true;
        level2 = false;
        level3 = false;
    }

    void Update()
    {
        if (scoreManager.Score == scorelimit - 50 && moved == false)
        {
            if(level1 == true)
            {
                for (int i = 0; i < _destructibleObjects.Count; i++)
                {
                    _inactiveObjects.Add(Instantiate(_destructibleObjects[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                    _destructibleObjects[i].SetActive(false);
                }
                ShuffleList(_inactiveObjects);
                moved = true;
            }
            else if(level2 == true)
            {
                for (int i = 0; i < Level2BossObjects.Count; i++)
                {
                    _inactiveObjects.Add(Instantiate(Level2BossObjects[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                    Level2BossObjects[i].SetActive(false);
                }
                ShuffleList(_inactiveObjects);
                moved = true;
            }
            else if(level3 == true)
            {
                Instantiate(Ground, new Vector3(0, -60f, _zedOffset), Quaternion.Euler(0, 0, 0));
                for (int i = 0; i < Level3BossObjects.Count; i++)
                {
                    _inactiveObjects.Add(Instantiate(Level3BossObjects[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                    Level3BossObjects[i].SetActive(false);
                }
                ShuffleList(_inactiveObjects);
                moved = true;
            }
        }

        if(scoreManager.Score == scorelimit || boss.Health == 0)
        {
            boss.Health = 100;
            boss.healthText.gameObject.SetActive(false);
            _inactiveObjects.Clear();
            level1 = false;
            level2 = true;
            level3 = false;
            scoreManager.levelUp = true;
            for(int i = 0; i < _level2Platformprefabs.Length; i++)
            {
                _inactiveObjects.Add(Instantiate(_level2Platformprefabs[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                _level2Platformprefabs[i].SetActive(false);
            }
        }

        if (scoreManager.Score == scorelimit2 || boss.Health == 0)
        {
            boss.Health = 100;
            boss.healthText.gameObject.SetActive(false);
            _inactiveObjects.Clear();
            level3 = true;
            level2 = false;
            level1 = false;
            scoreManager.levelUp = true;
            for (int i = 0; i < _level3PlatformPrefabs.Length; i++)
            {
                _inactiveObjects.Add(Instantiate(_level3PlatformPrefabs[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                _level3PlatformPrefabs[i].SetActive(false);
            }
        }

        if (scoreManager.Score == scorelimit3 || boss.Health == 0)
        {
            boss.Health = 100;
            boss.healthText.gameObject.SetActive(false);
            _inactiveObjects.Clear();
            level1 = true;
            level2 = false;
            level3 = false;
            scoreManager.levelUp = true;
            for (int i = 0; i < _platformPrefabs.Length; i++)
            {
                _inactiveObjects.Add(Instantiate(_platformPrefabs[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)));
                _platformPrefabs[i].SetActive(false);
            }
        }
    }

    public void RecyclePlatform()
    {
        if(_inactiveObjects.Count >= 3)
        {
            Debug.Log("Running");
            spawnObject = Random.Range(0, _inactiveObjects.Count);
            //reposition the platform on zed offset
            _inactiveObjects[spawnObject].transform.position = new Vector3(0, 0, _zedOffset);
            _inactiveObjects[spawnObject].SetActive(true);
            _inactiveObjects.RemoveAt(spawnObject);
            _zedOffset += 15;
        }

        if(level3 == true)
        {
            GameObject.FindGameObjectWithTag("Ground").transform.position = new Vector3(0, 0, _zedOffset);
        }
    }

    public void addInactivePlatform(GameObject platform)
    {
        if(platform.activeInHierarchy != true)
        {
            _inactiveObjects.Add(platform);
        }
    }

    public static void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            T tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }

    public static void ShuffleArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(4, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public int ScoreLimit1
    {
        get { return scorelimit; }
        set { scorelimit = value; }
    }

    public int ScoreLimit2
    {
        get { return scorelimit2; }
        set { scorelimit2 = value; }
    }

    public int ScoreLimit3
    {
        get { return scorelimit3; }
        set { scorelimit3 = value; }
    }
}
