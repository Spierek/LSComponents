//	Billboard.cs
//	original by Neil Carter (NCarter)
//	modified by Hayden Scott-Baron (Dock) - http://starfruitgames.com
//  allows specified orientation axis
//  modified further from https://wiki.unity3d.com/index.php/CameraFacingBillboard

using UnityEngine;
using System;

namespace LSTools
{
    [ExecuteInEditMode]
    public class Billboard : CachedMonoBehaviour
    {
        public enum EBillboardAxis { Up, Down, Left, Right, Forward, Back };

        [SerializeField]
        public bool m_ReverseFace = false;
        [SerializeField]
        public EBillboardAxis m_Axis = EBillboardAxis.Up;

        [NonSerialized]
        private Camera m_Camera;
        [NonSerialized]
        private Transform m_CameraTransform;

        private void Awake()
        {
            // if no camera referenced, grab the main camera
            if (!m_Camera)
            {
                m_Camera = Camera.main;
            }
            m_CameraTransform = m_Camera.transform;
        }

        private void Update()
        {
            // rotates the object relative to the camera
            Vector3 targetPos = CachedTransform.position + m_CameraTransform.rotation * (m_ReverseFace ? Vector3.forward : Vector3.back) ;
            Vector3 targetOrientation = m_CameraTransform.rotation * GetAxis(m_Axis);
            transform.LookAt(targetPos, targetOrientation);
        }

        // return a direction based upon chosen axis
        public static Vector3 GetAxis(EBillboardAxis refAxis)
        {
            switch (refAxis)
            {
                case EBillboardAxis.Down:
                    return Vector3.down;
                case EBillboardAxis.Forward:
                    return Vector3.forward;
                case EBillboardAxis.Back:
                    return Vector3.back;
                case EBillboardAxis.Left:
                    return Vector3.left;
                case EBillboardAxis.Right:
                    return Vector3.right;
            }

            // default is Vector3.up
            return Vector3.up;
        }
    }
}