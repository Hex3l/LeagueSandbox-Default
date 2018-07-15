using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Packets.PacketDefinitions.S2C;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;

namespace EveInvis
{
    internal class EveInvis : IBuffGameScript
    {
        private double _currentTime;
        private double _lastUpdate;
        private ObjAiBase _ownerUnit;
        private Buff _visualBuff;
        
        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _currentTime = 0;
            _lastUpdate = 0;
            _ownerUnit = unit;
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("EveInvis", 5.0f, 5, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            if (_currentTime >= (_lastUpdate + 1000))
            {
                var manaRegenerated = _ownerUnit.Stats.ManaPoints.Total / 100;
                var maxMana = _ownerUnit.Stats.ManaPoints.Total;
                var newMana = _ownerUnit.Stats.CurrentMana + manaRegenerated;
                if (maxMana >= newMana)
                {
                    _ownerUnit.Stats.CurrentMana = newMana;
                }
                else
                {
                    _ownerUnit.Stats.CurrentMana = maxMana;
                }

                _lastUpdate = _currentTime;
            }
        }
    }
}
