using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlarmBehavior : MonoBehaviour
{
    public int limit;
    public TurnoManager tm;
    public Text _text;
    //public AudioClip alarmSound;

    //AudioSource _audio;
    int turn;
    int realTurn;

    void Start()
    {
        _text.text = limit + " turnos para a implosão do prédio";
        //_audio = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        turn = tm.GetTurn();
    }

    void Update()
    {
        if (tm.GetTurn() != turn || tm.GetRealTurn() != realTurn)
        {
            turn = tm.GetTurn();
            realTurn = tm.GetRealTurn();
            var newlimit = limit - realTurn;
            if (newlimit > limit)
                newlimit = limit;
            try
            {
                _text.text = newlimit == 1? newlimit + " turno para a implosão do prédio":newlimit + " turnos para a implosão do prédio";
            }
            catch
            {

            }
            //_audio.playOneShot(alarmSound, 1);
            if(newlimit == 0)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
