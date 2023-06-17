using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HibbyGames
{
    public class exampleAnimationsControl : MonoBehaviour
    {
        public Animator anim;
        public void setAnimVelocity(float _value)
        {
            anim.SetFloat("velocity", _value);
        }
    }
}
