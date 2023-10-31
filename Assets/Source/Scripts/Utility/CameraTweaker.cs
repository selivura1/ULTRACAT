using Cinemachine;
using UnityEngine;
namespace Ultracat
{
    public class CameraTweaker : MonoBehaviour
    {
        public static CameraTweaker inst;
        CinemachineVirtualCamera _vcam;
        CinemachineBasicMultiChannelPerlin _noise;
        float _timer;
        private PlayerEntity _player;

        private void Awake()
        {
            if (inst == null)
                inst = this;
            else
                Destroy(this);
            _player = FindAnyObjectByType<PlayerEntity>();
        }
        void Start()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
            _noise = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            SetPlayer(_player.transform);
        }
        public static void Shake(float value, float frec, float time)
        {
            inst._noise.m_AmplitudeGain = value;
            inst._noise.m_FrequencyGain = frec;
            inst._timer = time;
        }
        public static void SetPlayer(Transform follow)
        {
            inst._vcam.Follow = follow;
        }
        private void Update()
        {
            if (_timer <= 0)
            {
                _noise.m_AmplitudeGain = 0;
                _noise.m_FrequencyGain = 0;
            }
            else
                _timer -= Time.deltaTime;
        }
    }
}