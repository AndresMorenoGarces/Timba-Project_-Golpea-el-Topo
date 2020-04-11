using UnityEngine;
public class MoleScript {
    public MoleScript(int moleNum, Transform moleTransform)
    { // Constructor que contiene las variables del topo
        GameObject moleInstance = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Mole", typeof(GameObject)));
        moleInstance.name = "Mole " + moleNum;
        moleInstance.transform.position = moleTransform.position;
    }
}