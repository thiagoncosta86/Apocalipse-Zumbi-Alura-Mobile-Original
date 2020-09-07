using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuavel : MonoBehaviour
{
    [SerializeField]
    private bool ehChefe;
    private Pontuacao pontuacao;

    public void Pontuar()
    {
        this.pontuacao.PontuarInimigosMortos(ehChefe);
    }

    public void SetPontuacao(Pontuacao pontuacao)
    {
        this.pontuacao = pontuacao;
    }


}
