﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Media;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pomodoro {

    
    public partial class ToDo : Form {

        
        public List<string> todos { get; set; }
        public List<int> indeksi { get; set; }
        public List<string> checkedItems { get; set; }
        int total;
        int done;
        int tempDone;

        public ToDo(List<string> Todos, List<string> CheckedItems, Color back, Color font, Color panel) {

            InitializeComponent();

            this.BackColor = back;
            this.ForeColor = font;
            cblTodo.BackColor = panel;
            button1.BackColor = back;
            button2.BackColor = back;
            label1.ForeColor = font;
            

            total = 0;
            done = 0;
          
            checkedItems = new List<string>();
            todos = new List<string>();

            foreach(string task in Todos) {
                cblTodo.Items.Add(task);
            }

            foreach(string li in CheckedItems) {
                checkedItems.Add(li);
            }

            loadItems();
        }

        public ToDo() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Distraction distraction = new Distraction(this.BackColor, this.ForeColor, cblTodo.BackColor);
            if(distraction.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                cblTodo.Items.Add(distraction.DistractionClass.text);
            }
            loadItems();

        }

        private void countTotal() {
            total = 0;
            done = 0;
            foreach(Object o in cblTodo.Items) {
                total++;
            }
            foreach(Object o in cblTodo.CheckedItems) {
                done++;
            }
            lblTotalCounter.Text = total.ToString();
            lblDoneCounter.Text = done.ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
           
            if(cblTodo.SelectedIndex > -1)
            deleteAnItem(cblTodo.SelectedItem.ToString());
           
        }

        public void loadItems() {

            foreach(string str in todos) {
                if(str != null) {
                    cblTodo.Items.Add(str);
                }
            }

            foreach(string s in checkedItems) {
                cblTodo.SetItemChecked(cblTodo.Items.IndexOf(s), true);
            }

            countTotal();
        }

        public void deleteAnItem(string text) {


            List<string> obichnaPomoshnaLista = new List<string>();

            foreach(string s in cblTodo.Items) {
                if(!text.Equals(s)) {
                    obichnaPomoshnaLista.Add(s);
                }
            }

            foreach(string li in obichnaPomoshnaLista) {
                checkedItems.Add(li);
            }

            cblTodo.Items.Clear();

            foreach(string s in obichnaPomoshnaLista) {
                cblTodo.Items.Add(s);
            }

            foreach(string s in checkedItems) {
                if(cblTodo.Items.IndexOf(s) > -1)
                cblTodo.SetItemChecked(cblTodo.Items.IndexOf(s), true);
            }

            countTotal();
        }
        
        private void cblTodo_SelectedIndexChanged(object sender, EventArgs e) {
            int done = cblTodo.CheckedItems.Count;
            lblDoneCounter.Text = done.ToString();
            countTotal();
        }

        private void button3_Click(object sender, EventArgs e) {

            foreach(string s in cblTodo.Items) {
                todos.Add(s);
            }

            if(cblTodo.CheckedItems.Count != 0) {
                foreach(string s in cblTodo.CheckedItems) {
                    checkedItems.Add(s);
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
