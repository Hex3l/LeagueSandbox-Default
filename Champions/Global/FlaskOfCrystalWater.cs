using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class FlaskOfCrystalWater : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            for (int i = 0; i < 30; i++)
            {
                ApiFunctionManager.CreateTimer(i * 0.5f, () =>
                   {
                       var maxMana = owner.Stats.ManaPoints.Total;
                       var newMana = owner.Stats.CurrentMana + 3.33f;
                       if (newMana > maxMana)
                       {
                           owner.Stats.CurrentMana = maxMana;
                       }
                       else
                       {
                           owner.Stats.CurrentMana = newMana;
                       }
                   });
            }
            var p = ApiFunctionManager.AddParticleTarget(owner, "GLOBAL_Item_ManaPotion.troy", owner);
            ApiFunctionManager.CreateTimer(15f, () =>
             {
                 ApiFunctionManager.RemoveParticle(p);
             });
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