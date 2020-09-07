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
        //indiceArmaAtiva = (PlayerPrefs.HasKey("ArmaAtiva")) ? PlayerPrefs.GetInt("ArmaAtiva") : 0;
        indiceArmaAtiva = PlayerPrefs.GetInt("ArmaAtiva", 0);
        IniciaArmaAtiva();
    }

    private void Awake()
    {
        
    }
    
    /*
    private void VerificaSeDispositivoMovel()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            this.gameObject.SetActive(false);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        
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
                //indiceArmaAtiva = i;
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