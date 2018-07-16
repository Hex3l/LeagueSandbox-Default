using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;

namespace GrievousWounds
{
    internal class GrievousWounds : IBuffGameScript
    {
        private ObjAiBase _owningUnit;
        private Spell _owningSpell;
        private Particle _grievousParticle;
        private Buff _grievousBuff;
        private StatsModifier _statMod;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _owningUnit = unit;
            _owningSpell = ownerSpell;
            _statMod = new StatsModifier();
            _statMod.HealthRegeneration.PercentBonus = -0.5f;
            unit.AddStatModifier(_statMod);
            if (unit is Champion) // Add a visual buff if the effected unit is a Champion
            {
                _grievousParticle = ApiFunctionManager.AddParticleTarget((Champion)unit, "global_grievousWound_tar.troy", unit);
            }
            _grievousBuff = ApiFunctionManager.AddBuffHudVisual("GrievousWound", -1, 1, unit, -1);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            if (_grievousParticle != null)
            {
                ApiFunctionManager.RemoveParticle(_grievousParticle);
            }
            ApiFunctionManager.RemoveBuffHudVisual(_grievousBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
