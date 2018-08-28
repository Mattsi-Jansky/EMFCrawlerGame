namespace Crawler.Queryables.Entities.Components
{
    public class WalletComponent : Component
    {
        private int gold = 0;

        public override void GiveGold(int value)
        {
            gold += value;
        }
    }
}