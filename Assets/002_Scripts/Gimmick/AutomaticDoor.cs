using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CovaTech.UnitySound;

namespace CovaTech.LT.AudioMixerSample
{
    public class AutomaticDoor : MonoBehaviour
    {
        //------------------------------------------------------------------
        // メンバ変数関連
        //------------------------------------------------------------------
        #region  ===== MEMBER_VARIABLES =====
        
        private IMixerEffectController m_effectCtrl = null;
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

        #endregion //) ===== INITIALIZE =====


        private void OnCollisionEnter()
        {

        }

        private void OnCollisionExit()
        {
            
        }




        private IEnumerator OpenDoorAnimation()
        {
            yield return null;
        }

        private IEnumerator CloseDoorAnimation()
        {
            yield return null;
        }
    }
}
