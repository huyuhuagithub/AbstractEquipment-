using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseModule.Helper
{
    class Other
    {
        /// <summary>
        /// Window Form 更新UI不会存在线程问题。
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        public static void updateTextBoxUI(TextBox textBox, string text)
        {
            textBox.Invoke(new Action(() => { textBox.Text = text; }));
        }
    }
}
