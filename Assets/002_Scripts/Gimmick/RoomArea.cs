using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CovaTech.LT.AudioMixerSample
{
    public interface IAreaController
    {
        bool IsInside();
        bool IsOutSide();

    }


    public class RoomArea : MonoBehaviour, IAreaController
    {
        private bool m_isInside = false;

        private void OnTriggerEnter()
        {
            m_isInside = true;
        }

        private void OnTriggerExit()
        {
            m_isInside = false;            
        }


        //------------------------------------------------------------------
        // IAreaController実装
        //------------------------------------------------------------------
        #region  ===== IAreaController =====

        bool IAreaController.IsInside(){ return m_isInside;}
        bool IAreaController.IsOutSide(){ return !m_isInside;}

        #endregion //) ===== IAreaController =====
        

    }
}
