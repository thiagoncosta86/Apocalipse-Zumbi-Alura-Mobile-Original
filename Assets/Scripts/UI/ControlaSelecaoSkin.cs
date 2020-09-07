using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaSelecaoSkin : MonoBehaviour
{
    [SerializeField]
    public GameObject[] skinsJogador;
    public GameObject Jogador;
    public Text nomeSkin;
    int numeroSkin = 0;
    int numeroSkinAtiva;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.Find("Jogador");
        //skinsJogador = GameObject.FindGameObjectsWithTag("PlayerSkin"); // só pegou o unico ativo

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        VerificaSkinAtiva();
        //TrocaSkin();
    }

    private void VerificaSkinAtiva()
    {
        for (int i = 0; i < skinsJogador.Length; i++)
        {
            //if (skinsJogador[i].active)
            if (skinsJogador[i].activeInHierarchy)
            {
                numeroSkinAtiva = i;
                numeroSkin = i;
                
                nomeSkin.text = PegaNomeSkin(i);

                SalvaSkinEmplayerPrefs();
                break;
            }
            
        }
    }

    //avanca ou retrocede skin ativa na interface
    public void TrocaSkinAtiva(int passo)
    {
        
        skinsJogador[numeroSkin].SetActive(false);

        if (numeroSkin + passo == skinsJogador.Length)
        {
            numeroSkin = 0;
        }
        else if (numeroSkin + passo < 0)
        {
            numeroSkin = skinsJogador.Length - 1;
        }
        else
        {
            numeroSkin = numeroSkin + passo;
        }
        
        skinsJogador[numeroSkin].SetActive(true);

        nomeSkin.text = PegaNomeSkin(numeroSkin);
        //Debug.Log(skinsJogador[numeroSkin].name);
    }

    //confirma a troca da skin
    public void ConfirmaTrocaSkin()
    {
        numeroSkinAtiva = numeroSkin;

        SalvaSkinEmplayerPrefs();
    }
    
    //cancela a troca da skin
    public void CancelaTrocaSkin()
    {
        skinsJogador[numeroSkin].SetActive(false);
        skinsJogador[numeroSkinAtiva].SetActive(true);
        numeroSkin = numeroSkinAtiva;

        SalvaSkinEmplayerPrefs();
    }

    public void SalvaSkinEmplayerPrefs()
    {
        PlayerPrefs.SetInt("NumeroSkinAtiva", numeroSkinAtiva);
    }

    public string PegaNomeSkin(int numeroSkin)
    {
        return skinsJogador[numeroSkin].name.Remove(0, 11);
    }
}
