using LeagueSandbox.GameServer.Logic.Content;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace EssenceFluxAttackSpeed
{
    internal class EssenceFluxAttackSpeed : IBuffGameScript
    {
        private StatsModifier _statMod;

        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.AttackSpeed.PercentBonus = new float[] { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f }[ownerSpell.Level - 1];
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }
    }
}
