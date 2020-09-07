using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaoDestruir : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

}
