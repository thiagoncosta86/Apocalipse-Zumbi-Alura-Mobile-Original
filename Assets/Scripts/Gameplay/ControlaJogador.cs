using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

    private Vector3 direcao;
    private GameObject TextoGameOver;
    [SerializeField]
    private AudioClip SomDeDano;
    [SerializeField]
    private AudioClip[] SonsDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;

    public LayerMask MascaraChao;
    public ControlaInterface scriptControlaInterface;
    public Status statusJogador;

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
        animacaoJogador.Movimentar(this.meuMovimentoJogador.Direcao.magnitude);
    }

    private void Awake()
    {
        EscolheQualSomDeDanoPelaSkinAtiva();
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador();
    }

    public void TomarDano (int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if(statusJogador.Vida <= 0)
        {
            Morrer();
        }        
    }

    public void Morrer ()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida (int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSliderVidaJogador();
    }

    public void EscolheQualSomDeDanoPelaSkinAtiva()
    {
        if (PlayerPrefs.HasKey("NumeroSkinAtiva"))
        {
            int numeroSkin = PlayerPrefs.GetInt("NumeroSkinAtiva");
            if (numeroSkin >= 4 && numeroSkin <=7 )
            {
                SomDeDano = SonsDeDano[1];
            }
            else
            {
                SomDeDano = SonsDeDano[0];
            }

        }
    }
}
