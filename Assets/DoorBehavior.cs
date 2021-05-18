using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    public int personsInTheBuilding;
    public string LevelToLoad;
    GameObject hud;
    
    void Start()
    {
        hud = GameObject.FindWithTag("hud");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log(col.transform.childCount);
            if (personsInTheBuilding == col.transform.childCount)
            {
                Destroy(hud);
                Debug.Log("venceu");
                SceneManager.LoadScene(LevelToLoad);
            }
        }
    }
}
