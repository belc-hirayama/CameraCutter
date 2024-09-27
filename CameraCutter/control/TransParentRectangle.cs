using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCutter.control
{
    internal class TransParentRectangle : UserControl
    {
        public static List<Panel> GetRectanglePanels(Point location, Size size, int borderWidth, Color borderColor)
        {
            List<Panel> panelList = [];
            panelList.Add(new Panel() { BackColor = borderColor, Location = location, Width = size.Width, Height = borderWidth });
            panelList.Add(new Panel() { BackColor = borderColor, Location = location, Width = borderWidth, Height = size.Height });
            panelList.Add(new Panel() { BackColor = borderColor, Location = new Point(location.X-borderWidth + size.Width, location.Y), Width = borderWidth, Height = size.Height });
            panelList.Add(new Panel() { BackColor = borderColor, Location = new Point(location.X, location.Y - borderWidth + size.Height), Width = size.Width, Height = borderWidth });
            return panelList;
        }
    }
}
