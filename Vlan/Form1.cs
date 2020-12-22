using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlan.lib;
using System.Linq;
using System.Diagnostics;

namespace Vlan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProcessMemoryReader mreader = new ProcessMemoryReader();

        uint ViewAddress = 0xD72F54;
        uint ViewOffset1 = 0x2C;
        uint ViewOffset2 = 0x21C4;
        uint ViewOffset3 = 0x78;
        uint offset = 0;
        int bytesOut = 0;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process process = Process.GetProcessesByName("War3").ToList().FirstOrDefault();
            if (process != null)
            {

                mreader.ReadProcess = process;
                mreader.OpenProcess();

                offset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(ViewAddress + (uint)process.Modules[43].BaseAddress), 4, out bytesOut), 0);
                var offset1 = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(offset + ViewOffset1), 4, out bytesOut), 0);
                var offset2 = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(offset1 + ViewOffset2), 4, out bytesOut), 0);
                var wtf = offset2 + ViewOffset3;
                var ViewNum = BitConverter.ToSingle(mreader.ReadMemory((IntPtr)wtf, 4, out bytesOut), 0);
                byte[] bytesOfTheNumber = BitConverter.GetBytes(ViewNum);
                textBox1.Text = ViewNum.ToString("");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            Process process = Process.GetProcessesByName("War3").ToList().FirstOrDefault();
            if (process != null)
            {

                mreader.ReadProcess = process;
                mreader.OpenProcess();

                offset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(ViewAddress + (uint)process.Modules[43].BaseAddress), 4, out bytesOut), 0);
                var offset1 = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(offset + ViewOffset1), 4, out bytesOut), 0);
                var offset2 = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(offset1 + ViewOffset2), 4, out bytesOut), 0);
                var wtf = offset2 + ViewOffset3;

                float FViewInput = (float)double.Parse(textBox1.Text);
                mreader.WriteMemory((IntPtr)wtf, BitConverter.GetBytes(FViewInput), out bytesOut);
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                timer1.Enabled = false;
                timer2.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
                timer2.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://discord.gg/PDpH7R8");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "0";
            }
        }
    }
}
 