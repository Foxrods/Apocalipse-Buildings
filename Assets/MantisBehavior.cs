using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MantisBehavior : MonoBehaviour
{
    public TurnoManager tm;
    public AudioClip gettingCaught;

    GameObject _player;
    AudioSource _audio;
    int turn;

    void Start()
    {
        _audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player");
        turn = tm.GetTurn();
    }

    void Update()
    {
        if (tm.GetTurn() != turn && tm.podeAgirMantis)
        {
            turn = tm.GetTurn();

            TryToMove();
        }
    }

    void TryToMove()
    {
        if(Mathf.Round(transform.position.y) == Mathf.Round(_player.transform.position.y))
        {
            if(transform.position.x - _player.transform.position.x <= 2 && transform.position.x - _player.transform.position.x > 0)
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }
            else if(transform.position.x - _player.transform.position.x >= -2 && transform.position.x - _player.transform.position.x < 0)
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }
        }
        if(Mathf.Round(transform.position.y + 1) == Mathf.Round(_player.transform.position.y))
        {
            if (transform.position.x - _player.transform.position.x <= 1 && transform.position.x - _player.transform.position.x > 0)
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }
            else if (transform.position.x - _player.transform.position.x >= -1 && transform.position.x - _player.transform.position.x < 0)
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "person")
        {
            _audio.PlayOneShot(gettingCaught, 1);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
