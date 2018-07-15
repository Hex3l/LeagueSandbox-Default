using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;

namespace Spells
{
    public class ItemSwordOfFeastAndFamine : IGameScript
    {

        private Champion _owningChampion;
        private BuffGameScriptController _botrkBuff;

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

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
            _botrkBuff = owner.AddBuffGameScript("BotrkBuff", "BotrkBuff", null);
        }

        public void OnDeactivate(Champion owner)
        {
            owner.RemoveBuffGameScript(_botrkBuff);
        }
    }
}
