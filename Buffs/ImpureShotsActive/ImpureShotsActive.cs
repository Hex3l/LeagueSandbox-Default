using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;


namespace ImpureShotsActive
{
    internal class ImpureShotsActive : IBuffGameScript
    {
        private StatsModifier _AttackSpeedMod;
        private ObjAiBase _owningUnit;
        private Spell _owningSpell;

        public void OnActivate(ObjAiBase buffOwner, Spell ownerSpell)
        {
            _owningSpell = ownerSpell;
            _owningUnit = buffOwner;
            _AttackSpeedMod = new StatsModifier();
            _AttackSpeedMod.AttackSpeed.PercentBonus = (new float[] { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f })[ownerSpell.Level - 1];
            buffOwner.AddStatModifier(_AttackSpeedMod);
            ApiEventManager.OnHitUnit.AddListener(this, buffOwner, OnAutoAttack);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
            unit.RemoveStatModifier(_AttackSpeedMod);
        }

        private void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
            if(target is ObjAiBase)
            {
                ((ObjAiBase)target).AddBuffGameScript("GrievousWounds", "GrievousWounds", _owningSpell, 2.0f, true);
            }
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
