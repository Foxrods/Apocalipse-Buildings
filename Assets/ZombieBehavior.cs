using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieBehavior : MonoBehaviour
{
    public float limitLeft;
    public float limitRight;
    public bool andandoDireita;
    public Sprite alive;
    public Sprite dead;
    public TurnoManager tm;
    public bool zumbiMorto = false;

    public AudioClip gettingCaught;
    AudioSource _audio;

    int turn;

    void Start()
    {
        _audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        turn = tm.GetTurn();
        if (!andandoDireita && transform.position.x == limitLeft)
        {
            andandoDireita = false;
        }
        else if (andandoDireita && transform.position.x == limitRight)
        {
            andandoDireita = true;
        }
    }

    void Update()
    {
        if (tm.GetTurn() != turn && tm.podeAgirZumbi && !zumbiMorto)
        {
            turn = tm.GetTurn();

            Move();
        }
        if (!zumbiMorto)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = alive;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        }
        if (andandoDireita)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
        }
    }

    void Move()
    {
        if (!andandoDireita && transform.position.x != limitLeft)
        {
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);

            if (!andandoDireita && transform.position.x == limitLeft)
            {
                andandoDireita = true;
                transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
        }
        else if (andandoDireita && transform.position.x != limitRight)
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);

            if (andandoDireita && transform.position.x == limitRight)
            {
                andandoDireita = false;
                transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "person" && !zumbiMorto)
        {
            _audio.PlayOneShot(gettingCaught, 1);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void Morrer()
    {
        zumbiMorto = true;
    }

    public void VoltarAVida()
    {
        zumbiMorto = false;
    }
}
