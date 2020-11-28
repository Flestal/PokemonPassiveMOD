using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579031:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "막 종료 시 부식에 의한 피해, 피격 시 부식에 의한 추가 피해를 받지 않음";
            }
        }
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.Decay;
        }
    }
}
