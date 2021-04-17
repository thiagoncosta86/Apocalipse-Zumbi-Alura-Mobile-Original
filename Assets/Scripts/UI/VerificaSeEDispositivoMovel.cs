using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaSeEDispositivoMovel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VerificaSeDispositivoMovel();
    }


    private void VerificaSeDispositivoMovel()
    {

        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            this.gameObject.SetActive(false);
            return;
        }

    }
}


