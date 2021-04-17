using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaSkinAtiva : MonoBehaviour
{
    public GameObject[] skinsJogador;

    private void Awake()
    {
        VerificaSkinAtivaPlayerPrefs();
    }


    private void VerificaSkinAtivaPlayerPrefs()
    {
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
