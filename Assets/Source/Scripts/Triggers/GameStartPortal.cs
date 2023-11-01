using UnityEngine;

namespace Ultracat
{
    public class GameStartPortal : Trigger
    {
        public override void OnTouch(Collision2D collision)
        {
            base.OnTouch(collision);
            FindAnyObjectByType<DungeonGenerator>().CreateDungeon();
        }
    }
}