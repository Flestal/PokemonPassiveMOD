using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579028:PassiveAbilityBase
    {
        public override bool isInvincible
        {
            get
            {
                return invincible;
            }
        }
        public override string debugDesc
        {
            get
            {
                return "매 막마다 1번 피해를 받지 않음";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
        }
        public override void OnRoundStart()
        {
            invincible = true;
        }
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (invincible)
            {
                invincible = false;
            }
        }
        private bool invincible;
    }
}
