using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AndroidDataExtractionUsingADB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
        private string Escape(string input)
        {
            
            return input.Replace("'", "''") ?? "";
            
        }
        private void InsertToDB(string query)
        {
            SqlConnection conn = new SqlConnection(connstring);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private string ExtractValue(string line, string key)
        {
            var match = Regex.Match(line, $"{Regex.Escape(key)}(.*?)((\\s\\w+=)|$)");
            if (match.Success)
                return match.Groups[1].Value.Trim().Replace("'", "");
            return null;
        }

        private string ExtractAccountName(string line)
        {
            int start = line.IndexOf("{") + 1;
            int end = line.IndexOf(",", start);
            return line.Substring(start, end - start).Trim();
        }

        private string RunADBCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = command,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string output = RunADBCommand("shell content query --uri content://contacts/phones/ --projection display_name:number");
            MessageBox.Show(output, "Contacts");
            dataGridView1.Text = output;

            foreach (string line in output.Split('\n'))
            {
                string name = ExtractValue(line, "display_name=");
                string phone = ExtractValue(line, "number=");
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone))
                {
                    InsertToDB($"INSERT INTO contactstable (Name, PhoneNumber, Email) VALUES ('{Escape(name)}', '{Escape(phone)}', '')");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string output = RunADBCommand("shell content query --uri content://sms/ --projection address:date:body");
            textBox1.Text = output;
            MessageBox.Show(output, "SMS Messages");
            dataGridView1.Text = output;

            foreach (string line in output.Split('\n'))
            {
                string senderAddr = ExtractValue(line, "address=");
                string body = ExtractValue(line, "body=");
                string date = ExtractValue(line, "date=");

                if (!string.IsNullOrEmpty(senderAddr) && !string.IsNullOrEmpty(body))
                {
                    InsertToDB($"INSERT INTO messagestable (Sender, Message, Timestamp) VALUES ('{Escape(senderAddr)}', '{Escape(body)}', FROM_UNIXTIME({date}/1000))");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string output = RunADBCommand("shell content query --uri content://call_log/calls/ --projection number:type:duration");
            textBox1.Text = output;
            MessageBox.Show(output, "Call Logs");
            dataGridView1.Text = output;

            foreach (string line in output.Split('\n'))
            {
                string number = ExtractValue(line, "number=");
                string type = ExtractValue(line, "type=");
                string duration = ExtractValue(line, "duration=");
                string callType;
                switch (type)
                {
                    case "1":
                        callType = "Incoming";
                        break;
                    case "2":
                        callType = "Outgoing";
                        break;
                    case "3":
                        callType = "Missed";
                        break;
                    default:
                        callType = "Unknown";
                        break;
                }

                if (!string.IsNullOrEmpty(number))
                {
                    InsertToDB($"INSERT INTO calllogstable (Number, CallType, Duration) VALUES ('{Escape(number)}', '{callType,20}', {duration ?? "0"})");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string cpuInfo = RunADBCommand("shell cat /proc/cpuinfo --limit 50");
            string memInfo = RunADBCommand("shell cat /proc/meminfo ");
            MessageBox.Show(cpuInfo + "\n\n" + memInfo, "CPU & Memory Info");
            dataGridView1.Text = cpuInfo + "\n\n" + memInfo;

            InsertToDB($"INSERT INTO cpuinfotable (CPUInfo, MemoryInfo) VALUES ('{Escape(cpuInfo)}', '{Escape(memInfo)}')");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = RunADBCommand("shell dumpsys account");
            MessageBox.Show(output, "Login Accounts");
            dataGridView1.Text = output;

            foreach (string line in output.Split('\n'))
            {
                if (line.Contains("Account {"))
                {
                    string account = ExtractAccountName(line);
                    string email = account.Contains("@") ? account : "";
                    InsertToDB($"INSERT INTO loginaccountstable (AccountName, EmailAddress) VALUES ('{Escape(account)}', '{Escape(email)}')");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string model = RunADBCommand("shell getprop ro.product.model");
            string androidVersion = RunADBCommand("shell getprop ro.build.version.release");
            string serialNumber = RunADBCommand("shell getprop ro.serialno");
            string device = RunADBCommand("shell getprop ro.product.device");
            string result = $"Model: {model}\n" +
                           $"Android Version: {androidVersion}\n\n" +
                            $"Serial Number: {serialNumber}\n" +
                            $"Device: {device}";
            MessageBox.Show(result, "Device Info");
            dataGridView1.Text = result;

            InsertToDB($"INSERT INTO devicedetailsTable (Model, AndroidVersion, SerialNumber, Device) VALUES ('{Escape(model)}', '{Escape(androidVersion)}', '{Escape(serialNumber)}', '{Escape(device)}')");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
