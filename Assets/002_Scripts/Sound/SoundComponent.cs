using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

using CovaTech.Extentions;
using CovaTech.UnitySound;
using Cysharp.Threading.Tasks;

namespace CovaTech.LT.AudioMixerSample
{
    public class SoundComponent : MonoBehaviour, System.IDisposable
    {
        [System.Serializable]
        public class SoundParam
        {
            public BGM_ID BgmId = BGM_ID.UNDEFINED;
            public SE_ID SeId = SE_ID.UNDEFINED;
            [Range( 0.0f, 1.0f)]
            public float Volume = 1.0f;
            public bool IsLoop = false;
        }

        //------------------------------------------------------------------
        // メンバ変数関連
        //------------------------------------------------------------------
        #region  ===== MEMBER_VARIABLES =====
        [SerializeField]
        private SoundParam[] m_param = null;


        private IAudioPlayer m_audioPlayer = null;

        private List<(int, float)> m_bgmHandlerList = null;
        private List<(int, float)> m_seHandlerList = null;
        private List<CancellationTokenSource> m_tokenSourceList = null;

        private bool m_isDisposed = false;
        #endregion //) ===== MEMBER_VARIABLES =====
        

        //-----------------------------------------------------
        // 初期化
        //-----------------------------------------------------
        #region ===== INITIALIZE =====

        [Inject]
        public void Initialize( IAudioPlayer _audioPlayer )
        {
            Debug.Assert( _audioPlayer != null);

            m_audioPlayer = _audioPlayer;
        }

        private async void Start()
        {
            if( m_audioPlayer == null || m_param.IsNullOrEmpty() )
            {
                return;
            }

            m_bgmHandlerList = new List<(int, float)>( m_param.Length );
            m_seHandlerList = new List<(int, float)>( m_param.Length );
            m_tokenSourceList = new List<CancellationTokenSource>();
            for (int i = 0; i < m_param.Length; i++)
            {
                if( m_isDisposed )
                {
                    break;
                }
                var tokenSource = new CancellationTokenSource();
                m_tokenSourceList.Add( tokenSource );
                int handler = await PrepareBgm( m_param[i], m_audioPlayer, tokenSource.Token);
                if( handler != SoundConsts.INVALID_HANDLER )
                {
                    m_bgmHandlerList.Add( (handler, m_param[i].Volume) );
                }

                if( m_isDisposed )
                {
                    break;
                }

                tokenSource = new CancellationTokenSource();
                m_tokenSourceList.Add( tokenSource );
                handler = await PrepareSe( m_param[i], m_audioPlayer, tokenSource.Token);
                if( handler != SoundConsts.INVALID_HANDLER )
                {
                    m_seHandlerList.Add( (handler, m_param[i].Volume) );
                }
            }

            for (int i = 0, length = m_bgmHandlerList.Count; i < length; i++)
            {
                m_audioPlayer?.PlayBgm( m_bgmHandlerList[i].Item1,m_bgmHandlerList[i].Item2);
            }
            m_bgmHandlerList.Clear();

            for (int i = 0, length = m_seHandlerList.Count; i < length; i++)
            {
                m_audioPlayer?.PlaySe( m_seHandlerList[i].Item1,m_seHandlerList[i].Item2);
            }

        }
        #endregion //) ===== INITIALIZE =====

        private async UniTask<int> PrepareBgm( SoundParam _param, IAudioPlayer _player, CancellationToken _token )
        {
            if( _param == null || _player == null )
            {
                return SoundConsts.INVALID_HANDLER;
            }
            if( _param.BgmId == BGM_ID.UNDEFINED )
            {
                return SoundConsts.INVALID_HANDLER;
            }
            var bgmParam = new BgmParam();
            bgmParam.SetId( (int)_param.BgmId);
            bgmParam.SetLoopFlag(_param.IsLoop);
            bgmParam.SetVolume(_param.Volume);
            bgmParam.SetPosition( this.transform.position );
            return await _player.PrewarmBGM(bgmParam, _token);
        }

        private async UniTask<int> PrepareSe( SoundParam _param, IAudioPlayer _player, CancellationToken _token )
        {
            if( _param == null || _player == null )
            {
                return SoundConsts.INVALID_HANDLER;
            }
            if( _param.SeId == SE_ID.UNDEFINED )
            {
                return SoundConsts.INVALID_HANDLER;
            }
            var seParam = new SeParam();
            seParam.SetId( (int)_param.SeId);
            seParam.SetLoopFlag(_param.IsLoop);
            seParam.SetPosition( this.transform.position );
            return await _player.PrewarmSE(seParam, _token);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            if( m_isDisposed )
            {
                return;
            }
            m_isDisposed = true;

            for (int i = 0, length = m_tokenSourceList.Count; i < length; i++)
            {
                m_tokenSourceList[i].Cancel();
            }
            m_tokenSourceList.Clear();

            for (int i = 0, length = m_bgmHandlerList.Count; i < length; i++)
            {
                m_audioPlayer?.StopBGM( m_bgmHandlerList[i].Item1, _isForceStop:true, CancellationToken.None).Forget( e=> Debug.LogError( e.Message));
            }
            m_bgmHandlerList.Clear();

            for (int i = 0, length = m_seHandlerList.Count; i < length; i++)
            {
                m_audioPlayer?.StopSE( m_seHandlerList[i].Item1, _isForceStop:true, CancellationToken.None).Forget( e=> Debug.LogError( e.Message));
            }
            m_seHandlerList.Clear();
        }

    }
}
