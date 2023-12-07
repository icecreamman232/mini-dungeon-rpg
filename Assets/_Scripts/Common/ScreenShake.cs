using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Events;
using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Common
{
    public class ScreenShake : MonoBehaviour
    {
        [SerializeField] private GlobalRuntimeVariables m_runtimeVariables;
        [SerializeField] private ScreenShakeEvent m_shakeEvent;
        
        private void Start()
        {
            m_shakeEvent.AddListener(DoShake);
        }

        private void DoShake(ShakeProfile profile)
        {
            StartCoroutine(ShakeRoutine(profile));
        }

        private IEnumerator ShakeRoutine(ShakeProfile profile)
        {
            var duration = profile.ShakeDuration;
            while (duration > 0)
            {
                Vector3 randomPos = Random.insideUnitCircle * profile.ShakePower;
                randomPos.z = -10;
                m_runtimeVariables.MainCamera.transform.position += randomPos;
                yield return new WaitForSeconds(profile.Frequency);
                duration -= profile.Frequency;
            }
            //ResetCamera();
        }

        private void ResetCamera()
        {
            var pos = Vector3.zero;
            pos.z = -10;
            m_runtimeVariables.MainCamera.transform.position = pos;
        }

        private void OnDestroy()
        {
            m_shakeEvent.RemoveListener(DoShake);
        }
    } 
}

