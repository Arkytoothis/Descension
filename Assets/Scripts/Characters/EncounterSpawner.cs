using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class EncounterSpawner : MonoBehaviour
    {
        [SerializeField] List<NpcData> npcs = new List<NpcData>();

        [SerializeField] Collider trigger = null;
        [SerializeField] EncounterData encounterData = null;

        [SerializeField] Transform target = null;
        [SerializeField] float turnSpeed = 2f;
        [SerializeField] int partyLayer = 14;

        private void Awake()
        {
            trigger = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            encounterData = EncounterGenerator.GenerateEncounter();    
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == partyLayer)
            {
                target = other.transform;
                EncounterManager.instance.StartEncounter(encounterData, this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == partyLayer)
            {
                target = null;
                //EncounterManager.instance.EndEncounter();
            }
        }

        void Update()
        {
            if (target != null)
            {
                Vector3 lTargetDir = target.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * turnSpeed);
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Destroy(gameObject);
        }
    }
}