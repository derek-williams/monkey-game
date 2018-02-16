using UnityEngine;
using System.Collections;

public class SwingPoint : MonoBehaviour
{
    public HingeJoint hinge;
    public Rigidbody rigid;

    public void Awake()
    {
        if(!hinge)
        {
            hinge = GetComponent<HingeJoint>();
        }
        if(!rigid)
        {
            rigid = GetComponent<Rigidbody>();
        }
    }
}
