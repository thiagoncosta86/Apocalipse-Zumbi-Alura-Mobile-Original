using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicaInstancia : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var outrasInstancias = GameObject.FindGameObjectsWithTag(this.tag);
        foreach (var instancia in outrasInstancias)
        {
            if (instancia != this.gameObject)
            {
                GameObject.Destroy(instancia.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
