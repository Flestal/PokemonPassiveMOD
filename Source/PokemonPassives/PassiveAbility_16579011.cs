using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579011:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "자신의 주사위 위력 +3, 피해량 x2, 매 무대마다 자신이 처음 사용한 전투 책장을 제외한 모든 전투 책장 소멸, 자신이 처음 사용한 전투 책장 8장을 추가";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            card = null;
        }
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (card == null)
            {
                card = curCard.card;
                this.owner.allyCardDetail.ExhaustAllCards();
                for(int i = 0; i < 9; i++)
                {
                    this.owner.allyCardDetail.AddNewCardToDeck(card.GetID());
                }
                this.owner.allyCardDetail.DrawCards(4);
            }
            
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = 3
            });
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            //base.BeforeGiveDamage(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmgRate = 100
            });
        }
        BattleDiceCardModel card;
    }
}
