using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections.ObjectModel;
using Random = UnityEngine.Random;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private List<Colocado> listaColocados;

    private static string NOME_DO_ARQUIVO = "Ranking.json";
    private string caminhoParaOArquivo;

    
    

    void Awake()
    {
        this.caminhoParaOArquivo = Path.Combine(Application.persistentDataPath, NOME_DO_ARQUIVO);
        if (File.Exists(this.caminhoParaOArquivo))
        {
            var textoJson = File.ReadAllText(this.caminhoParaOArquivo);
            JsonUtility.FromJsonOverwrite(textoJson, this);
        }
        else
        {
            this.listaColocados = new List<Colocado>();
        }

    }

    public int AdicionarPontuacao (Pontuacao novaPontuacao)
    {
        var id = this.listaColocados.Count * Random.Range(1, 100000);
        var novoColocado = new Colocado(id, novaPontuacao);
        
        this.listaColocados.Add(novoColocado);
        this.listaColocados.Sort();
        this.SalvarPontuacao();
        return id;

    }

    private void SalvarPontuacao()
    {
        var textoJson = JsonUtility.ToJson(this);

        File.WriteAllText(this.caminhoParaOArquivo,textoJson);
    }

    public int Quantidade()
    {
        return listaColocados.Count;
    }

    public ReadOnlyCollection<Colocado> GetListaColocados()
    {
        return this.listaColocados.AsReadOnly();
    }

    public void AlterarNome(string novoNome, int id)
    {
        foreach (var item in this.listaColocados)
        {
            if (id == item.idColocado)
            {
                item.nomeJogador = novoNome;
                break;
            }  
        }
        this.SalvarPontuacao();
    }
}

[System.Serializable]
public class Colocado : IComparable
{
    public int idColocado;
    public string nomeJogador;
    public string zumbisMortosString;
    public string tempoSobrevivenciaString;
    public int pontos;
    public string pontosString;
    

    public Colocado(int idColocado, string nomeJogador, string zumbisMortosString, string tempoSobrevivenciaString, int pontos, string pontosString)
    {
        this.idColocado = idColocado;
        this.nomeJogador = nomeJogador;
        this.zumbisMortosString = zumbisMortosString;
        this.tempoSobrevivenciaString = tempoSobrevivenciaString;
        this.pontos = pontos;
        this.pontosString = pontosString;
    }

    public Colocado(int idColocado, Pontuacao novaPontuacao)
    {
        this.idColocado = idColocado;
        this.nomeJogador = novaPontuacao.GetNome();
        this.zumbisMortosString = novaPontuacao.GetZumbisMortos().ToString();
        this.tempoSobrevivenciaString = FormatarTempo(novaPontuacao.GetTempoSobrevivencia());
        this.pontos = novaPontuacao.GetPontos();
        this.pontosString = FormataPontuacaoParaString(novaPontuacao.GetPontos());
    }

    public int CompareTo(object obj)
    {
        var outroObjeto = obj as Colocado;
        return outroObjeto.pontos.CompareTo(this.pontos);
    }

    //garante que a pontuaçao tenha no maximo 5 digitos 
    public string FormataPontuacaoParaString(int pontos)
    {
        if (pontos > 99999)
        {
            pontos = 99999;
        }

        string pontosTexto = pontos.ToString();
        int quantidadeZeros = 5 - pontosTexto.Length;
        string zerosAntes = "0";
        
        while (quantidadeZeros > 0)
        {
            zerosAntes += "0";
            quantidadeZeros -= 1;
        }
        pontosTexto = zerosAntes + pontosTexto;
        return pontosTexto;
    }
    
    //quebra o valor de tempo e coloca no formato 99:59
    public string FormatarTempo(int tempo)
    {
        int min, seg;
        min = tempo / 60;
        seg = tempo % 60;

        string minuto, segundo;
        minuto = FormataTextoTempo(min);
        segundo = FormataTextoTempo(seg);
        return  minuto + ":" + segundo;
    }

    //garante que sempre tem doi digitos em cada parte do relogio na interface
    private string FormataTextoTempo(int tempo)
    {
        string tempoTexto = "";
        if (tempo < 10)
        {
            tempoTexto += "0";
        }
        
            tempoTexto += tempo.ToString();

        return tempoTexto;
    }
}