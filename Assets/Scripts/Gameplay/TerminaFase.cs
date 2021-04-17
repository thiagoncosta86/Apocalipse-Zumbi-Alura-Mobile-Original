using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TerminaFase : MonoBehaviour
{

    private Status jogadorStatus;
    private Pontuacao pontuacao;
    private int nextSceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jogador")
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("VidaJogador", jogadorStatus.Vida);
            PlayerPrefs.SetInt("ZumbisMortos", pontuacao.GetZumbisMortos()); //talvez evoluir para levar a pontuacao toda 
            
            SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        jogadorStatus = GameObject.FindWithTag("Jogador")
                                .GetComponent<Status>();
        pontuacao = GameObject.FindObjectOfType<Pontuacao>();
    }
}
