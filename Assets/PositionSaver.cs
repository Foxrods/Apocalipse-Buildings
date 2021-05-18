using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    public TurnoManager tm;

    int turn;
    List<Vector3> position = new List<Vector3>();
    List<Vector3> localPosition = new List<Vector3>();
    List<Transform> parent = new List<Transform>();
    List<bool> descendo = new List<bool>();
    List<bool> playerDentro = new List<bool>();
    List<bool> andandoDireita = new List<bool>();
    List<bool> zumbiMorto = new List<bool>();

    void Start()
    {
        turn = tm.GetTurn();
        position.Add(transform.position);
        if (this.gameObject.tag == "person")
        {
            parent.Add(transform.parent);
            localPosition.Add(transform.localPosition);
        }
        if (this.gameObject.tag == "elevator")
        {
            descendo.Add(gameObject.GetComponent<ElevatorBehavior>().descendo);
            playerDentro.Add(gameObject.GetComponent<ElevatorBehavior>().playerDentro);
        }
        if (this.gameObject.tag == "zombie")
        {
            andandoDireita.Add(gameObject.GetComponent<ZombieBehavior>().andandoDireita);
            zumbiMorto.Add(gameObject.GetComponent<ZombieBehavior>().zumbiMorto);
        }
    }

    void Update()
    {
        if (tm.GetTurn() != turn && tm.podeSalvarPosicao)
        {
            turn = tm.GetTurn();

            StartCoroutine(waitAndSave());
        }

        if (Input.GetKeyDown(KeyCode.Z) && parent.Count > 1)
        {
            parent.RemoveAt(parent.Count - 1);
            transform.parent = parent[parent.Count - 1];
        }

        if (Input.GetKeyDown(KeyCode.Z) && position.Count > 1 && localPosition.Count > 1 && this.gameObject.tag == "person")
        {
            position.RemoveAt(position.Count - 1);
            localPosition.RemoveAt(localPosition.Count - 1);
            if(transform.parent == null)
                transform.position = position[position.Count - 1];
            else
                transform.localPosition = localPosition[localPosition.Count - 1];
        }

        if (Input.GetKeyDown(KeyCode.Z) && position.Count > 1 && this.gameObject.tag != "person")
        {
            position.RemoveAt(position.Count - 1);
            transform.position = position[position.Count - 1];
        }

        if (Input.GetKeyDown(KeyCode.Z) && this.gameObject.tag == "Player")
        {
            tm.GetTurnBack();
        }

        if (Input.GetKeyDown(KeyCode.Z) && descendo.Count > 1 && this.gameObject.tag == "elevator")
        {
            descendo.RemoveAt(descendo.Count - 1);
            gameObject.GetComponent<ElevatorBehavior>().descendo = descendo[descendo.Count - 1];
            playerDentro.RemoveAt(playerDentro.Count - 1);
            gameObject.GetComponent<ElevatorBehavior>().playerDentro = playerDentro[playerDentro.Count - 1];
        }

        if (Input.GetKeyDown(KeyCode.Z) && andandoDireita.Count > 1 && this.gameObject.tag == "zombie")
        {
            andandoDireita.RemoveAt(andandoDireita.Count - 1);
            gameObject.GetComponent<ZombieBehavior>().andandoDireita = andandoDireita[andandoDireita.Count - 1];
            zumbiMorto.RemoveAt(zumbiMorto.Count - 1);
            gameObject.GetComponent<ZombieBehavior>().zumbiMorto = zumbiMorto[zumbiMorto.Count - 1];
        }
    }

    IEnumerator waitAndSave()
    {
        if (this.gameObject.tag == "zombie")
        {
            andandoDireita.Add(gameObject.GetComponent<ZombieBehavior>().andandoDireita);
            zumbiMorto.Add(gameObject.GetComponent<ZombieBehavior>().zumbiMorto);
        }
        yield return new WaitForSeconds(0.05f);

        if (this.gameObject.tag == "person")
        {
            parent.Add(transform.parent);
            localPosition.Add(transform.localPosition);
        }
        if (this.gameObject.tag == "elevator")
        {
            descendo.Add(gameObject.GetComponent<ElevatorBehavior>().descendo);
            playerDentro.Add(gameObject.GetComponent<ElevatorBehavior>().playerDentro);
        }
        
        position.Add(transform.position);
    }

}
