using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;
using System.Collections.Generic;


namespace Spells
{
    public class AlphaStrike : IGameScript
    {
        private Champion _owningChampion;
        private Spell _owningSpell;
        private List<AttackableUnit> _currentTargetList;

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            _owningSpell = spell;

            spell.AddProjectileTarget("AlphaStrike", target);
            _owningChampion.Stats.Size.BaseValue = 0;
            ApiFunctionManager.CreateTimer(0.8f, () => {
                _owningChampion.Stats.Size.BaseValue = 1;
                ApiFunctionManager.TeleportTo(owner, target.X + 80, target.Y + 80);
            });
            _currentTargetList = ApiFunctionManager.GetUnitsInRange(target,600,true);
            foreach (var enemyTarget in _currentTargetList)
            {
                var isValidUnit = enemyTarget is Minion || enemyTarget is Monster || enemyTarget is Champion;
                if (enemyTarget != target && enemyTarget != owner && target.GetDistanceTo(enemyTarget) < 100 && isValidUnit)
                {
                    ApiFunctionManager.CreateTimer(1.0f, () => {
                        var p1 = ApiFunctionManager.AddParticle(owner, "MasterYi_Base_Q_tar.troy", enemyTarget.X, enemyTarget.Y);
                        spell.SpellAnimation("SPELL1", owner);
                        var p2 = ApiFunctionManager.AddParticleTarget(owner, "MasterYi_Base_Q_tar.troy", target);
                        var p3 = ApiFunctionManager.AddParticleTarget(owner, "MasterYi_Base_Q_mis.troy", owner);
                        spell.AddProjectileTarget("AlphaStrike", enemyTarget);
                        ApiFunctionManager.CreateTimer(0.8f, () =>
                        {
                             ApiFunctionManager.RemoveParticle(p1);
                             ApiFunctionManager.RemoveParticle(p2);
                             ApiFunctionManager.RemoveParticle(p3);
                        });
                    });
                }
            }
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            float damage = new[] { 25, 60, 95, 130, 165 }[spell.Level - 1] + owner.Stats.AttackDamage.Total * 1f + owner.Stats.AbilityPower.Total * 0.7f;
            foreach (var enemyTarget in _currentTargetList)
            {
                bool isMinionOrMonster = enemyTarget is Minion || enemyTarget is Monster;
                bool isChampion = enemyTarget is Champion;
                if((enemyTarget.Team != owner.Team) || (enemyTarget == owner) || (!isChampion) || (!isMinionOrMonster))
                {
                    continue;
                }
                if(isMinionOrMonster)
                {
                    damage += new[] { 75 , 100 , 125 , 150 , 175 }[spell.Level - 1];
                }
            }
            _currentTargetList = null;
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            projectile.SetToRemove();

        }
        public void OnUpdate(double diff)
        {
        }
    }
}
