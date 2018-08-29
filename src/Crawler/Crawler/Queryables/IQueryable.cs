using Crawler.Models;
using System.Collections.Generic;
using Crawler.Commands;
using Crawler.ObjectResolvers;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Queryables
{
    public interface IQueryable
    {
        bool CanMove();
        void GetGraphics(ref IList<Graphic> graphics);
        void GetPosition(ref Point? position);
        void SetPosition(Point position);
        string GetDisplayText();
        string GetName();
        void AttachParent(IQueryable parent);
        void DetatchParent();
        ICommand GetCommand();
        void InitialiseController(ObjectResolver objectResolver);
        bool IsBlocked();
        void RecieveMessage(string message);
        IList<string> GetMessages();
        EquipableComponent GetEquipable(EquipableSlot slot);
        void GetEquipables(ref List<EquipableComponent> equipables);
        string GetPreTitle();
        string GetPostTitle();
        void GetGold(ref IList<GoldComponent> gold);
        void GiveGold(int value);
        void GetStrength(ref int value);
        void GetDexterity(ref int value);
        void GetConstitution(ref int value);
        void GetWisdom(ref int value);
        void GetHitBonus(ref int value);
        void GetWeapon(ref WeaponComponent weapon);
        void GetDamageBonus(ref int value);
        void TakeDamage(int damage);
        bool IsDead();
        void GetArmourClass(ref int ac);
        void GetDrops(ref List<Entity> drops);
    }
}
