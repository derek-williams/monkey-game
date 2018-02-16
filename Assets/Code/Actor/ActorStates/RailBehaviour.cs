using System.Collections;
using System.Collections.Generic;
using EasyEditor;
using UnityEngine;

public class RailBehaviour : MonoBehaviour 
{
    //self
    public BezierSpline spline;

    public float length;
    public float timeStarted;

    public Collider colliderToLocallyDisable;

    public List<GameObject> playersOnMe;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("Entered the the railing {0}", other.name);
        Animator anim = other.GetComponent<Animator>();
        anim.speed =0f;
        anim.applyRootMotion = false;
        //locally check
        colliderToLocallyDisable.enabled = false;

        SplineWalker newSpline = other.gameObject.AddComponent<SplineWalker>();
        newSpline.spline = spline;
        newSpline.duration = length;
        newSpline.lookForward = true;
        newSpline.mode = SplineWalkerMode.Once;
        timeStarted = Time.time;

        if(!playersOnMe.Contains(other.gameObject))
        {
            playersOnMe.Add(other.gameObject);
        }
    }

	// Update is called once per frame
	public void Update () 
    {
        if(!Mathf.Approximately(0f, timeStarted) && timeStarted+length < Time.time)
        {//need to check to see if local player here on photon
            Destroy(playersOnMe[0].GetComponent<SplineWalker>());
            playersOnMe[0].GetComponent<Animator>().speed = 1f;
            playersOnMe[0].GetComponent<Animator>().applyRootMotion = true;
            colliderToLocallyDisable.enabled = true;
            playersOnMe.Remove((playersOnMe[0]));
            timeStarted = 0f;
        }
	}
}
