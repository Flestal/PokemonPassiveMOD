using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579007:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "모두에게 적용, 주사위가 깨져 있을 시 깨진 주사위의 비율에 따라 최대 체력의 12.5%까지 피해";
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
        public override void OnRoundEnd()
        {
            List<BattleUnitModel> alivelist = BattleObjectManager.instance.GetAliveList();
            foreach(BattleUnitModel target in alivelist)
            {
                int breakedcount = 0;
                foreach(SpeedDice dice in target.speedDiceResult)
                {
                    if (dice.breaked)
                    {
                        breakedcount++;
                    }
                }
                target.TakeDamage(Convert.ToInt32(target.MaxHp*0.125*breakedcount/target.speedDiceResult.Count));
                target.TakeBreakDamage(Convert.ToInt32(target.MaxBreakLife * 0.125 * breakedcount / target.speedDiceResult.Count));
            }
        }
    }
}
