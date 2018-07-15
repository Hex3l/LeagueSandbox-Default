using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class LuluQ : IGameScript 
    {
        public void OnActivate(Champion owner)
        {
        }
        public void OnDeactivate(Champion owner)
        {
        }
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL1", owner);			
        }
        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        { 
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 925;
            var trueCoords = current + range;
			
            spell.AddProjectile("LuluQMissile", trueCoords.X, trueCoords.Y);
        }
        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        { 
            var ap = owner.Stats.AbilityPower.Total * 0.50f;
            var damage = 35 + spell.Level * 45 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.SetToRemove();

            float time = 2.00f + 0.01f * spell.Level;
            var buff = ((ObjAiBase) target).AddBuffGameScript("LuluQSlow", "LuluQSlow", spell);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluQSlow", time, 1, (ObjAiBase) target);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });			
        }
        public void OnUpdate(double diff)
        {
        }
    }
}
