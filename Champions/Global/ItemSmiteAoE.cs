using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class ItemSmiteAoE : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Smite_AoEStun_Monster.troy", target, 1);
            var damage = (new float[] { 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 })[owner.Stats.Level - 1];
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL, false);
            var AOEDamage = damage / 2;
            var buff1 = ((ObjAiBase)target).AddBuffGameScript("Stun", "Stun", spell);
            ApiFunctionManager.CreateTimer(1.5f, () =>
            {
                ((ObjAiBase)target).RemoveBuffGameScript(buff1);
            });
            var spellData = spell.SpellData;
            var sideTargets = ApiFunctionManager.GetUnitsInRange(target, spellData.BounceRadius, true);
            foreach (AttackableUnit additionalTarget in sideTargets)
            {
                if((additionalTarget is Minion) || (additionalTarget is Monster))
                {
                    if(additionalTarget.Team == owner.Team)
                    {
                        continue;
                    }
                    additionalTarget.TakeDamage(owner,AOEDamage,DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL,false);
                    if (!additionalTarget.IsDead)
                    {
                        var buff2 = ((ObjAiBase)additionalTarget).AddBuffGameScript("Stun", "Stun", spell);
                        ApiFunctionManager.CreateTimer(1.5f, () =>
                         {
                             ((ObjAiBase)additionalTarget).RemoveBuffGameScript(buff2);
                         });
                    }
                }
            }

            if (target is Monster) {
                RestoreHealth(owner);
                RestoreMana(owner);
            }
        }

        public void RestoreHealth(Champion owner)
        {
            var maxHealth = owner.Stats.HealthPoints.Total;
            var missingHealth = (maxHealth) - (owner.Stats.CurrentHealth);
            owner.RestoreHealth(missingHealth);
        }

        public void RestoreMana(Champion owner)
        {
            var maxMana = owner.Stats.ManaPoints.Total;
            var missingMana = maxMana - (owner.Stats.CurrentMana);
            var newMana = (owner.Stats.CurrentMana) + (missingMana * 0.15f);
            if (newMana >= maxMana)
            {
                owner.Stats.CurrentMana = maxMana;
            }
            else
            {
                owner.Stats.CurrentMana = newMana;
            }
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}
