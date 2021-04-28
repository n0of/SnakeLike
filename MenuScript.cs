using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript: MonoBehaviour
{
    public GameObject title;

    private void Start()
    {
        title.GetComponent<Text>().text = $"Current record is {PlayerPrefsHandler.GetRecord()}";    
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
