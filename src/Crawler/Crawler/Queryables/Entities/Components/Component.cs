﻿using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Models;
using Crawler.ObjectResolvers;

namespace Crawler.Queryables.Entities.Components
{
    public abstract class Component : IQueryable
    {
        public IQueryable Parent { get; private set; }

        public virtual bool CanMove() { return true; }
        public virtual void GetGraphics(ref IList<Graphic> graphics) { }
        public virtual void GetPosition(ref Point? position) { }
        public virtual void SetPosition(Point position) { }
        public virtual string GetDisplayText() { return default(string); }
        public virtual string GetName() { return default(string); }
        public virtual ICommand GetCommand() { return default(ICommand); }
        public virtual void InitialiseController(ObjectResolver objectResolver) { }
        public virtual bool IsBlocked() { return default(bool); }
        public virtual void RecieveMessage(string message) { }
        public virtual IList<string> GetMessages() { return new List<string>(); }
        public virtual EquipableComponent GetEquipable(EquipableSlot slot) { return default(EquipableComponent); }
        public virtual void GetEquipables(ref List<EquipableComponent> equipables) { }
        public virtual string GetPreTitle() { return string.Empty; }
        public virtual string GetPostTitle() { return string.Empty; }
        public virtual void GetGold(ref IList<GoldComponent> gold) { }
        public virtual void GiveGold(int value) { }
        public virtual void GetStrength(ref int value) { }
        public virtual void GetDexterity(ref int value) { }
        public virtual void GetConstitution(ref int value) { }
        public virtual void GetWisdom(ref int value) { }
        public virtual void GetHitBonus(ref int value) { }
        public virtual void GetWeapon(ref WeaponComponent weapon) { }
        public virtual void GetDamageBonus(ref int value) { }
        public virtual void TakeDamage(int damage) { }
        public virtual bool IsDead() { return default(bool); }
        public virtual void GetArmourClass(ref int ac) { }
        public virtual void GetDrops(ref List<Entity> drops) { }

        public virtual void AttachParent(IQueryable parent)
        {
            Parent = parent;
        }

        public void DetatchParent()
        {
            Parent = null;
        }
    }
}