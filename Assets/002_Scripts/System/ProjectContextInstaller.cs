using UnityEngine;
using Zenject;
using CovaTech.UnitySound;

namespace CovaTech.LT.AudioMixerSample
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager m_soundManager = null;

        public override void InstallBindings()
        {

            Container.Bind<IAudioPlayer>().FromInstance( m_soundManager ).AsCached();
            Container.Bind<IVolumeController>().FromInstance( m_soundManager ).AsCached();
            Container.Bind<IMixerEffectController>().FromInstance( m_soundManager ).AsCached();
        }

        void Awake()
        {
            m_soundManager.Initialize();
        }
    }


}