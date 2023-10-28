using UnityEngine;

public class GameExitHandler : MonoBehaviour
{
    float timer;
    [SerializeField] float timeReq = 3;
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            timer = 0;
        }
        if (timer >= timeReq)
        {
            Application.Quit();
        }
    }
}
