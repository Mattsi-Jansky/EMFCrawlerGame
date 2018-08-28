namespace Crawler.Queryables.Entities.Components
{
    public class CharacterComponent
    {
        int str, dex, con, wis;

        public CharacterComponent(int str, int dex, int con, int wis)
        {
            this.str = 8 + str;
            this.dex = 8 + dex;
            this.con = 8 + con;
            this.wis = 8 + wis;
        }
    }
}