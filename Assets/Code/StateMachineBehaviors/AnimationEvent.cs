using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using EasyEditor;

public class AnimationEvent : StateMachineBehaviour
{
    public List<TimeEvent> Events = new List<TimeEvent>();

    [Serializable]
    public class TimeEvent
    {
        public string Name;
        [HideInInspector]
        public bool hasBeenFired = false;
        [Range(0, 1)]
        public float eventTime;
        public List<ParticleInfo> Particles;

        public void PoolParticles()
        {
            for (int i = 0; i < Particles.Count; ++i)
            {
                Particles[i].PoolParticle();
            }
        }
    }

    [SerializableAttribute]
    public class ParticleInfo
    {
        public bool useRoot = false;
        public HumanBodyBones modelLocation;
        public Vector3 Offset;
        public GameObject particlePrefab;
        public GameObject recyclableParticlePrefab;
        public bool worldSpace;
        public GameObject spawnedParticle;
        public GameObject spawnedRecyclableParticle;
        public bool StopOnStateExit = false;
        public float KillDelay = 0;
        public bool UnparentOnStateExit = false;
        public bool findTheGround = false;
        public Vector3 OverrideRotation;
        public bool usePrefabRotation = false;
        public bool isWeaponTipVFX;
        public bool DestroyTheParticleOnRagdoll = false;
        //public ObjectPool ParticlePool;
        //public ObjectPool RecyclablePool;
        public void PoolParticle()
        {
            //ParticlePool = ObjectPool.GetPool(particlePrefab.GetComponent<PooledObject>(), 5);
            if (recyclableParticlePrefab)
            {
            //    RecyclablePool = ObjectPool.GetPool(recyclableParticlePrefab.GetComponent<PooledObject>(), 5);
            }
        }
    }

    public void OnEnable()
    {
        PoolParticles();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int j = 0; j < Events.Count; j++)
        {
            if (!Events[j].hasBeenFired && stateInfo.normalizedTime >= Events[j].eventTime)
            {
                Events[j].hasBeenFired = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reset();
    }

    public virtual void PoolParticles()
    {

    }

    public virtual void Reset()
    {

    }
}
