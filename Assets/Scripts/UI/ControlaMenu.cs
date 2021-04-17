using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoJogar;
    public GameObject BotaoSkin;
    public GameObject BotaoOpcoes;
    public GameObject BotaoSair;

    public GameObject BotaoVoltar;
    public Text TextoOpcoes;
    public Slider SliderVolumeMaster;
    public Slider SliderVolumeSfx;
    public Slider SliderVolumeMusic;


    public Text TextoTituloSkin;
    public Text TextoNomeSkin;
    public GameObject botaoAvancarSkin;
    public GameObject botaoRetrocederSkin;
    public GameObject botaoSelecionarSkin;
    public GameObject botaoCancelarSkin;


    public bool opcoesIsShowing;
    public bool skinIsShowing;


    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        BotaoSair.SetActive(true);
        #endif

        SliderVolumeMaster.value = PlayerPrefs.GetFloat("MasterVolume", 80);
        SliderVolumeSfx.value = PlayerPrefs.GetFloat("SfxVolume", 80);
        SliderVolumeMusic.value = PlayerPrefs.GetFloat("MusicVolume", 80);

    }

    private void Awake()
    {
        opcoesIsShowing = false;

        BotaoJogar = GameObject.Find("JogarButton");
        BotaoSkin = GameObject.Find("SkinButton");
        BotaoOpcoes = GameObject.Find("OpcoesButton");
        BotaoSair = GameObject.Find("SairButton");

        BotaoVoltar = GameObject.Find("VoltarButton");
        
        MenuOpcoes();
        MenuSkin();
    }

    public void JogarJogo ()
    {
        StartCoroutine(MudarCena("fase1"));
    }

    IEnumerator MudarCena(string name)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(name);
    }

    public void MenuOpcoes ()
    {
        StartCoroutine(ToggleOpcoesMenu());
    }

    
    IEnumerator ToggleOpcoesMenu()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        BotaoVoltar.SetActive(opcoesIsShowing);
        TextoOpcoes.gameObject.SetActive(opcoesIsShowing);
        SliderVolumeMaster.gameObject.SetActive(opcoesIsShowing);
        SliderVolumeSfx.gameObject.SetActive(opcoesIsShowing);
        SliderVolumeMusic.gameObject.SetActive(opcoesIsShowing);

        opcoesIsShowing = !opcoesIsShowing;

        ToggleMainMenu(opcoesIsShowing);
    }


    public void SelecionaSkin()
    {
        //faz algo para mudar para nova skin
        StartCoroutine(ToggleSkinMenu());
    }

    public void MenuSkin()
    {
        //não muda skin
        StartCoroutine(ToggleSkinMenu());
    }

    IEnumerator ToggleSkinMenu()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        botaoCancelarSkin.SetActive(skinIsShowing);
        TextoTituloSkin.gameObject.SetActive(skinIsShowing);
        TextoNomeSkin.gameObject.SetActive(skinIsShowing);
        botaoAvancarSkin.SetActive(skinIsShowing);
        botaoRetrocederSkin.SetActive(skinIsShowing);
        botaoSelecionarSkin.SetActive(skinIsShowing);
        botaoCancelarSkin.SetActive(skinIsShowing);

        skinIsShowing = !skinIsShowing;

        ToggleMainMenu(skinIsShowing);
    }

    public void ToggleMainMenu(bool showMainMenu)
    {

        BotaoJogar.SetActive(showMainMenu);
        BotaoSkin.SetActive(showMainMenu);
        BotaoOpcoes.SetActive(showMainMenu);
        BotaoSair.SetActive(showMainMenu);
    }

    public void SairDoJogo ()
    {
        StartCoroutine(Sair());
    }

    IEnumerator Sair ()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
