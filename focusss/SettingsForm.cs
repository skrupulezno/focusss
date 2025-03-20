using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace focusss
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            //картинка
            comboBox1.Items.AddRange(new string[] { "Смешарики", "Дота", "Животное" });
            //тыл
            comboBox2.Items.AddRange(new string[] { "Рисунок 1", "Рисунок 2", "Рисунок 3", "Рисунок 4", "Рисунок 5" });
            //фон
            comboBox3.Items.AddRange(new string[] { "Фон 1", "Фон 2", "Фон 3", "Фон 4", "Фон 5" });

            comboBox1.SelectedIndex = GameSettings.FrontSetIndex;
            comboBox2.SelectedIndex = GameSettings.BackImageIndex;
            comboBox3.SelectedIndex = GameSettings.BackgroundImageIndex;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameSettings.FrontSetIndex = comboBox1.SelectedIndex;
            GameSettings.BackImageIndex = comboBox2.SelectedIndex;
            GameSettings.BackgroundImageIndex = comboBox3.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
