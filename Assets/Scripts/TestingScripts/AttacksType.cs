using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksType : MonoBehaviour
{
    private string[] attackTypes;

    public void OnEnable()
    {
        Zombie.OnAttack += PublishAttacksType;
    }
    public void OnDisable()
    {
        Zombie.OnAttack -= PublishAttacksType;
    }

    private void Awake()
    {
        attackTypes = new string[3]
        {
            "La sopladora", "La hurracarrana", "El Tiro del Bizco"
        };
    }
    private void PublishAttacksType(GameObject go) 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            print("Hay Mamita, "+ go.name +" te hizo " +attackTypes[Random.Range(0, attackTypes.Length)]);
    }
}
