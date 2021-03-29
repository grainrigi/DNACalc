using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DNACalc {
    public class Samples {
        private ulong[] data;
        private int length;
        private ulong accFull;
        private CombTree tree = new CombTree();

        public int Width { get => length; }
        public int Height { get => data.Length; }

        public Samples(bool[][] vs) {
            // サイズを決定
            var x = vs.Select(v => v.Length).Max();
            var y = vs.Length;

            if(x > 64) throw new Exception("64項目以上のデータは使えません。");
            if(y > 255) throw new Exception("256サンプル以上のデータは使えません。");

            // 変換
            data = vs.Select(v => Util.bs2ulong(v, x)).ToArray();
            length = x;

            for (int i = 0; i < length; i++) {
                accFull |= (ulong)1 << i;
            }
        }

        public BigInteger Calc(int pick) {
            Combinator c = new Combinator(data.Length, pick);
            BigInteger result = 0;

            do {
                ulong acc = 0;

                // キャッシュを参照
                int searchLen = tree.Search(c.combination);
                if (searchLen > 0) {
                    result += c.SkipThis(c.combination, searchLen);
                    continue;
                }

                // 補完するか計算
                foreach(var index in c.combination) {
                    acc |= data[index];
                }
                if(acc == accFull) {
                    result++;
                    if (pick < 6) tree.AddCombination(c.combination);
                }
            } while(c.Next());

            return result;
        }
    }
}
