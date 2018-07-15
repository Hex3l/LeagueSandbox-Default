using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class DetonatingShot : IGameScript
    {
        public void OnActivate(Champion owner)
        {	
        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.CreateTimer(1.0f, () => {
                var ap = owner.Stats.AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(2.0f, () => {
                var ap = owner.Stats.AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(3.0f, () => {
                var ap = owner.Stats.AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(4.0f, () => {
                var ap = owner.Stats.AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(5.0f, () => {
                var ap = owner.Stats.AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });				
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {	
            spell.AddProjectileTarget("DetonatingShot", target, false);	
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            float time = 5;
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("ExplosiveShotDebuff", time, 5, (ObjAiBase) target);			
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
            });			
            projectile.SetToRemove();			
        }

        public void OnUpdate(double diff)
        {
        }
    }
}


