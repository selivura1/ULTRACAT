using UnityEngine;
namespace Ultracat
{
    public class PlayerSpawner : MonoBehaviour
    {
        PlayerEntity player;
        [SerializeField] GameObject playerPref;
        //[SerializeField] float gameOverDelay = 2;
        private void Awake()
        {
            SpawnPlayer();
        }
        public PlayerEntity GetPlayer()
        {
            return player;
        }
        PlayerEntity SpawnPlayer()
        {
            if (player != null) return player;
            player = Instantiate(playerPref).GetComponent<PlayerEntity>();
            player.onDeath += GameOverInvoker;
            return player;
        }
        public void GameOverInvoker(EntityBase fix)
        {
            //Invoke(nameof(GameOver), gameOverDelay);
            GameOver();
        }
        private void GameOver()
        {
            player.gameObject.SetActive(false);
            var ents = FindObjectsByType<EntityBase>(FindObjectsSortMode.InstanceID);
            foreach (var item in ents)
            {
                item.gameObject.SetActive(false);
            }
            player.onDeath -= GameOverInvoker;
        }
        public void ResetGame()
        {
            Destroy(player);
        }
    }
}