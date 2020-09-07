using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelRanking : MonoBehaviour
{
    [SerializeField]
    private Ranking ranking;
    [SerializeField]
    private GameObject prefabColocado;

    private void Start()
    {
        var listaDeColocados = this.ranking.GetListaColocados();
        
        for (int i = 0; i < listaDeColocados.Count; i++)
        {
            if (i >= 5 )
            {
                break;
            }
            var colocado = GameObject.Instantiate(this.prefabColocado, this.transform);
            var colocacao = i + 1;

            //colocado.GetComponent<ItemRanking>().Configurar(colocacao, listaPontuacao[i].GetNome(), listaPontuacao[i].GetZumbisMortos().ToString(), listaPontuacao[i].GetTempoSobrevivenciaString(), listaPontuacao[i].GetPontosString());

            //colocado.GetComponent<ItemRanking>().Configurar(colocacao, listaDeColocados[i].nomeJogador, listaDeColocados[i].zumbisMortosString, listaDeColocados[i].tempoSobrevivenciaString, listaDeColocados[i].pontos.ToString());
            colocado.GetComponent<ItemRanking>().Configurar(colocacao, listaDeColocados[i].nomeJogador, listaDeColocados[i].zumbisMortosString, listaDeColocados[i].tempoSobrevivenciaString, listaDeColocados[i].pontosString);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
