using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stmodoru : MonoBehaviour
{

    private string barcodeData = "";
    private float barcodeTimeout = 0.5f;
    private float lastInputTime;

    void Update()
    {

        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                if (Time.time - lastInputTime < barcodeTimeout)
                {
                    ProcessBarcode(barcodeData);
                }
                barcodeData = "";
            }
            else
            {
                barcodeData += c;
                lastInputTime = Time.time;
            }
        }
    }

    void ProcessBarcode(string data)
    {

        if (data == "6890263421674")
        {

            SceneManager.LoadScene("start");
        }

    }
}