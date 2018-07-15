using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class KatarinaW : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL2", owner);
            ApiFunctionManager.AddParticleTarget(owner, "katarina_W_cas.troy", owner);
            ApiFunctionManager.AddParticleTarget(owner, "katarina_w_mis.troy", owner);
            ApiFunctionManager.AddParticleTarget(owner, "katarina_W_Dagger", owner);

            //most of the particles have been found, still dont know how to get the spinning red and black tho.

        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var damage = new[] { 40, 75, 110, 145, 180 }[spell.Level - 1] + (owner.Stats.AbilityPower.Total * 0.6f) + (owner.Stats.AttackDamage.Total * 0.25f);
            bool shouldGrantMovementSpeed = false;

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 375, true))
            {
                if (enemyTarget != owner && owner.GetDistanceTo(enemyTarget) < 375 && (enemyTarget.Team != owner.Team) && (!(enemyTarget is BaseTurret)))
                {
                    enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    ApiFunctionManager.AddParticleTarget(owner, "katarina_w_tar.troy", enemyTarget);
                    if(enemyTarget is Champion)
                    {
                        shouldGrantMovementSpeed = true;
                    }
                }
            }

            if (shouldGrantMovementSpeed)
            {
                var buff = ((ObjAiBase)target).AddBuffGameScript("KatarinaWBuff", "KatarinaWBuff", spell, -1, true);
                ApiFunctionManager.AddBuffHudVisual("KatarinaW", 1.0f, 1, owner, 1.0f);

                ApiFunctionManager.CreateTimer(1.0f, () =>
                {
                    owner.RemoveBuffGameScript(buff);
                });
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
