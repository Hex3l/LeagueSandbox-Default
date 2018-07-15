using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace MFStrut
{
    internal class MFStrut : IBuffGameScript
    {
        private StatsModifier _statMod;
        private float _currentStatMod;
        private double _currentTime;
        private double _lastUpdate;
        private ObjAiBase _ownerUnit;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _currentTime = 0;
            _lastUpdate = 0;
            _currentStatMod = 25;
            _ownerUnit = unit;
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.FlatBonus = _currentStatMod;
            _ownerUnit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("MissFortuneStrutStacks", 5.0f, (int)_currentStatMod, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            if(_currentTime >= (_lastUpdate + 1000)) {
                if (_currentStatMod < 70)
                {
                    _currentStatMod += 8;
                    if (_currentStatMod > 70)
                    {
                        _currentStatMod = 70;
                    }
                    _ownerUnit.RemoveStatModifier(_statMod);
                    ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
                    _statMod.MoveSpeed.FlatBonus = _currentStatMod;
                    _lastUpdate = _currentTime;
                    _ownerUnit.AddStatModifier(_statMod);
                    _visualBuff = ApiFunctionManager.AddBuffHudVisual("MissFortuneStrutStacks", 5.0f, (int)_currentStatMod, _ownerUnit, -1);
                }
            }
        }
    }
}
