using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using focusss;
using System.IO;

namespace focusss
{
    public partial class GameForm : Form
    {
        public class Card
        {
            public PictureBox PictureBox { get; set; }
            public int CardValue { get; set; }
            public bool IsFaceUp { get; set; }
            public bool IsMatched { get; set; }
        }

        private List<Card> cards = new List<Card>();
        private Card firstSelectedCard = null;
        private int wrongStreak = 0;
        private int round = 1;
        private bool isBusy = false; 

        public GameForm()
        {
            InitializeComponent();
            StartRound();
        }

        private void StartRound()
        {
            isBusy = false;
            lblRound.Text = $"Раунд: {round}";
            panelCards.Controls.Clear();
            cards.Clear();
            firstSelectedCard = null;
            wrongStreak = 0;
            label1.Text = $"Неправильных ходов: {wrongStreak}";

            int numCards = 10;
            if (round >= 4 && round <= 8)
                numCards = 16;
            else if (round >= 9)
                numCards = 28;

            int numPairs = numCards / 2;
            List<int> cardValues = new List<int>();
            for (int i = 1; i <= numPairs; i++)
            {
                cardValues.Add(i);
                cardValues.Add(i);
            }
            Random rnd = new Random();
            cardValues = cardValues.OrderBy(x => rnd.Next()).ToList();

            int cardWidth = 80;
            int cardHeight = 80;
            int margin = 10;
            int columns = (int)Math.Ceiling(Math.Sqrt(numCards));
            int rows = (int)Math.Ceiling((double)numCards / columns);

            for (int i = 0; i < numCards; i++)
            {
                Card card = new Card();
                PictureBox pb = new PictureBox();
                pb.Width = cardWidth;
                pb.Height = cardHeight;
                pb.BackColor = Color.Gray;
                pb.BorderStyle = BorderStyle.FixedSingle;
                int row = i / columns;
                int col = i % columns;
                pb.Left = margin + col * (cardWidth + margin);
                pb.Top = margin + row * (cardHeight + margin);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Cursor = Cursors.Hand;
                pb.Image = GenerateBackImage(GameSettings.BackImageIndex);
                pb.Click += Card_Click;
                pb.Tag = card;

                card.PictureBox = pb;
                card.CardValue = cardValues[i];
                card.IsFaceUp = false;
                card.IsMatched = false;
                cards.Add(card);
                panelCards.Controls.Add(pb);
            }

            this.BackgroundImage = GenerateBackgroundImage(GameSettings.BackgroundImageIndex);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private async void Card_Click(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            PictureBox pb = sender as PictureBox;
            Card card = pb.Tag as Card;

            if (card.IsFaceUp || card.IsMatched)
                return;

            FlipCard(card, true);

            if (firstSelectedCard == null)
            {
                firstSelectedCard = card;
            }
            else
            {
                if (firstSelectedCard.CardValue == card.CardValue)
                {
                    // Если пара угадана, сбрасываем счётчик неправильных ходов
                    card.IsMatched = true;
                    firstSelectedCard.IsMatched = true;
                    card.PictureBox.Enabled = false;
                    firstSelectedCard.PictureBox.Enabled = false;
                    firstSelectedCard = null;
                    wrongStreak = 0;
                    label1.Text = $"Неправильных ходов: {wrongStreak}";

                    if (cards.All(c => c.IsMatched))
                    {
                        MessageBox.Show("Раунд пройден!");
                        round++;
                        StartRound();
                    }
                }
                else
                {
                    wrongStreak++;
                    label1.Text = $"Неправильных ходов: {wrongStreak}";
                    isBusy = true; // Блокируем выбор карточек во время задержки
                    await Task.Delay(1000);

                    if (wrongStreak >= 3)
                    {
                        MessageBox.Show("Проигрыш! Игра сброшена.");
                        round = 1;
                        StartRound();
                        return;
                    }
                    FlipCard(card, false);
                    FlipCard(firstSelectedCard, false);
                    firstSelectedCard = null;
                    isBusy = false; // Разблокируем выбор карточек после задержки
                }
            }
        }

        private void FlipCard(Card card, bool faceUp)
        {
            if (faceUp)
            {
                card.PictureBox.Image = GenerateFrontImage(GameSettings.FrontSetIndex, card.CardValue);
                card.IsFaceUp = true;
            }
            else
            {
                card.PictureBox.Image = GenerateBackImage(GameSettings.BackImageIndex);
                card.IsFaceUp = false;
            }
        }

        private Image GenerateFrontImage(int frontSetIndex, int cardValue)
        {
            string fileName = $"front_{frontSetIndex}_{cardValue}.png";
            string filePath = Path.Combine(Application.StartupPath, fileName);
            if (File.Exists(filePath))
            {
                return Image.FromFile(filePath);
            }
            else
            {
                Bitmap bmp = new Bitmap(80, 80);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    Font font = new Font("Arial", 16, FontStyle.Bold);
                    SizeF textSize = g.MeasureString(cardValue.ToString(), font);
                    g.DrawString(cardValue.ToString(), font, Brushes.Black,
                        (bmp.Width - textSize.Width) / 2, (bmp.Height - textSize.Height) / 2);
                }
                return bmp;
            }
        }

        private Image GenerateBackImage(int backIndex)
        {
            string fileName = $"back_{backIndex}.png";
            string filePath = Path.Combine(Application.StartupPath, fileName);
            if (File.Exists(filePath))
            {
                return Image.FromFile(filePath);
            }
            else
            {
                Bitmap bmp = new Bitmap(80, 80);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.DarkGray);
                    Font font = new Font("Arial", 12, FontStyle.Italic);
                    SizeF textSize = g.MeasureString("Back", font);
                    g.DrawString("Back", font, Brushes.White,
                        (bmp.Width - textSize.Width) / 2, (bmp.Height - textSize.Height) / 2);
                }
                return bmp;
            }
        }
        private Image GenerateBackgroundImage(int bgIndex)
        {
            string fileName = $"background_{bgIndex}.png";
            string filePath = Path.Combine(Application.StartupPath, fileName);
            if (File.Exists(filePath))
            {
                Image img = Image.FromFile(filePath);
                Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
                }
                return bmp;
            }
            else
            {
                Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Beige);
                }
                return bmp;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    StartRound();
                }
            }
        }
    }

}
