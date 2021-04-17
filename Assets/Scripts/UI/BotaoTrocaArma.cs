using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BotaoTrocaArma : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] imagemArmas;

    [SerializeField]
    private ControlaArma scriptControlaArma;
    [SerializeField]
    private int indiceArmaAtiva;

    public AudioClip SomTrocaArma;



    // Start is called before the first frame update
    void Start()
    {
        indiceArmaAtiva = PlayerPrefs.GetInt("ArmaAtiva", 0);
        IniciaArmaAtiva();
    }
    
    public void TrocaDeArma()
    {
        MudaImagemAtiva();

        ControlaAudio.instancia.PlayOneShot(SomTrocaArma);
    }

    public void MudaImagemAtiva()
    {
        for (int i = 0; i < imagemArmas.Length; i++)
        {
            if (!imagemArmas[i].gameObject.activeInHierarchy)
            {
                imagemArmas[i].gameObject.SetActive(true);
                
                indiceArmaAtiva = scriptControlaArma.GetIndiceArmaAtiva();
            }
            else
            {
                imagemArmas[i].gameObject.SetActive(false);
            }

        }
        
    }

    public void IniciaArmaAtiva()
    {
        for (int i = 0; i < imagemArmas.Length ; i++)
        {
            imagemArmas[i].gameObject.SetActive(false);
        }
        
        imagemArmas[indiceArmaAtiva].gameObject.SetActive(true);

    }
}