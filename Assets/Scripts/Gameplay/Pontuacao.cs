using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pontuacao : MonoBehaviour
{
    [SerializeField]
    private MeuUnityEventString aoMatarZumbi;
    private int pontos;
    private int zumbisMortos = 0;
    public int bonusZumbisMortosOutraCena = 15;
    private int tempoSobrevivencia;
    private int bonusMinutos;
    private string nomeJogador;
    

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

        this.aoMatarZumbi.Invoke(zumbisMortos.ToString());
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

        this.aoMatarZumbi.Invoke(zumbisMortos.ToString());
    }

    //calcula bonus por tempo sobrevivio
    public void PontuarTempo(int min, int seg)
    {
        tempoSobrevivencia = seg + (min * 60);
        bonusMinutos = min*50;
    }
    
    private void CalculaPontosTotal()
    {
        this.pontos = (zumbisMortos * 10) + (tempoSobrevivencia * 2) + (bonusMinutos);
    }

    public void SetZumbisMortos(int zumbisMortos)
    {
        this.zumbisMortos = zumbisMortos;
    }

    public int GetZumbisMortos()
    {
        return this.zumbisMortos;
    }

    public int GetTempoSobrevivencia()
    {
        return this.tempoSobrevivencia;
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
        }
        return this.nomeJogador;
    }
    
    public int GetPontos()
    {
        CalculaPontosTotal();
        return this.pontos;
    }
    
    public Pontuacao GetInstance()
    {
        return this;
    }

    
}


[System.Serializable]
public class MeuUnityEventString : UnityEvent<string>
{

}

