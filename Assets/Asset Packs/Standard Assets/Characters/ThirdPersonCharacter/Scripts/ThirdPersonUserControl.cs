using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        NavMeshAgent m_Agent;
        RaycastHit hit;
        float h, v;

        Vector3 cameraPosition;

        Vector3 hitPosition;
        Vector3 hitPositionNormalized;
        Vector3 myTargetVector;


        private void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }



        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

            // read inputs
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxis("Vertical");

            

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                cameraPosition = m_Cam.forward;
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;

            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
                // .forward = (0,0,1)
                // .right = (1,0,0)
            
            }

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    hitPosition = hit.point - m_Cam.position ;
                    hitPositionNormalized = hitPosition.normalized;
                    //m_Agent.destination = hit.point;
                    myTargetVector = Vector3.Scale(hit.point, new Vector3(1, 0, 1));

                }
            }

#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
            //print(m_Move);
            // if (Input.GetMouseButton(1)) m_Move = Vector3.zero; // use to set movement to 0 when right mouse clicked down
            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_Jump); //, crouch
            m_Jump = false;
        }
    }
}
