using UnityEngine;

//Spawns popUp's (ex. damage/heal/info)
public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] PopUp damagePopup;
    public void SpawnPopUp(Vector3 position, string text, Color color)
    {
        var spawned = Instantiate(damagePopup, transform);
        spawned.Initialize(text, color, position);
        spawned.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
