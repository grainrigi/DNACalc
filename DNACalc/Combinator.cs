using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class Combinator {
        public int[] combination;
        private int pick;
        private int elements;

        public Combinator(int elements, int pick) {
            combination = new int[pick];
            this.pick = pick;
            this.elements = elements;

            for (int i = 0; i < pick; i++) {
                combination[i] = i;
            }
        }

        public bool Next() {
            int i = 1;
            // 後退
            while (combination[pick - i] >= elements - i) {
                if(++i > pick) return false;
            }
            // 更新
            combination[pick - i]++;
            // 前進
            i = pick - i;
            while (++i < pick) {
                combination[i] = combination[i - 1] + 1;
            }

            return true;
        }

        public BigInteger SkipThis(int len) {
            // スキップカウントを計算
            BigInteger skipCount = Util.nCr(elements - combination[len - 1] - 1, combination.Length - len) - CalcPos(combination, len);

            for (int i = len; i < combination.Length; i++) {
                combination[i] = elements - (combination.Length - i);
            }

            return skipCount;
        }

        private BigInteger CalcPos(int[] comb, int start) {
            if (start == comb.Length - 1) {
                return comb[start] - comb[start - 1] - 1;
            }

            BigInteger result = CalcPos(comb, start + 1);
            for (int i = comb[start - 1] + 1; i < comb[start]; i++) {
                result += Util.nCr(elements - i - 1, combination.Length - start - 1);
            }

            return result;
        }
    }
}
