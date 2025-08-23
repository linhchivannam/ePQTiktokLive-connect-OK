using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ePQTiktokLive.LIVE
{
    public partial class Form1 : Form
    {
        DataGridView dataGridView1;
        public Form1()
        {
            InitializeComponent();
             dataGridView1 = new DataGridView();
            // Tạo DataGridView
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(dataGridView1);

            // Thêm cột
            dataGridView1.Columns.Add("payloadType", "PayloadType");
            dataGridView1.Columns.Add("seqId", "SeqId");
            dataGridView1.Columns.Add("roomId", "RoomId");
            dataGridView1.Columns.Add("msgType", "MsgType");
            dataGridView1.Columns.Add("serverTime", "ServerFetchTime");
            dataGridView1.Columns.Add("pushTime", "PushTime");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "ePQsendFrames.txt"; // file log bạn lưu

            foreach (string line in File.ReadLines(path))
            {
                try
                {
                    var obj = JObject.Parse(line);
                    string payloadType = obj.Value<string>("payloadType");
                    string encoding = obj.Value<string>("payloadEncoding");
                    string payloadB64 = obj.Value<string>("payload");

                    string seqId = "", roomId = "", msgType = "", serverTime = "", pushTime = "";

                    if (!string.IsNullOrEmpty(payloadB64))
                    {
                        byte[] raw = Convert.FromBase64String(payloadB64);

                        if (encoding == "pb")
                        {
                            // Nhiều khi pb thực ra chứa JSON (ACK, EN)
                            string decoded = Encoding.UTF8.GetString(raw);

                            if (decoded.TrimStart().StartsWith("{"))
                            {
                                // Parse JSON
                                var inner = JObject.Parse(decoded);
                                seqId = inner.Value<string>("seq_id") ?? "";
                                roomId = inner.Value<string>("room_id") ?? "";
                                msgType = inner.Value<string>("msg_type") ?? "";
                                serverTime = inner.Value<string>("server_fetch_time") ?? "";
                                pushTime = inner.Value<string>("push_time") ?? "";
                            }
                            else
                            {
                                // Thực sự là Protobuf -> TODO: gọi parser đã sinh từ .proto
                                // var frame = WebcastResponse.Parser.ParseFrom(raw);
                                // ...
                                msgType = "(protobuf)";
                                msgType = decoded;
                            }
                        }
                        else if (encoding == "json")
                        {
                            var inner = JObject.Parse(Encoding.UTF8.GetString(raw));
                            seqId = inner.Value<string>("seq_id") ?? "";
                            roomId = inner.Value<string>("room_id") ?? "";
                        }
                    }

                    dataGridView1.Rows.Add(payloadType, seqId, roomId, msgType, serverTime, pushTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi dòng: " + ex.Message);
                }
            }
        }
    }
}
