using UnityEngine;

namespace NPCs {

public class CivilianBehavior : NpcBehavior
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Physics.CheckBox(transform.position+new Vector3(0,-0.6f,0),new Vector3(0.55f,1.1f,0.55f)));
    }

}

}