using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRanking : MonoBehaviour
{
    [SerializeField]
    private Text textoColocacao;
    [SerializeField]
    private Text textoNome;
    [SerializeField]
    private Text textoZumbisMortos;
    [SerializeField]
    private Text textoTempoSobrevivencia;
    [SerializeField]
    private Text textoPontuacaoFinal;

    public void Configurar(int colocacao, string nome, string zumbisMortos, string tempoSobrevivencia, string pontuacaoFinal)
    {
        this.textoColocacao.text = "#" + colocacao.ToString();
        this.textoNome.text = nome;
        this.textoZumbisMortos.text = zumbisMortos;
        this.textoTempoSobrevivencia.text = tempoSobrevivencia;
        this.textoPontuacaoFinal.text = pontuacaoFinal;
    }
}
