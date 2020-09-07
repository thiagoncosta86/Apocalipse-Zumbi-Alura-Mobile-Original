using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaSkinAtiva : MonoBehaviour
{
    public GameObject[] skinsJogador;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        VerificaSkinAtivaPlayerPrefs();
    }


    private void VerificaSkinAtivaPlayerPrefs()
    {
        /*
        int skinEscolhida = 11; //uma skin padrao
        
        if (PlayerPrefs.HasKey("NumeroSkinAtiva"))
        {
            skinEscolhida = PlayerPrefs.GetInt("NumeroSkinAtiva");           
        }
        /*else
        {
            PlayerPrefs.SetInt("NumeroSkinAtiva", skinEscolhida);
        }*/
        int skinEscolhida = PlayerPrefs.GetInt("NumeroSkinAtiva", 11);

        for (int i = 0; i < skinsJogador.Length; i++)
        {
            if (skinsJogador[i].activeInHierarchy && i != skinEscolhida)
            {
                skinsJogador[i].SetActive(false);
                skinsJogador[skinEscolhida].SetActive(true);
                
                break;
            }
        }
    }
}
