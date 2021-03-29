using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNACalc {
    public partial class PasteHookedTextBox : System.Windows.Forms.TextBox {
        public PasteHookedTextBox() {
        }

        public event EventHandler<EventArgs> Pasted;

        private const int WM_PASTE = 0x0302;
        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_PASTE) {
                var evt = Pasted;
                if (evt != null) {
                    evt(this, new EventArgs());
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
