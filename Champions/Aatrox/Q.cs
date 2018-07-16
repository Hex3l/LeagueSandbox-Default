using System.Numerics;
using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Collections.Generic;

namespace Spells
{
    public class AatroxQ : GameScript
    {
        public void OnActivate(Champion owner)
        {
            ApiFunctionManager.LogInfo("Aatrox Q OnActivate");
        }

        public static void OnProc(AttackableUnit target, bool isCrit)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var spellData = spell.SpellData;
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;
            if (to.Length() > 580)
            {
                to = Vector2.Normalize(to);
                var range = to * 580;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }

            //ApiFunctionManager.DashToLocation(owner, current.X, current.Y, 3200, false, "Spell1", 20);
            ApiFunctionManager.AddParticleTarget(owner, "Aatrox_Base_Q_Tar_Green.troy", new Target(trueCoords));
            ApiFunctionManager.AddParticleTarget(owner, "Aatrox_Base_Q_Cast.troy", owner);
            // "GlobalHit_Yellow_tar.troy"
            ApiFunctionManager.CreateTimer(0.3f, () =>
            {
                if (owner.IsDashing)
                {
                    ApiFunctionManager.CancelDash(owner);
                }
                ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 2200, false, "Spell1", 80);
                ApiFunctionManager.CreateTimer(0.22f, () =>
                {
                    ApplyDamage(owner, spell);
                    ApiFunctionManager.AddParticleTarget(owner, "Aatrox_Base_Q_land_sound.troy", new Target(trueCoords));
                    //ApiFunctionManager.AddParticleTarget(owner, "Aatrox_Base_Q_Land.troy", new Target(trueCoords));
                });
            });
        }

        public void ApplyDamage(Champion owner, Spell spell)
        {
            try { 
                ApiFunctionManager.LogInfo("Aatrox Q ApplyDamage");
                List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(new Target(spell.X, spell.Y), 225, true);
                foreach(AttackableUnit unit in units){
                    var current = new Vector2(unit.X, unit.Y);
                    var to = new Vector2(spell.X, spell.Y) - current;
                    if (to.Length() <= 75) {
                        //TODO: implement KnockUp
                        //Adding stun temporary
                        ApiFunctionManager.AddBuff("Stun", 1, 1, (ObjAIBase)unit, owner);
                        ApiFunctionManager.AddParticleTarget(owner, "Aatrox_Base_Q_Hit.troy", unit);
                    }
                    var ad = owner.GetStats().AttackDamage.Total * 1.1f;
                    var damage = 20 + (spell.Level - 1) * 30 + ad;

                    unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                }
            }
            catch (InvalidOperationException e) {
                ApiFunctionManager.LogInfo(e.StackTrace);
                Console.WriteLine(e.StackTrace);
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
    }
}
