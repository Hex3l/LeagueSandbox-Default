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

namespace EveSpeed
{
    class EveSpeed : IBuffGameScript
    {
        private StatsModifier _statMod;
        private float _currentStatMod;
        private ObjAiBase _ownerUnit;
        private Buff _visualBuff;
        //private int _speedMultiplier = 0;
        private int _speedMultiplier = 1;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _ownerUnit = unit;
            _currentStatMod = (new float[] { 4.0f, 8.0f, 12.0f, 16.0f, 20.0f })[ownerSpell.Level - 1];
            _statMod = new StatsModifier();
            //TODO: Find a way to have evelynn's Q and R give speed stacks
            _statMod.MoveSpeed.FlatBonus = _currentStatMod * _speedMultiplier;
            _ownerUnit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("EveSpeed", 5.0f, 5, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
            _statMod.MoveSpeed.FlatBonus = _currentStatMod;
        }

        /*public void SpeedUp()
        {
            if(_speedMultiplier < 4)
            {
                _speedMultiplier++;
            }
            else
            {
                _speedMultiplier = 4;
            }
        }*/
    }
}
