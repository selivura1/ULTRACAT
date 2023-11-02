using System.Collections.Generic;
using UnityEngine;
namespace Ultracat
{
    public class PlayerLevels : MonoBehaviour
    {
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; } = 0;
        public int ExperienceToNextLevel { get; private set; } = 5;
        public int Tokens = 0;
        [SerializeField] int scaling = 5;
        [SerializeField] int startExpToNextLevel = 5;
        [SerializeField] private int _generatedItemsAmount = 3;
        public List<Item> RandomItems = new List<Item>();
        private PlayerEntity _player;
        public System.Action onLevelUp;
        public System.Action onExperienceChange;
        public System.Action onTokenUsed;
        public System.Action onItemsChanged;
        private void Awake()
        {
            ExperienceToNextLevel = startExpToNextLevel;
            _player = GetComponent<PlayerEntity>();
            GenerateNewRandomItems();
        }
        private void Update()
        {
#if UNITY_EDITOR
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    AddExperience(1);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    AddExperience(10);
                }
            }
#endif
        }
        public void AddExperience(int amount)
        {
            Experience += amount;
            CheckForLevelUp();
            onExperienceChange?.Invoke();
        }
        private void CheckForLevelUp()
        {
            if (Experience >= ExperienceToNextLevel)
            {
                Experience = Experience - ExperienceToNextLevel;
                Level++;
                ExperienceToNextLevel += Level * scaling;
                Tokens++;
                onLevelUp?.Invoke();
            }
        }
        public void GenerateNewRandomItems()
        {
            RandomItems.Clear();
            RandomItems.AddRange(GameManager.GetRandomObjectsFromList(GameManager.Database.UnlockedItems, _generatedItemsAmount, false));
            onItemsChanged?.Invoke();
        }
        public void UseToken(int index)
        {
            Tokens--;
            _player.GetComponent<Inventory>().Give(RandomItems[index]);
            _player.Heal(999);
            GenerateNewRandomItems();
            onTokenUsed?.Invoke();
        }
    }
}