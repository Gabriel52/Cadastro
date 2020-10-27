using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Banco_de_dados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        RichTextBox rbt = new RichTextBox();
        int nr;
        string[] campo;
        private void Limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            consulta.Clear();
            listBox1.Items.Clear();

            
        }
        private void Alterar(int nr, string nome, string ender, string fone, string flag)
        {
            string linhanew = nome + "|" + ender + "|" + fone + "|" + flag;
            string oldlinha = rbt.Lines[nr];
            rbt.Text = rbt.Text.Replace(oldlinha, linhanew);
            rbt.SaveFile("clientes.cly", RichTextBoxStreamType.PlainText);
            rbt.LoadFile("clientes.cly", RichTextBoxStreamType.PlainText);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                nr = int.Parse(consulta.Text);
                campo = rbt.Lines[nr].Split('|');
                if (campo[3] == "D")
                {
                    MessageBox.Show("Registro deletado", "Erro ao procurar Registro");
                    Limpar();
                }
                else
                {
                    textBox3.Text = campo[0];
                    textBox2.Text = campo[1];
                    textBox1.Text = campo[2];
                    label5.Text = nr.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Digite algo no campo de consulta", "Erro de Burrice");
            }
            
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (label5.Text == consulta.Text)
                {
                    Alterar(nr, textBox3.Text, textBox2.Text, textBox1.Text, "A");

                }
                else
                {
                    rbt.Text += "\n" + textBox3.Text + "|" + textBox2.Text + "|" + textBox1.Text + "|" + "A";
                    rbt.SaveFile("clientes.cly", RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Salvo com sucesso ");
                   
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Campo vazio, verifique e tente novamente", "Erro");
                textBox1.Focus();
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpar();
            label5.Text = rbt.Lines.Length.ToString();
            rbt.LoadFile("clientes.cly", RichTextBoxStreamType.PlainText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                rbt.LoadFile("clientes.cly", RichTextBoxStreamType.PlainText);
                label5.Text = rbt.Lines.Length.ToString();
              
            }
            catch (IOException IOex)
            {
                rbt.SaveFile("clientes.cly", RichTextBoxStreamType.PlainText);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != (char) 8) && (e.KeyChar!= (char) 127) && (e.KeyChar!='-')
                && (e.KeyChar!='(') && (e.KeyChar != ')'))
            {
                e.KeyChar = (char)0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
                if (nr.ToString() == label5.Text)
                {
                    if (MessageBox.Show("Deseja excluir esse registro", "Atenção", MessageBoxButtons.OKCancel) == DialogResult.OK)
                     {
                        Alterar(nr, textBox3.Text, textBox2.Text, textBox1.Text, "D");
                    MessageBox.Show("Resgistro deletado com sucesso", "DLETADO");
                    Limpar();

                }
                }
                else
                {
                MessageBox.Show("Escolha um registro para excluir", "Resgitro não selecionado");
                }
     
          
        }

        int i;
        
        string[] letras;

        private void consultaN_KeyUp(object sender, KeyEventArgs e)
        {
            Limpar();
            for (i = 1; i < rbt.Lines.Length; i++)
            {

                letras = rbt.Lines[i].Split('|');

                if (letras[0].Substring(0, consultaN.Text.Length).ToUpper() == consultaN.Text.ToUpper())
                {

                    listBox1.Items.Add(letras[0].Substring(0));

                    
                }

            }
        }

    

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string nome = listBox1.SelectedItem.ToString(); //listBox1.Items[it].ToString();

            for (i = 1; i < rbt.Lines.Length; i++)
            {


                letras = rbt.Lines[i].Split('|');


                if (letras[0].ToUpper() == nome.ToUpper())
                {
                    textBox3.Text = letras[0];
                    textBox2.Text = letras[1];
                    textBox1.Text = letras[2];
                    label5.Text = i.ToString();

                }
            }
        }
    }
}
