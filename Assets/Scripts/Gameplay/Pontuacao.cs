using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
public class Pontuacao : MonoBehaviour
{
    [SerializeField]
    private MeuUnityEventString aoMatarZumbi;
    //private UnityEvent aoMatarZumbi;
    private int pontos;
    private string pontosString;
    private int zumbisMortos = 0;
    public int bonusZumbisMortosOutraCena = 15;
    //private int chefesMortos; //se der ou precisar
    private int tempoSobrevivencia;
    private string tempoSobrevivenciaString;
    private int bonusMinutos;
    private string nomeJogador;
    private string zumbisMortosString = "";


    private void Start()
    {
        //veificar se a cena não é fase1
        PontuaZumbisDeOutraCena();
    }

    public void PontuarInimigosMortos(bool ehChefe)
    {
        if (ehChefe)
        {
            zumbisMortos += 10;
        }
        else
        {
            zumbisMortos++;
        }
        zumbisMortosString = this.zumbisMortos.ToString();
        this.aoMatarZumbi.Invoke(zumbisMortosString);
    }
    
    public void PontuaZumbisDeOutraCena()
    {
        int zumbisMortosOutraCena = 0;
        if (PlayerPrefs.HasKey("ZumbisMortos"))
        {
            zumbisMortosOutraCena = PlayerPrefs.GetInt("ZumbisMortos") + bonusZumbisMortosOutraCena;
            PlayerPrefs.DeleteKey("ZumbisMortos");
        }

        zumbisMortos = zumbisMortosOutraCena;
        zumbisMortosString = this.zumbisMortos.ToString();
        this.aoMatarZumbi.Invoke(zumbisMortosString);
    }

    /*
    public void PontuarZumbis()
    {
        zumbisMortos++;
    }

    
    public void PontuarChefes()
    {
        //chefesMortos++;
        zumbisMortos += 10;
    }
    */

    public void PontuarTempo(int min, int seg)
    {
        tempoSobrevivencia = seg + (min * 60);
        bonusMinutos = min*50;

        string minuto, segundo;
        minuto = FormataTextoTempo(min);
        segundo = FormataTextoTempo(seg);
        tempoSobrevivenciaString = minuto + ":" + segundo;
    }
    
    //private int CalculaPontosTotal()
    private void CalculaPontosTotal()
    {
        //adicionar 50 pontos de bonus por minuto inteiro sobrevivido no calculo
        //pontos = (zumbisMortos * 10) + (chefesMortos * 100) + (tempoSobrevivencia * 2) + (bonusMinutos*50);
        this.pontos = (zumbisMortos * 10) + (tempoSobrevivencia * 2) + (bonusMinutos);
 
        //return pontos;
    }

    public void SetZumbisMortos(int zumbisMortos)
    {
        this.zumbisMortos = zumbisMortos;
    }

    public int GetZumbisMortos()
    {
        return this.zumbisMortos;
    }

    public void SetTempoSobrevivencia(int tempoSobrevivencia)
    {
        this.tempoSobrevivencia = tempoSobrevivencia;
    }

    public int GetTempoSobrevivencia()
    {
        return this.tempoSobrevivencia;
    }

    public string GetTempoSobrevivenciaString()
    {
        return this.tempoSobrevivenciaString;
    }



    public void SetBonusMinutos(int bonusMinutos)
    {
        this.bonusMinutos = bonusMinutos;
    }

    public int GetBonusMinutos()
    {
        return this.bonusMinutos;
    }

    public void SetNome(string nome)
    {
        nomeJogador = nome;
    }

    public string  GetNome()
    {
        if (this.nomeJogador==null || this.nomeJogador=="")
        {
            this.nomeJogador = "ABC";
            //return "ABC";
        }
        return this.nomeJogador;
    }

    public int GetPontos()
    {
        return this.pontos;
    }

    public string GetPontosString()
    {
        this.pontosString = FormataPontuacaoParaString();
        return this.pontosString;
    }
    public Pontuacao GetInstance()
    {
        return this;
    }

    public string FormataPontuacaoParaString()
    {
        CalculaPontosTotal();
        if (this.pontos > 99999)
        {
            this.pontos = 99999;
        }

        string pontosTexto = this.pontos.ToString();
        int tamanhoDigitosScore = pontosTexto.Length;
        int quantidadeZeros = 5 - tamanhoDigitosScore;
        string zerosAntes = "0";
        if (tamanhoDigitosScore < 5)
        {
            while (quantidadeZeros > 0)
            {
                pontosTexto = zerosAntes + pontosTexto;
                quantidadeZeros -= 1;
            }
        }

        return pontosTexto;
    }

    private static string FormataTextoTempo(int tempo)
    {
        string tempoTexto;
        if (tempo < 10)
        {
            tempoTexto = "0" + tempo.ToString();
        }
        else
        {
            tempoTexto = tempo.ToString();
        }

        return tempoTexto;
    }

}

[System.Serializable]
public class MeuUnityEventString : UnityEvent<string>
{

}