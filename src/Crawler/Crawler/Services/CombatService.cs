using System;
using System.Linq;
using Crawler.Maps;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Tiles;

namespace Crawler.Services
{
    public class CombatService
    {
        private IMap _map;
        private readonly Random _random;
        private readonly EntitiesCollection _entitiesCollection;

        public CombatService(IMap map, Random random, EntitiesCollection entitiesCollection)
        {
            _map = map;
            _random = random;
            _entitiesCollection = entitiesCollection;
        }

        public void Attack(Entity attacker, Point target)
        {
            var targetTile = _map.Get(target);
            var opponentAc = GetArmourClass(targetTile);
            var hitBonus = GetHitBonus(attacker);
            var opponentName = targetTile.GetName();
            var attackerName = attacker.GetName();
            
            if (AttackHits(hitBonus, opponentAc))
            {
                var damage = CalculateDamage(attacker, targetTile);
                targetTile.TakeDamage(damage);
                
                attacker.RecieveMessage($"You attack {opponentName} for {damage} DMG!");
                targetTile.RecieveMessage($"{attackerName} attacks you for {damage} DMG!");
                RemoveTargetIfDead(targetTile);
            }
            else
            {
                attacker.RecieveMessage($"You attack {opponentName} but miss!");
                targetTile.RecieveMessage($"{attackerName} attacks you but misses!");
            }
        }
        
        private void RemoveTargetIfDead(Tile targetTile)
        {
            if (targetTile.IsDead())
            {
                RemoveEntity(null);
            }
        }

        private bool AttackHits(int hitBonus, int opponentAc)
        {
            var attackDice = new Dice(1, 20);
            return attackDice.RollOne(_random) + hitBonus > opponentAc;
        }

        private static int GetHitBonus(Entity attacker)
        {
            int hitBonus = 0;
            attacker.GetHitBonus(ref hitBonus);
            return hitBonus;
        }

        private static int GetArmourClass(Tile targetTile)
        {
            int opponentAc = 0;
            targetTile.GetArmourClass(ref opponentAc);
            return opponentAc;
        }

        public int CalculateDamage(Entity attacker, Tile tile)
        {
            Dice dice = null;
            attacker.GetDamageDice(ref dice);
            int damageBonus = 0;
            attacker.GetDamageBonus(ref damageBonus);

            return dice.RollAll(_random).Sum() + damageBonus;
        }

        private void RemoveEntity(Entity entity)
        {
            
        }
    }
}