namespace Crawler.Queryables.Entities.Components
{
    public class CharacterComponent : Component
    {
        private int str, dex, con, wis;

        public CharacterComponent(int str, int dex, int con, int wis)
        {
            this.str = 8 + str;
            this.dex = 8 + dex;
            this.con = 8 + con;
            this.wis = 8 + wis;
        }
        
        public override void GetStrength(ref int value)
        {
            value += str;
        }

        public override void GetDexterity(ref int value)
        {
            value += dex;
        }

        public override void GetConstitution(ref int value)
        {
            value += con;
        }

        public override void GetWisdom(ref int value)
        {
            value += wis;
        }
    }
}