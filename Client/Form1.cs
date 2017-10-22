using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        Socket server;
        Socket client;
        string input, stringData;
        byte[] data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ipep);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Unable to connect to server.");
                MessageBox.Show(ex.ToString());

                return;
            }

        }



        public void Recevice()
        {

            data = new byte[1024];

            if (data != null)
            {
                client.Receive(data);
                lbchat.Items.Add("Sever: " + Encoding.ASCII.GetString(data));
            }

        }
        public void Send()
        {
            if (txtmess.Text == "")
            {
                MessageBox.Show("Ban chua nhap thong tin can gui");
            }
            else
            {
                data = Encoding.ASCII.GetBytes(txtmess.Text);
                client.Send(data);
                lbchat.Items.Add("Client: " + txtmess.Text);
                txtmess.Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnsend_Click_1(object sender, EventArgs e)
        {
            Send();
            Recevice();

        }

        private void btnconnect_Click_1(object sender, EventArgs e)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            client.Connect(ipep);
            byte[] data = new byte[1024];
            client.Receive(data);
            lbchat.Items.Add("Server:" + Encoding.ASCII.GetString(data));
        }

        public void Disconnect()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
