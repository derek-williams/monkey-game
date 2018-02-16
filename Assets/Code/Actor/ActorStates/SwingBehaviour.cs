using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;

public class SwingBehaviour : MonoBehaviour
{
    public List<SwingPoint> points;
    public GameObject player;
    public Muscle.Group group;

    public bool engaged;

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;

            if(!engaged && Input.GetKey(KeyCode.Space))
            {
                engaged = true;

                Debug.Log("Player Engaged");

                /*
                 * First player needs to attach the hinge joint to the object
                 * Second pin weight of arm needs to be set to 0
                 * Third movement input while you are on the swing should push the character back and forth
                 */

                var puppet = player.GetComponentInChildren<PuppetMaster>();
                puppet.SetMuscleWeightsRecursive(player.GetComponent<Actor>().shoulder, 1f, 0f, 1f, 1f);
                //puppet.SetMuscleWeights(group, 1f, 0f, 1f, 1f);
                var hand = puppet.muscles[puppet.muscles.Length - 1];
                HingeJoint hinge = points[0].hinge;

                hinge.connectedBody = hand.rigidbody;
                hand.transform.position = hinge.transform.position;

                foreach(Rigidbody rb in player.GetComponentsInChildren<Rigidbody>(true))
                {
                    rb.isKinematic = false;
                }
            }
            if (engaged && Input.GetKeyUp(KeyCode.Space))
            {
                engaged = false;
                player = null;
                Debug.Log("Player Disengaged");
                //need to destroy the hinge join
                foreach (SwingPoint sp in points)
                {
                    if(sp.hinge.connectedBody != null)
                    {
                        sp.hinge.connectedBody = null;
                    }
                }
                //need to reset arm pin weight to 1
                var puppet = player.GetComponentInChildren<PuppetMaster>();
                puppet.SetMuscleWeightsRecursive(player.GetComponent<Actor>().shoulder, 1f, 1f, 1f, 1f);
            }
        }
    }
}