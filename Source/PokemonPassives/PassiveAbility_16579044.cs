using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579044:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "부식을 거는 주사위의 내성 수준에 따라 부식을 1~3 추가로 부여함";
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            resist = AtkResist.None;
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            resist = behavior.card.target.Book.GetResistHP(behavior.Detail);
        }
        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (buf.bufType == KeywordBuf.Decay&&resist!=AtkResist.None)
            {
                switch (resist)
                {
                    case AtkResist.Immune:
                    case AtkResist.Resist:
                        return 3;
                    case AtkResist.Endure:
                    case AtkResist.Normal:
                        return 2;
                    case AtkResist.Vulnerable:
                    case AtkResist.Weak:
                        return 1;
                }
            }
            return 0;
        }
        private AtkResist resist;
    }
}
