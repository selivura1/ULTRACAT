namespace Ultracat
{
    public class SoulCollectible : Bonus
    {
        public override void OnCollected()
        {
            _target.GetComponent<PlayerLevels>().AddExperience((int)_value);
            gameObject.SetActive(false);
        }
    }
}