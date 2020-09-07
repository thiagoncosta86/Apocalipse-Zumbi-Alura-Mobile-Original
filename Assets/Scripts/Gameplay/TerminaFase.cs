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
            //Time.timeScale = 0;

            SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
            //StartCoroutine(CarregaProximaFase());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        jogadorStatus = GameObject.FindWithTag("Jogador")
                                .GetComponent<Status>();
        pontuacao = GameObject.FindObjectOfType<Pontuacao>();
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    IEnumerator CarregaProximaFase()
    {
        yield return new WaitForSeconds(0.02f);
        //SceneManager.LoadScene("fase2");
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }
    */
}
