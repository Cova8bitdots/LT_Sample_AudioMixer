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
        // メンバ変数関連
        //------------------------------------------------------------------
        #region  ===== MEMBER_VARIABLES =====
        [SerializeField]
        private Transform m_doorObject =null;
        [SerializeField, Range( 0.01f, 10.0f)]
        private float m_animTime =1.0f;
        [SerializeField, Range( -360.0f, 360.0f)]
        private float m_rotAngle =0.0f;

        private IAudioPlayer m_audioPlayer = null;
        private IMixerEffectController m_effectCtrl = null;

        private Coroutine m_doorAnimCoroutine = null;

        private int m_bgmHandler = SoundConsts.INVALID_HANDLER;
        #endregion //) ===== MEMBER_VARIABLES =====
        

        //-----------------------------------------------------
        // 初期化
        //-----------------------------------------------------
        #region ===== INITIALIZE =====

        [Inject]
        public void Initialize( IAudioPlayer _audioPlayer, IMixerEffectController _mixerEffectCtrl )
        {
            Debug.Assert( _audioPlayer != null);
            Debug.Assert( _mixerEffectCtrl != null);
            m_audioPlayer = _audioPlayer;
            m_effectCtrl = _mixerEffectCtrl;
        }

        private async void Start()
        {
            m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, 0.0f);
            if( m_audioPlayer != null )
            {
                m_bgmHandler =  await m_audioPlayer.PlayBgm( (int)BGM_ID.SAMPLE_BGM, 1.0f, true, new System.Threading.CancellationToken() );
            }
        }
        #endregion //) ===== INITIALIZE =====

        private void OnDestroy()
        {
            if( m_audioPlayer != null && m_bgmHandler != SoundConsts.INVALID_HANDLER )
            {
                m_audioPlayer.StopBGM( m_bgmHandler, true, new System.Threading.CancellationToken() ).Forget( e => Debug.LogError(e.Message));
            }
        }


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
                m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, ratio);
                yield return null;
            }
            m_doorObject.localRotation = targetRotation;
            m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, 1.0f);


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
                m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, 1.0f - ratio);
                yield return null;
            }
            m_doorObject.localRotation = targetRotation;

            m_doorObject.localRotation = Quaternion.Euler( Vector3.zero);
            m_effectCtrl?.SetLowPassFilter( SOUND_CATEGORY.DIEGETIC_BGM, 0.0f);

        }
    }
}
