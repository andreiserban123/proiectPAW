﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProiectPaw {
    public partial class FormMain : Form {

        Utilizator u1;
        List<Grup> grupuri;
        public FormMain() {
            InitializeComponent();
            u1 = new Utilizator("Andrei", "andyspeed2003@gmail.com",
                Utils.ComputeSHA256Hash("pisica123"), "5031009467119", new System.DateTime(2003, 06, 02));
            grupuri = new List<Grup>();
            Grup grup = new Grup("Admin");
            grupuri.Add(grup);
            grupuri[0].drepturi.Add("Read");
            grupuri[0].drepturi.Add("Update");
            grupuri[0].drepturi.Add("Delete");
            grupuri[0].listaUtilizatori.Add(u1);
            ListViewItem lvItem = new ListViewItem(u1.Nume);
            lvItem.SubItems.Add(u1.CNP);
            lvItem.SubItems.Add(u1.Email);
            lvItem.SubItems.Add(u1.Password);
            lvItem.SubItems.Add(u1.DataNastere.ToString());
            lvItem.Tag = u1;
            lvUtilizatori.Items.Add(lvItem);
        }

        private void lvUtilizatori_SelectedIndexChanged(object sender, EventArgs e) {
            if (lvUtilizatori.SelectedItems.Count > 0) {
                tbSelectedUtilizator.Text = "";
                ListViewItem lv = lvUtilizatori.SelectedItems[0];
                tbSelectedUtilizator.Text += lv.Text + "," + lv.SubItems[1].Text + "," + lv.SubItems[2].Text +
                                              "," + lv.SubItems[3].Text + "," + lv.SubItems[4].Text;
                Utilizator u = lv.Tag as Utilizator;

                // Clear previous items in lvGrupuri
                lvGrupuri.Items.Clear();

                foreach (Grup grup in grupuri) {
                    foreach (Utilizator utilizator in grup.listaUtilizatori) {
                        if (utilizator == u) {
                            ListViewItem lvGrup = new ListViewItem(grup.Nume);
                            if (grup.drepturi.Count > 0) {
                                lvGrup.SubItems.Add(grup.drepturi[0]);
                            }
                            lvGrupuri.Items.Add(lvGrup);
                            for (int i = 1; i < grup.drepturi.Count; i++) {
                                lvGrup = new ListViewItem("");
                                lvGrup.SubItems.Add(grup.drepturi[i]);
                                lvGrup.Tag = u;
                                lvGrupuri.Items.Add(lvGrup);
                            }
                        }
                    }
                }
            }
        }


        private void adaugaToolStripMenuItem_Click(object sender, System.EventArgs e) {
            Utilizator u = null;
            FormUtilizator form = new FormUtilizator(u);
            if (form.ShowDialog() == DialogResult.OK) {
                u = form.uFormUtilizator;
                ListViewItem lvItem = new ListViewItem(u.Nume);
                lvItem.SubItems.Add(u.CNP);
                lvItem.SubItems.Add(u.Email);
                lvItem.SubItems.Add(u.Password);
                lvItem.SubItems.Add(u.DataNastere.ToString());
                lvItem.Tag = u;
                lvUtilizatori.Items.Add(lvItem);
            }
        }

        private void modificaToolStripMenuItem_Click(object sender, System.EventArgs e) {
            if (lvUtilizatori.SelectedItems.Count > 0) {
                ListViewItem lv = lvUtilizatori.SelectedItems[0];
                Utilizator u = (Utilizator)lv.Tag;
                FormUtilizator form = new FormUtilizator(u);
                if (form.ShowDialog() == DialogResult.OK) {
                    lv.Text = u.Nume;
                    lv.SubItems[1].Text = u.CNP;
                    lv.SubItems[2].Text = u.Email;
                    lv.SubItems[3].Text = u.Password;
                    lv.SubItems[4].Text = u.DataNastere.ToString();
                }
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, System.EventArgs e) {
            if (lvUtilizatori.SelectedItems.Count > 0) {
                if (MessageBox.Show("Sigur doriti sa stergeti utilizatorul?", "Stergere", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    lvUtilizatori.Items.Remove(lvUtilizatori.SelectedItems[0]);
            }
        }
    }
}
