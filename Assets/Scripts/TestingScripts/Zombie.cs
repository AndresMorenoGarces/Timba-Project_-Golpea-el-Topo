using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public delegate void _OnAttack(GameObject go);
    public static event _OnAttack OnAttack;
    private void Update()
    {
        if (OnAttack != null)
            OnAttack(gameObject);
    }
}
