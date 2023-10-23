using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] float _randomRotationMin = -180;
    [SerializeField] float _randomRotationMax = 180;
    public void Initialize(float lifetime)
    {
        CancelInvoke();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(_randomRotationMin, _randomRotationMax));
        Invoke(nameof(Terminate), lifetime);
    }
    public void Terminate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
