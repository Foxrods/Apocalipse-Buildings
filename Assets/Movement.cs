using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public TurnoManager tm;
    public ElevatorBehavior elevator;
    public AudioClip gettingCaught;

    public bool podeEntrarElevador = false;
    bool podeAndarDireita = true;
    bool podeAndarEsquerda = true;
    AudioSource _audio;

    void Start()
    {
        _audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        MoveOrWaitTurn();
        EnterElevator();
    }

    void MoveOrWaitTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tm.NextTurn();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.D) && podeAndarDireita)
            {
                CheckForZombie(transform.position.x + 1);
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                transform.localScale = new Vector3(0.7f, 0.7f, 1f);
                try
                {
                    elevator.Exit();
                }
                catch
                {

                }
                tm.NextTurn();
            }
            else if (Input.GetKeyDown(KeyCode.A) && podeAndarEsquerda)
            {
                CheckForZombie(transform.position.x - 1);
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
                try
                {
                    elevator.Exit();
                }
                catch
                {

                }
                tm.NextTurn();
            }
        }

    }

    void EnterElevator()
    {
        if (Input.GetKeyDown(KeyCode.E) && podeEntrarElevador)
        {
            Debug.Log("Entrei no elevador");
            elevator.Enter();
            //tm.NextTurn();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="elevator")
        {
            Debug.Log("Posso entrar no elevador");
            podeEntrarElevador = true;
            elevator = col.gameObject.GetComponent<ElevatorBehavior>();
        }
        if (col.gameObject.tag == "person")
        {
            Debug.Log("Pessoa encontrada");
            col.transform.parent = transform;
            col.transform.position = new Vector2(transform.position.x + Random.Range(-0.35f, 0.35f), transform.position.y);
        }
        if (col.gameObject.tag == "zombie" || col.gameObject.tag == "mantis")
        {
            _audio.PlayOneShot(gettingCaught, 1);
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        if (col.gameObject.tag == "barrier")
        {
            Debug.Log("Barreira encontrada");
            if (col.transform.position.x > transform.position.x)
            {
                podeAndarDireita = false;
            }
            else if(col.transform.position.x < transform.position.x)
            {
                podeAndarEsquerda = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "elevator")
        {
            elevator.Exit();
            podeEntrarElevador = false;
            
        }
        if (col.gameObject.tag == "barrier")
        {
            if (col.transform.position.x > transform.position.x)
            {
                podeAndarDireita = true;
            }
            else if (col.transform.position.x < transform.position.x)
            {
                podeAndarEsquerda = true;
            }
        }
    }

    void CheckForZombie(float xPos)
    {
        var zombies = GameObject.FindGameObjectsWithTag("zombie");

        foreach (var zombie in zombies)
        {
            if (!zombie.GetComponent<ZombieBehavior>().zumbiMorto)
            {
                if (Mathf.Round(zombie.transform.position.y) == Mathf.Round(transform.position.y))
                {
                    if (zombie.transform.position.x == Mathf.Round(xPos))
                    {
                        _audio.PlayOneShot(gettingCaught, 1);
                        Scene scene = SceneManager.GetActiveScene();
                        SceneManager.LoadScene(scene.name);
                    }
                }

            }
        }
    }
}
