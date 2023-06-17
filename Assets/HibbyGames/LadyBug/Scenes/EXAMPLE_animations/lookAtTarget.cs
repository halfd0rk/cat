using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HibbyGames
{
    public class lookAtTarget : MonoBehaviour
    {
        public Transform target;
        public float strength = 5f;
        public Vector3 offset;

        private Vector3 lookPos;

        public void setStrength(float _strength)
        {
            strength = _strength;
        }

        private void Start()
        {
            lookPos = target.transform.position + offset;
        }

        private void Update()
        {
            lookPos = Vector3.Lerp(lookPos, target.transform.position + offset, Time.deltaTime * strength);
            transform.LookAt(lookPos);
        }

        
    }
}
