using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject fruit;
    public GameObject gameInfo;
    public SettingsScriptableObject settings;

    private int _currentHealth = 0;
    private int _score = 0;
    private float _iframes = 0;
    private GameObject _playerInstance;
    private readonly List<GameObject> _enemies = new List<GameObject>();
    private readonly List<GameObject> _fruits = new List<GameObject>();

    void Start()
    {
        _currentHealth = settings.Health;
        SetGameInfo();
        var screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var playerSize = player.transform.GetComponent<BoxCollider2D>().size.x / 2;

        var playerStartPosition = new Vector3(screenSize.x - playerSize, -screenSize.y + playerSize, 0);
        _playerInstance = Instantiate(player, playerStartPosition, Quaternion.identity, transform);
        for (var i = 0; i < settings.EnemyCount; i++)
            _enemies.Add(Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity, transform));
        for (var i = 0; i < settings.FruitCount; i++)
            AddNewFruit();
    }

    void Update()
    {
        _iframes = Mathf.Max(0f, _iframes - Time.deltaTime);
        if (_iframes == 0)
        {
            foreach (var enemy in _enemies)
            {
                if (IsIntersectsWithPlayer(enemy))
                {
                    _currentHealth--;
                    _iframes = settings.IFrames;
                    continue;
                }
            }
        }
        if (_currentHealth == 0)
        {
            var prevRecord = PlayerPrefsHandler.GetRecord();
            if (prevRecord < _score)
                PlayerPrefsHandler.SetRecord(_score);
            SceneManager.LoadScene("MenuScene");
            return;
        }
        var collectedFruits = new List<GameObject>();
        foreach (var fruit in _fruits)
        {
            if (IsIntersectsWithPlayer(fruit))
            {
                _score++;
                collectedFruits.Add(fruit);
            }
        }
        foreach (var fruit in collectedFruits)
        {
            _fruits.Remove(fruit);
            Destroy(fruit);
            AddNewFruit();
        }
        SetGameInfo();
    }

    private bool IsIntersectsWithPlayer(GameObject o)
    {
        return _playerInstance.GetComponent<BoxCollider2D>().bounds.Intersects(o.GetComponent<BoxCollider2D>().bounds);
    }

    private void AddNewFruit()
    {
        var screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var fruitSize = fruit.transform.GetComponent<BoxCollider2D>().size.x / 2;
        var x = Random.Range(-screenSize.x + fruitSize, screenSize.x - fruitSize);
        var y = Random.Range(-screenSize.y + fruitSize, screenSize.y - fruitSize);
        _fruits.Add(Instantiate(fruit, new Vector3(x, y, 0), Quaternion.identity, transform));
    }

    private void SetGameInfo()
    {
        gameInfo.GetComponent<Text>().text = $"Health: {_currentHealth} Score: {_score}";
    }
}
