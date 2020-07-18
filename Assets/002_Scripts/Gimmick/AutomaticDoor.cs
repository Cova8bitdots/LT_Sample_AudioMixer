using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CovaTech.UnitySound;
using Cysharp.Threading.Tasks;

namespace CovaTech.LT.AudioMixerSample
{
    public class AutomaticDoor : MonoBehaviour
    {
        //------------------------------------------------------------------
        // 定数関連
        //------------------------------------------------------------------
        #region  ===== CONSTS =====
        private const float NORMALIZED_FREQ_MAX = 1.0f;
        private const float NORMALIZED_FREQ_MIN = 0.01360f; // 300Hz

        #endregion //) ===== CONSTS =====
        

        //------------------------------------------------------------------
        // メンバ変数関連
        //------------------------------------------------------------------
        #region  ===== MEMBER_VARIABLES =====
        [SerializeField]
        private Transform m_doorObject =null;
        [SerializeField, Range( 0.01f, 10.0f)]
        private float m_animTime =1.0f;
        [SerializeField, Range( -360.0f, 360.0f)]
        private float m_rotAngle =0.0f;

        [SerializeField]
        private RoomArea m_roomArea = null;
        private IAreaController m_areaCtrl = null;

        private IMixerEffectController m_effectCtrl = null;

        private Coroutine m_doorAnimCoroutine = null;

        #endregion //) ===== MEMBER_VARIABLES =====
        

        //-----------------------------------------------------
        // 初期化
        //-----------------------------------------------------
        #region ===== INITIALIZE =====

        [Inject]
        public void Initialize( IMixerEffectController _mixerEffectCtrl )
        {
            Debug.Assert( _mixerEffectCtrl != null);
            m_effectCtrl = _mixerEffectCtrl;
        }

        private void Start()
        {
            m_areaCtrl = m_roomArea;
            m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, CalcFreq(0.0f) );
        }
        #endregion //) ===== INITIALIZE =====


        private void OnTriggerEnter()
        {
            if( m_doorAnimCoroutine != null )
            {
                StopCoroutine(m_doorAnimCoroutine);
                m_doorAnimCoroutine = null;
            }
            m_doorAnimCoroutine = StartCoroutine( OpenDoorAnimation() );
        }

        private void OnTriggerExit()
        {
            if( m_doorAnimCoroutine != null )
            {
                StopCoroutine(m_doorAnimCoroutine);
                m_doorAnimCoroutine = null;
            }
            m_doorAnimCoroutine = StartCoroutine( CloseDoorAnimation() );
            
        }

        /// <summary>
        /// 指定範囲内の正規化周波数を計算して返す
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        private float CalcFreq( float ratio)
        {
            return (NORMALIZED_FREQ_MAX- NORMALIZED_FREQ_MIN) * ratio + NORMALIZED_FREQ_MIN;
        }

        private void SetLowPassEffect( float ratio )
        {
            if( m_areaCtrl != null && m_areaCtrl.IsOutSide() )
            {
                m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, ratio );
            }       
        }

        private IEnumerator OpenDoorAnimation()
        {
            if( m_doorObject == null )
            {
                yield break;
            }

            Quaternion currentRotation = m_doorObject.localRotation;
            Quaternion targetRotation = Quaternion.Euler( m_rotAngle * Vector3.up);
            for (float t = 0; t < m_animTime; t+= Time.deltaTime)
            {
                float ratio = Mathf.Clamp01(t / m_animTime);
                m_doorObject.localRotation = Quaternion.Lerp( currentRotation, targetRotation, ratio);

                SetLowPassEffect( CalcFreq(ratio) );
                yield return null;
            }
            m_doorObject.localRotation = targetRotation;
            SetLowPassEffect( CalcFreq(1.0f) );

            yield return null;
        }

        private IEnumerator CloseDoorAnimation()
        {
            if( m_doorObject == null )
            {
                yield break;
            }

            Quaternion currentRotation = m_doorObject.localRotation;
            Quaternion targetRotation = Quaternion.Euler( Vector3.zero);
            for (float t = 0; t < m_animTime; t+= Time.deltaTime)
            {
                float ratio = Mathf.Clamp01(t / m_animTime);
                m_doorObject.localRotation = Quaternion.Lerp( currentRotation, targetRotation, ratio);
                SetLowPassEffect( CalcFreq(1.0f- ratio) );

                yield return null;
            }
            m_doorObject.localRotation = targetRotation;

            m_doorObject.localRotation = Quaternion.Euler( Vector3.zero);
            SetLowPassEffect( CalcFreq(0.0f) );

        }
    }
}
