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
            //this.pontuacao = new List<Pontuacao>();
            //Debug.Log(this.listaPontuacao.Count);
        }
        else
        {
            this.listaColocados = new List<Colocado>();
        }

    }

    public int AdicionarPontuacao (Pontuacao novaPontuacao)
    {
        var id = this.listaColocados.Count * Random.Range(1, 100000);
        //var novoColocado = new Colocado(novaPontuacao);
        var novoColocado = new Colocado(id, novaPontuacao);
        
        //var novoColocado = new Colocado(id, novaPontuacao.GetNome(), novaPontuacao.GetZumbisMortos().ToString(), novaPontuacao.GetTempoSobrevivenciaString(), novaPontuacao.GetPontosString());
        //var novoColocado = new Colocado(id, novaPontuacao.GetNome(), novaPontuacao.GetZumbisMortos().ToString(), novaPontuacao.GetTempoSobrevivenciaString(), novaPontuacao.GetPontos(), novaPontuacao.GetPontosString());

        this.listaColocados.Add(novoColocado);
        this.listaColocados.Sort();
        this.SalvarPontuacao();
        return id;

    }

    private void SalvarPontuacao()
    {
        var textoJson = JsonUtility.ToJson(this);

        //Debug.Log(textoJson);
        

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

    /*
    public ReadOnlyCollection<Pontuacao> GetListaPontuacao()
    {
        return this.listaColocados.AsReadOnly();
    }
    */
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
        this.tempoSobrevivenciaString = novaPontuacao.GetTempoSobrevivenciaString();
        this.pontos = novaPontuacao.GetPontos();
        this.pontosString = novaPontuacao.GetPontosString();
    }

    public int CompareTo(object obj)
    {
        var outroObjeto = obj as Colocado;
        return outroObjeto.pontos.CompareTo(this.pontos);
    }
}