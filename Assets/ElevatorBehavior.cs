using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehavior : MonoBehaviour
{
    public float limitUp;
    public float limitDown;
    public bool descendo;
    public TurnoManager tm;
    public GameObject player;

    int turn;
    public bool playerDentro;

    public AudioClip clipE;
    public AudioClip clipS;
    AudioSource _audio;

    void Start()
    {
        _audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        turn = tm.GetTurn();
        if (descendo && transform.position.y == limitDown)
        {
            descendo = false;
        }
        else if (!descendo && transform.position.y == limitUp)
        {
            descendo = true;
        }
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (tm.GetTurn() != turn && tm.podeAgirElevador)
        {
            turn = tm.GetTurn();

            Move();
        }
    }

    void Move()
    {
        if (descendo && transform.position.y != limitDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);

            if (playerDentro)
            {
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1);
            }

            if (descendo && transform.position.y == limitDown)
            {
                descendo = false;
            }
        }
        else if (!descendo && transform.position.y != limitUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            if (!descendo && transform.position.y == limitUp)
            {
                descendo = true;
            }

            if (playerDentro)
            {
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
            }
        }
    }

    public void Enter()
    {
        playerDentro = true;
        _audio.PlayOneShot(clipE, 1);
    }

    public void Exit()
    {
        if(playerDentro == true)
            _audio.PlayOneShot(clipS, 1);
        playerDentro = false;
    }

    
}
