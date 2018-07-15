using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;

namespace EveFrenzy
{
    class EveFrenzy : IBuffGameScript
    {
        private StatsModifier _statMod;
        private float _currentStatMod;
        private ObjAiBase _ownerUnit;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _ownerUnit = unit;
            _currentStatMod = (new float[] { 0.30f, 0.40f, 0.50f, 0.60f, 0.70f })[ownerSpell.Level - 1];
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = _currentStatMod;
            _ownerUnit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("EveFrenzy", 5.0f, 5, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
