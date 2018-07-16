using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class AatroxPassive : GameScript
    {
        static Champion owner;
        static float latestSpell;
        public void OnActivate(Champion owner)
        {
            AatroxPassive.owner = owner;
            owner.GetStats().CurrentMana -= 10;
            ApiEventManager.OnSpellHit.AddListener(OnSpellHit);
        }

        private void OnSpellHit(AttackableUnit unit, Spell spell)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
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

