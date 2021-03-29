using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class Combinator {
        public byte[] combination;
        private int elements;

        public Combinator(int elements, int pick) {
            combination = new byte[pick];
            this.elements = elements;

            for (int i = 0; i < pick; i++) {
                combination[i] = (byte)i;
            }
        }

        public bool Next() {
            return Incr(combination.Length - 1, elements - 1);
        }

        public BigInteger SkipThis(byte[] comb, int len) {
            // スキップカウントを計算
            BigInteger skipCount = Util.nCr(elements - combination[len - 1] - 1, combination.Length - len) - CalcPos(comb, len);

            for (int i = 0; i < combination.Length; i++) {
                if(i < len) combination[i] = comb[i];
                else combination[i] = (byte)(elements - (combination.Length - i));
            }

            return skipCount;
        }

        private BigInteger CalcPos(byte[] comb, int start) {
            if (start == comb.Length - 1) {
                return comb[start] - comb[start - 1] - 1;
            }

            BigInteger result = CalcPos(comb, start + 1);
            for (int i = comb[start - 1] + 1; i < comb[start]; i++) {
                result += Util.nCr(elements - i - 1, combination.Length - start - 1);
            }

            return result;
        }

        private bool Incr(int pos, int max) {
            if (combination[pos] < max) {
                combination[pos]++;
            } else {
                if(pos == 0) return false;
                if(!Incr(pos - 1, max - 1)) return false;
                combination[pos] = (byte)(combination[pos - 1] + 1);
            }
            return true;
        }
    }
}
