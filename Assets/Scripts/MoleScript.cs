using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript
{
    public MoleScript(int moleNum, Transform moleTransform) 
    {
        GameObject moleInstance = GameObject.Instantiate(GameObject.Find("MolePrefab"));
        moleInstance.name = "Mole " + moleNum;
        moleInstance.transform.position = moleTransform.position;
    }
}
