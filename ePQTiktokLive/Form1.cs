using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace ePQTiktokLive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnet_Click(object sender, EventArgs e)
        {

            var ws = new WebSocket(txtWebSocketUrl.Text);
            ws.OnMessage += (se, t) =>
            {
                Console.WriteLine("Received: " + t.RawData.Length + " bytes");
                // hoặc giải mã protobuf tại đây
            };

            ws.OnOpen += (s, t) => Console.WriteLine("Connected");
            ws.OnError += (s, t) => Console.WriteLine("Error: " + t.Message);
            ws.OnClose += (s, t) => Console.WriteLine("Closed");

            ws.Connect();
        }
    }
}
