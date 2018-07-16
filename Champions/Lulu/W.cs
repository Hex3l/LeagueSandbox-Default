using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class LuluW : IGameScript
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
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            Champion champion = (Champion) target;
            if (champion.Team != owner.Team)
            {
                spell.AddProjectileTarget("LuluWTwo", target);
            }
            else
            {
                Particle p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_02.troy", target, 1);
                ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_01.troy", target, 1);
                float time = 2.5f + 0.5f * spell.Level;
                var buff = ((ObjAiBase) target).AddBuffGameScript("LuluWBuff", "LuluWBuff", spell);
                var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluWBuff", time, 1, (ObjAiBase) target);
                ApiFunctionManager.CreateTimer(time, () =>
                {
                    ApiFunctionManager.RemoveParticle(p);
                    ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                    owner.RemoveBuffGameScript(buff);
                });
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            Champion champion = (Champion) target;
            float time = 1 + 0.25f * spell.Level;
            var buff = ((ObjAiBase) target).AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluWDebuff", time, 1, (ObjAiBase) target);
            string model = champion.Model;
            ChangeModel(owner.Skin, target);

            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_polymorph_01.troy", target, 1);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
               champion.Model = model;
            });
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }

        private void ChangeModel(int skinId, AttackableUnit target)
        {
            switch (skinId)
            {
                case 0:
                    target.Model = "LuluSquill";
                    break;
                case 1:
                    target.Model = "LuluCupcake";
                    break;
                case 2:
                    target.Model = "LuluKitty";
                    break;
                case 3:
                    target.Model = "LuluDragon";
                    break;
                case 4:
                    target.Model = "LuluSnowman";
                    break;
            }
        }
    }
}
