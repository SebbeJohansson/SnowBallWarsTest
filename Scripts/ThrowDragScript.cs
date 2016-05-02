using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{

    public class ThrowDragScript : MonoBehaviour
    {
        public float spring = 50.0f;
        public float damper = 5.0f;
        public float drag = 10.0f;
        public float angularDrag = 5.0f;
        public float distance = 0.2f;
        public bool attachToCenterOfMass = false;

        private SpringJoint m_SpringJoint;


        private void Update()
        {
            // Make sure the user pressed the mouse down
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var mainCamera = FindCamera();

            // We need to actually hit an object
            RaycastHit hit = new RaycastHit();
            if (
                !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                                 mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
                                 Physics.DefaultRaycastLayers))
            {
                return;
            }
            // We need to hit a rigidbody that is not kinematic
            if (!hit.rigidbody || hit.rigidbody.isKinematic)
            {
                return;
            }

            if (!m_SpringJoint)
            {
                var go = new GameObject("Rigidbody dragger");
                Rigidbody body = go.AddComponent<Rigidbody>();
                m_SpringJoint = go.AddComponent<SpringJoint>();
                body.isKinematic = true;
            }

            m_SpringJoint.transform.position = hit.point;
            m_SpringJoint.anchor = Vector3.zero;

            m_SpringJoint.spring = spring;
            m_SpringJoint.damper = damper;
            m_SpringJoint.maxDistance = distance;
            m_SpringJoint.connectedBody = hit.rigidbody;

            StartCoroutine("DragObject", hit.distance);
        }


        private IEnumerator DragObject(float distance)
        {
            var oldDrag = m_SpringJoint.connectedBody.drag;
            var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
            m_SpringJoint.connectedBody.drag = drag;
            m_SpringJoint.connectedBody.angularDrag = angularDrag;
            var mainCamera = FindCamera();
            while (Input.GetMouseButton(0))
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                m_SpringJoint.transform.position = ray.GetPoint(distance) + new Vector3(0, 0.5f, 0);
                yield return null;
            }
            if (m_SpringJoint.connectedBody)
            {
                m_SpringJoint.connectedBody.drag = oldDrag;
                m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
                m_SpringJoint.connectedBody = null;
            }
        }


        private Camera FindCamera()
        {
            if (GetComponent<Camera>())
            {
                return GetComponent<Camera>();
            }

            return Camera.main;
        }
    }
}
