using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc {
    // 非飽和コンビネーションのキャッシュ
    public class CombTree {
        class CombNode {
            public Dictionary<byte, CombNode> children = new Dictionary<byte, CombNode>();
        }

        Dictionary<byte, CombNode> roots;

        public CombTree() {
            roots = new Dictionary<byte, CombNode>();
        }

        public void AddCombination(byte[] comb) {
            Dictionary<byte, CombNode> nodes = roots;
            for (int i = 0; i < comb.Length; i++) {
                if (!nodes.TryGetValue(comb[i], out var node)) {
                    if (i == comb.Length - 1) {
                        // リーフノードを追加
                        nodes[comb[i]] = node = new CombNode();
                    } else {
                        // リーフでない場合はすでに消されているので無視
                        break;
                    }
                }
                nodes = node.children;
            }
        }

        public int Search(byte[] comb) {
            Dictionary<byte, CombNode> nodes = roots;
            int i;
            for (i = 0; i < comb.Length - 1; i++) {
                if (!nodes.TryGetValue(comb[i], out var node)) {
                    break;
                } else {
                    nodes = node.children;
                }
            }
            return i + 1;
        }
    }
}
