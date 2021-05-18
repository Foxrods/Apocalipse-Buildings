using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoManager : MonoBehaviour
{
    int turn = 0;
    int realTurn = 0;

    public bool podeAgirElevador;
    public bool podeAgirZumbi;
    public bool podeAgirMantis;
    public bool podeAgirPolicial;
    public bool podeSalvarPosicao;

    public void NextTurn()
    {
        turn++;
        realTurn++;
        Debug.Log(realTurn);
        StartCoroutine(ExecutaTurnos());
    }

    public void GetTurnBack()
    {
        realTurn = realTurn > 0 ? realTurn-1 : 0;
    }

    public int GetTurn()
    {
        return turn;
    }

    public int GetRealTurn()
    {
        return realTurn;
    }

    IEnumerator ExecutaTurnos()
    {
        podeAgirElevador = true;
        yield return new WaitForSeconds(0.03f);
        podeAgirZumbi = true;
        yield return new WaitForSeconds(0.03f);
        podeAgirMantis = true;
        yield return new WaitForSeconds(0.05f);
        podeAgirPolicial = true;
        yield return new WaitForSeconds(0.03f);
        podeSalvarPosicao = true;
        yield return new WaitForSeconds(0.03f);
        podeAgirElevador = false;
        podeAgirZumbi = false;
        podeAgirMantis = false;
        podeAgirPolicial = false;
        podeSalvarPosicao = false;
    }

    

    

}
