using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579005:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "일방 공격을 받을 시 상대에게 피해의 20%만큼 반사";
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
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (!atkDice.IsParrying())
            {
                atkDice.owner.TakeDamage(Convert.ToInt32(dmg*0.2f),base.owner);
            }
        }
    }
}
