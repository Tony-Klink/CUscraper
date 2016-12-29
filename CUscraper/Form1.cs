using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUscraper
{
    public partial class Form1 : Form
    {
        DataTable table;


        public Form1()
        {
            InitializeComponent();
        }

        //Initialize table and fill columns
        private void InitTable()
        {
            table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("url", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("image url", typeof(string));
            table.Columns.Add("Price", typeof(string));
            ShopDataTable.DataSource = table;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            InitTable();
        }

        private void ShopDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        private async void scrapeButton_Click(object sender, EventArgs e)
        {
            Parser a = new Parser();
            table.Clear();
            try
            {
                var data = await a.GetItemsFromPage(ToInteger(textBox1.Text), ToInteger(textBox2.Text));                
                foreach (var item in data)
                {
                    table.Rows.Add(item.Name, item.URL, item.Description, item.Image, item.Price);
                }
            }
            catch (NullReferenceException err)
            {
                MessageBox.Show("Page doesn't exist!\n" + err.ToString(), "Null reference error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Utility method; returns converted integer
        /// </summary>
        /// <param name="str">input string</param>
        static int ToInteger(string str)
        {
            int x = 0;
            try
            {
                x = Convert.ToInt32(str);
            }
            catch (OverflowException err)
            {
                MessageBox.Show("Wrong type of input!\n" + err.ToString(), "Null reference error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return x;
        }
    }
}