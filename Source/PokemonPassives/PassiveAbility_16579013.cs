using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579013:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 처음 받은 피해의 방어 수준이 내성이 됨";
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
            this.owner.Book.SetOriginalResists();
            isUsed = false;
        }
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (!isUsed)
            {
                //atkDice.Detail
                this.owner.Book.SetResistHP(atkDice.Detail, AtkResist.Resist);
                this.owner.Book.SetResistBP(atkDice.Detail, AtkResist.Resist);
                isUsed = true;
            }
        }
        private bool isUsed;
    }
}
