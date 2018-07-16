using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace BotrkBuff
{
    internal class BotrkBuff : IBuffGameScript
    {
        private ObjAiBase _owningUnit;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _owningUnit = unit;
            ApiEventManager.OnHitUnit.AddListener(this, unit, OnHitEffect);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
        }

        private void OnHitEffect(AttackableUnit target, bool isCrit)
        {
            if (!(target is Minion || target is Monster || target is Champion)) // Blade of the Ruined King will only damage minions, monsters, and champions.
            {
                return;
            }

            var damage = System.Math.Max(target.Stats.CurrentHealth * 0.08f, 15); // 8% of the target's current health (15 minimum)
            if (target is Minion || target is Monster) // Bonus Damage from Blade of the Ruined King is capped at 60 for Minions and Monsters.
            {
                damage = System.Math.Min(damage, 60);
            }
            target.TakeDamage(_owningUnit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}