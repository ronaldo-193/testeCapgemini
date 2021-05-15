using System;
using System.Data;
using System.Windows.Forms;

namespace CalculadoraParte2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                string query = @"SELECT Id
                                            ,upper(NomeAnuncio)NomeAnucio
                                            ,upper(NomeCliente)NomeCliente
                                            ,DataInicio
                                            ,DataFim
                                            ,Investimento
                                        FROM dbo.Anuncios ";
                dataGridView1.DataSource = DAL.ExecuteDataTable(query, CommandType.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Anuncio = textBox1.Text;
                string Cliente = textBox2.Text;
                string DataInicio = maskedTextBox1.Text;
                string DataFim = maskedTextBox2.Text;
                string Investimento = textBox5.Text;

                if(!String.IsNullOrEmpty(Anuncio) && DataFim !="__/__/____" &&
                    !String.IsNullOrEmpty(Cliente) && DataInicio != "__/__/____" &&
                    !String.IsNullOrEmpty(Investimento))
                {
                    DAL.Salvar(Anuncio, Cliente, DataInicio, DataFim, Investimento);

                    limparTextBoxes(this.Controls);
                    CarregaDados();
                    MessageBox.Show("Cadastro Inserido com sucesso!", "Sucesso!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Todos os campos são obrigatórios", "Valida campos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limparTextBoxes(this.Controls);           
        }

        private void limparTextBoxes(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = String.Empty;                   
                }
                //Se for maskBox
                if (ctrl is MaskedTextBox)
                {
                    ((MaskedTextBox)(ctrl)).Text = String.Empty;
                }
            }
        }
       
    }
}
