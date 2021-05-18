using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public TurnoManager tm;
    public AudioClip shotSound;

    int turn;
    AudioSource _audio;

    void Start()
    {
        _audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        turn = tm.GetTurn();
    }

    void Update()
    {
        if (tm.GetTurn() != turn && tm.podeAgirPolicial)
        {
            turn = tm.GetTurn();
            TryToKillZombie();
        }
    }

    void TryToKillZombie()
    {
        var zombies = GameObject.FindGameObjectsWithTag("zombie");

        foreach (var zombie in zombies)
        {
            if (!zombie.GetComponent<ZombieBehavior>().zumbiMorto)
            {
                if(Mathf.Round(zombie.transform.position.y) == Mathf.Round(transform.position.y))
                {
                    if(zombie.transform.position.x == Mathf.Round(transform.position.x) - 1 ||
                       zombie.transform.position.x == Mathf.Round(transform.position.x) + 1)
                    {
                        zombie.GetComponent<ZombieBehavior>().Morrer();
                        _audio.PlayOneShot(shotSound, 1);
                    }
                }

            }
        }

        var mantis = GameObject.FindGameObjectsWithTag("mantis");
        foreach (var manti in mantis)
        {
            if(transform.parent != null)
            {
                if (Mathf.Round(manti.transform.position.y) == Mathf.Round(transform.position.y))
                {
                    if (manti.transform.position.x == Mathf.Round(transform.position.x) - 1 ||
                       manti.transform.position.x == Mathf.Round(transform.position.x) + 1)
                    {
                        _audio.PlayOneShot(shotSound, 1);
                    }
                }
            }
        }
    }
}
