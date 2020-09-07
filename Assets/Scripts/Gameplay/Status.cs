using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    //[HideInInspector]
    public int Vida;
    public float Velocidade = 5;

    void Awake ()
    {
        Vida = VidaInicial;

        if (this.tag == "Jogador" && PlayerPrefs.HasKey("VidaJogador"))
        {
            int VidaFaseAnterior = PlayerPrefs.GetInt("VidaJogador");
            PlayerPrefs.DeleteKey("VidaJogador");
            if (VidaFaseAnterior>0)
            {
                Vida = VidaFaseAnterior;
            }
        }
    }
}
