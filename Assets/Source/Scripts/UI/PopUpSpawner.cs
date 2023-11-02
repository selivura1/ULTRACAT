using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool<PopUp> _popUpPool;
    [SerializeField] PopUp _damagePopup;
    private void Awake()
    {
        _popUpPool = new ObjectPool<PopUp>(_damagePopup,0, transform);
    }
    public void SpawnPopUp(Vector3 position, string text, Color color)
    {
        var spawned = _popUpPool.GetFreeElement();
        spawned.Initialize(text, color, position);
        spawned.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
