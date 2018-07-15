using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;

namespace EveRavage
{
    class EveRavage : IBuffGameScript
    {
        private StatsModifier _statMod;
        private ObjAiBase _ownerUnit;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _ownerUnit = unit;
            _statMod = new StatsModifier();
            _statMod.AttackSpeed.PercentBonus = (new float[] { .6f, .75f, .90f, 1.05f, 1.2f })[ownerSpell.Level - 1];
            _ownerUnit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
