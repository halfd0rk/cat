using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HibbyGames
{
    //This script is used for making an object follow another object's position and rotation//
    public class followPosition : MonoBehaviour
    {
        //The targetTransform is the transform that this object will follow//
        [Header("Transform to Follow")]
        [SerializeField] private Transform targetTransform;
        [Header("Follows Position and Rotation")]
        //Determines whether this object follows the target's position and rotation//
        [SerializeField] private bool followingPosition = true;
        [SerializeField] private bool followingRotation = true;
        [Header("Follow Speed")]
        //Controls the speed at which this object moves and rotates to match the target//
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float turnSpeed = 3f;
        [Header("Affect Velocity float parameter in Animator to change animation speed")]
        //Used to control the speed of the object's animation based on its velocity//
        [SerializeField] private float animVelocityMultiplier = 1.2f;
        [SerializeField] private float animVelocityMax = 1.5f;

        //Private variables used for animating the object//
        private bool hasAnim = false;
        private Animator myAnim;
        private Vector3 prevPos;
        private float velocity = 0f;
        private Quaternion prevRot;
        private float velocityRot;

        // Start is called before the first frame update
        private void Start()
        {
            //Check if this object has an animator//
            if (TryGetComponent<Animator>(out var _animator))
            {
                //If it does, store a reference to the animator in myAnim//
                myAnim = _animator;

                //Check if the animator has a velocity parameter//
                foreach (AnimatorControllerParameter param in myAnim.parameters)
                {
                    if(param.name == "velocity")
                    {
                        //If it does, check that it is a float parameter and set hasAnim to true//
                        if (param.type == AnimatorControllerParameterType.Float)
                        {
                            hasAnim = true;
                        }
                    }
                }
            }
            //Use the object's initial position and rotation for default prevPos and prevRot//
            prevPos = transform.position;
            prevRot = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            //If targetTransform is not null, update the object's position and rotation to match the target//
            if (targetTransform != null)
            {
                //If followingPosition is true, move the object towards the target's position//
                if (followingPosition)
                {
                    transform.position = Vector3.Lerp(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
                }
                //If followingRotation is true, rotate the object towards the target's rotation//
                if (followingRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, turnSpeed * Time.deltaTime);
                }
            }

            //If this object has an animator with a velocity float parameter, update the animation speed//
            if (hasAnim)
            {
                //Calculate the object's current velocity by comparing its current and previous positions and rotations//
                velocity = Vector3.Distance(prevPos, transform.position) * 50f;
                velocityRot = Quaternion.Angle(transform.rotation, prevRot);
                velocity += velocityRot;

                //Clamp the velocity to the maximum allowed speed and set it to 0 if it is very small//
                if (velocity > animVelocityMax)
                {
                    velocity = animVelocityMax;
                }
                if (velocity < 0.01f)
                {
                    velocity = 0f;
                }

                //Multiply the velocity by the animVelocityMultiplier to control the animation speed.
                velocity *= animVelocityMultiplier;
                //Update the previous position and rotation values for the next frame//
                prevPos = transform.position;
                prevRot = transform.rotation;

                //Set the "velocity" parameter on the animator, if it exists//
                myAnim.SetFloat("velocity", velocity);

            }
        }
    }
}
//1.0.0 - Hibby Games//
