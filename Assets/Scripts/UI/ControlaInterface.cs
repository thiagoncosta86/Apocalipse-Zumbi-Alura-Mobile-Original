using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ControlaInterface : MonoBehaviour{

    private ControlaJogador scriptControlaJogador;
    private ControlaArma scriptControlaArma;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public GameObject BotaoAnalogico;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;
    public Text TextoQuantidadeDeZumbisMortosFinal;
    public Text TextoTempoDeSobrevivenciaFinal;
    public Text TextoPontuacaoFinal;
    public Button BotaoTrocaArma;
    public Button BotaoDeTiro;

    //private int tempoPartida;

    [SerializeField]
    private Pontuacao pontuacao;

    [SerializeField]
    private Ranking ranking;

    private int id;

    // Use this for initialization
    void Start () {
        //scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        scriptControlaArma = GameObject.FindWithTag("Jogador")
                                .GetComponent<ControlaArma>();

        BotaoAnalogico = GameObject.FindGameObjectWithTag("BotaoAnalogico");
        //BotaoTrocaArma = GameObject.FindGameObjectWithTag("BotaoTrocaArma");

        //SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        //AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }
    private void Awake()
    {
        PainelDeGameOver.SetActive(false);

        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.VidaInicial;
        AtualizarSliderVidaJogador();

    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos ()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("{0}", quantidadeDeZumbisMortos);
    }

    public void GameOver ()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;
        BotaoAnalogico.SetActive(false);
        BotaoTrocaArma.gameObject.SetActive(false);
        BotaoDeTiro.gameObject.SetActive(false);
        TextoChefeAparece.gameObject.SetActive(false); //para nao atrapalhar ranking


        SliderVidaJogador.gameObject.SetActive(false);
        TextoQuantidadeDeZumbisMortos.gameObject.SetActive(false);
        scriptControlaArma.JogoParado();

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        pontuacao.PontuarTempo(minutos, segundos);

        AtualizarPlacarZumbisMortos();
        AtualizarPlacarTempo();
        AtualizarPlacarPontuacaoFinal();

        this.id = this.ranking.AdicionarPontuacao(pontuacao);
    }

    private void AtualizarPlacarZumbisMortos()
    {
        TextoQuantidadeDeZumbisMortosFinal.text = pontuacao.GetZumbisMortos().ToString();
    }

    void AtualizarPlacarPontuacaoFinal()
    {
        TextoPontuacaoFinal.text = pontuacao.GetPontosString();
    }
    
    void AtualizarPlacarTempo()
    {
        TextoTempoDeSobrevivenciaFinal.text = pontuacao.GetTempoSobrevivenciaString();
    }

    public void AlterarNome(string nome)
    {
        this.ranking.AlterarNome(nome.ToUpper(), this.id);
    }
    
    public void Reiniciar ()
    {
        LimparPrefsDeRanking();
        //pontuacao.LimpparPontuacao();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Sair()
    {
        LimparPrefsDeRanking();
        int sceneIndex = 0; //main menu
        
        //SceneManager.LoadScene("fase2");
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    private static void LimparPrefsDeRanking()
    {
        PlayerPrefs.DeleteKey("VidaJogador");
        PlayerPrefs.DeleteKey("ZumbisMortos");
    }

    public void AparecerTextoChefeCriado ()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
