using System.Drawing;
using System.Windows.Forms;

namespace SocietiesMS.Forms
{
    public static class UIStyle
    {
        // FAST University Official Colors
        public static readonly Color Navy = Color.FromArgb(0, 32, 96);
        public static readonly Color Blue = Color.FromArgb(0, 102, 204);
        public static readonly Color LightBlue = Color.FromArgb(173, 216, 230);
        public static readonly Color Gray = Color.FromArgb(128, 128, 128);
        public static readonly Color LightGray = Color.FromArgb(245, 245, 245);
        public static readonly Color White = Color.White;
        public static readonly Color Green = Color.FromArgb(0, 128, 0);
        public static readonly Color Red = Color.FromArgb(220, 53, 69);
        public static readonly Color Orange = Color.FromArgb(255, 140, 0);
        public static readonly Color DarkGray = Color.FromArgb(64, 64, 64);
        public static readonly Color AccentBlue = Color.FromArgb(0, 120, 215); // Lighter blue from logo
        public static readonly Color Border = Color.FromArgb(224, 224, 224); // Light border color

        // Fonts
        public static readonly Font TitleFont = new Font("Segoe UI", 22, FontStyle.Bold);
        public static readonly Font HeaderFont = new Font("Segoe UI", 12, FontStyle.Bold);
        public static readonly Font TextFont = new Font("Segoe UI", 11);
        public static readonly Font SmallFont = new Font("Segoe UI", 9);

        public static TextBox CreateModernTextBox(string placeholder, bool isPassword = false)
        {
            var txt = new TextBox
            {
                Font = TextFont,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Gray,
                Text = placeholder,
                Tag = placeholder // Store placeholder in Tag
            };

            txt.Enter += (s, e) => {
                if (txt.Text == placeholder) {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                    if (isPassword) txt.UseSystemPasswordChar = true;
                }
            };

            txt.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txt.Text)) {
                    txt.Text = placeholder;
                    txt.ForeColor = Gray;
                    if (isPassword) txt.UseSystemPasswordChar = false;
                }
            };

            return txt;
        }

        public static Button CreateModernButton(string text, Color backColor, Color foreColor, bool outlined = false)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = outlined ? Color.White : backColor,
                ForeColor = outlined ? backColor : foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = HeaderFont,
                Cursor = Cursors.Hand,
                Size = new Size(350, 45)
            };
            btn.FlatAppearance.BorderSize = outlined ? 1 : 0;
            if (outlined) btn.FlatAppearance.BorderColor = backColor;
            return btn;
        }

        public static void ApplyModernStyle(Control control)
        {
            control.Font = TextFont;
            if (control is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Blue;
                btn.ForeColor = White;
                btn.Cursor = Cursors.Hand;
            }
            else if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is Label lbl && lbl.Name == "lblTitle")
            {
                lbl.Font = TitleFont;
                lbl.ForeColor = Navy;
            }

            foreach (Control child in control.Controls)
            {
                ApplyModernStyle(child);
            }
        }
    }
}
