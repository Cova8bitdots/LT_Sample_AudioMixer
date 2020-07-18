using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CovaTech.LT.AudioMixerSample
{
    public class TempInputSystem : MonoBehaviour
    {
        [SerializeField]
        private float m_moveSpeed = 1.0f;

        [SerializeField]
        private float m_rotSpped = 90.0f;

        // Update is called once per frame
        void Update()
        {
            float dt =Time.deltaTime;
            float moveSpped = m_moveSpeed* dt;
            float rotSpeed = m_rotSpped * dt;

            if( Input.GetKey( KeyCode.W) )
            {
                this.transform.Translate(Vector3.forward * moveSpped);
            }
            if( Input.GetKey( KeyCode.A) )
            {
                this.transform.Translate(Vector3.left * moveSpped);
            }
            if( Input.GetKey( KeyCode.D) )
            {
                this.transform.Translate(Vector3.right * moveSpped);
            }

            if( Input.GetKey( KeyCode.Q) )
            {
                this.transform.RotateAround(this.transform.position, Vector3.up, -rotSpeed );
            }
            if( Input.GetKey( KeyCode.E) )
            {
                this.transform.RotateAround(this.transform.position, Vector3.up, rotSpeed);
            }
            // 反転
            if( Input.GetKeyDown( KeyCode.S) )
            {
                this.transform.RotateAround(this.transform.position, Vector3.up, 180.0f);
            }

        }
    }
}
