using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryingToParse : MonoBehaviour
{
    public InputField inputField;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            inputField.text = "Holi";
        }
    }
}
