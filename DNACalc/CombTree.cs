using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc {
    // 非飽和コンビネーションのキャッシュ
    public class CombTree {
        class CombNode {
            public Dictionary<byte, CombNode> children = null;

            public CombNode(bool isLeaf) {
                if(!isLeaf) children = new Dictionary<byte, CombNode>();
            }
        }

        Dictionary<byte, CombNode> roots;

        public CombTree() {
            roots = new Dictionary<byte, CombNode>();
        }

        public void AddCombination(byte[] comb) {
            Dictionary<byte, CombNode> nodes = roots;
            for (int i = 0; i < comb.Length; i++) {
                if (!nodes.TryGetValue(comb[i], out var node)) {
                    nodes[comb[i]] = node = new CombNode(i == comb.Length - 1);
                } else if (node.children == null) {
                    // リーフなのでやめる
                    return;
                }
                nodes = node.children;
            }
        }

        public int Search(byte[] comb) {
            Dictionary<byte, CombNode> nodes = roots;
            int i;
            for (i = 0; i < comb.Length; i++) {
                if (nodes == null) {
                    // ここがリーフ
                    return i;
                }
                if (!nodes.TryGetValue(comb[i], out var node)) {
                    // 飽和するかわからん
                    return 0;
                }
                nodes = node.children;
            }
            // ここには来ない？
            throw new NotImplementedException();
        }
    }
}
