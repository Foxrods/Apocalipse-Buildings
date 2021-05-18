using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicPlayer : MonoBehaviour
{
    private static GameMusicPlayer instance = null;
    public static GameMusicPlayer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(this.gameObject);
        }
    }
}