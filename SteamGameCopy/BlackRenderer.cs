using System.Drawing;
using System.Windows.Forms;

namespace SteamGameCopy
    {
    public class BlackRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color c = Color.Black;
            using (SolidBrush brush = new SolidBrush(c))
                e.Graphics.FillRectangle(brush, rc);
        }
    }
}