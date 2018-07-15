using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;


namespace Spells
{
    public class Highlander : IGameScript
    {

        public void OnActivate(Champion owner)
        {

        }

        public void OnDeactivate(Champion owner)
        {
        }
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL4", owner);
            ApiFunctionManager.AddBuffHudVisual("Highlander", 10.0f, 1, owner, 10.0f);
        }
        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            float duration =  10.0f;

            var highlanderbuff = ((ObjAiBase)target).AddBuffGameScript("HighlanderBuff", "HighlanderBuff", spell, -1, true);

            ApiFunctionManager.CreateTimer(duration, () =>
            {
                owner.RemoveBuffGameScript(highlanderbuff);
            });

        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {

        }

        public void OnUpdate(double diff)
        {
        }
    }
}
