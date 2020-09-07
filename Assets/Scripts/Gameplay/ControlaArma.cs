using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{

    public ReservaExtensivel reservaDeBalas;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    public AudioClip SomTrocaArma;
    private bool jogoParado = false;

    [SerializeField]
    private BotaoTrocaArma scriptBotaoTrocaArma;

    //private bool armaAtiva;
    private int indiceArmaAtiva;
    public GameObject[] Armas;

    [SerializeField]
    private float[] tempoEntreTiros;
    private float tempoEntreTirosArmaAtiva = 1.0f;
    private float tempoProximoTiro = 0.0f;

    private float contadorTempo = 0;

    private void Start()
    {
        //indiceArmaAtiva = (PlayerPrefs.HasKey("ArmaAtiva")) ? PlayerPrefs.GetInt("ArmaAtiva") : 0;
        indiceArmaAtiva = PlayerPrefs.GetInt("ArmaAtiva", 0);

        EquipaArmaAtivaStart();
    }

    private void Update()
    {
        var toquesNaTela = Input.touches;

        contadorTempo += Time.deltaTime;

        if (contadorTempo > tempoEntreTirosArmaAtiva && Time.time > tempoProximoTiro)
        {
            tempoProximoTiro = Time.time + tempoEntreTirosArmaAtiva;
            
            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                if (Input.GetButtonDown("Fire1") && !jogoParado)
                {
                    this.Atirar();
                }
                
            }
        }

        if (SystemInfo.deviceType != DeviceType.Handheld)
        { 
            if (Input.GetButtonDown("Fire2") && !jogoParado)
            {
                TrocaDeArma();
                scriptBotaoTrocaArma.MudaImagemAtiva();
                ControlaAudio.instancia.PlayOneShot(SomTrocaArma);
            }
        }
    }

    private void Awake()
    {
        
    }

    public void TrocaDeArma()
    {
        for (int i = 0; i < Armas.Length; i++)
        {
            if (!Armas[i].activeInHierarchy && i != indiceArmaAtiva)
            {
                indiceArmaAtiva = i;

                Armas[indiceArmaAtiva].SetActive(true);

                tempoEntreTirosArmaAtiva = tempoEntreTiros[indiceArmaAtiva];

                AtualizaArmaAtivaPlayerPrefs(indiceArmaAtiva);
            }
            else
            {
                Armas[i].SetActive(false);
            }
        }
    }

    public int GetIndiceArmaAtiva()
    { 
        return indiceArmaAtiva;
    }

public void EquipaArmaAtivaStart()
    {
        for (int i = 0; i < Armas.Length; i++)
        {
            if (i == indiceArmaAtiva)
            {
                Armas[i].SetActive(true);

                tempoEntreTirosArmaAtiva = tempoEntreTiros[indiceArmaAtiva];
            }
            else
            {
                Armas[i].SetActive(false);
            }
            
        }
    }

    
    public void AtualizaArmaAtivaPlayerPrefs(int novaArmaAtivaSalva)
    {
        PlayerPrefs.SetInt("ArmaAtiva", novaArmaAtivaSalva);   
    }
    
    public float iniciaTempoArmaAtiva()
    {
        return tempoEntreTiros[indiceArmaAtiva];
    }

    private void Atirar()
    {
        contadorTempo += Time.deltaTime;
        
        if (this.reservaDeBalas.TemObjeto())
        {
            var bala = this.reservaDeBalas.PegarObjeto();
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
    }

    IEnumerator IAtirar()
    {
        contadorTempo += Time.deltaTime;
        yield return new WaitForSeconds(tempoEntreTirosArmaAtiva);

        if (this.reservaDeBalas.TemObjeto())
        {

            var bala = this.reservaDeBalas.PegarObjeto();
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
            
        }
    }

    public void AtirarComBotao()
    {
        if (contadorTempo > tempoEntreTirosArmaAtiva && Time.time > tempoProximoTiro)
        {
            tempoProximoTiro = Time.time + tempoEntreTirosArmaAtiva;


            if (this.reservaDeBalas.TemObjeto())
            {

                var bala = this.reservaDeBalas.PegarObjeto();
                bala.transform.position = CanoDaArma.transform.position;
                bala.transform.rotation = CanoDaArma.transform.rotation;
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);

            }
        }
    }

    public void JogoParado()
    {
        this.jogoParado = true;
    }
}
