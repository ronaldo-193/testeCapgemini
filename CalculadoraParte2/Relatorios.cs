using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculadoraParte2
{
    public partial class Relatorios : Form
    {
        public Relatorios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cliente = textBox1.Text;
            string DataInicio = maskedTextBox1.Text;
            string DataFim = maskedTextBox2.Text;

            //monta query

            string query = "Select * From Anuncios where 1=1 ";

            if (!String.IsNullOrEmpty(Cliente))
            {
                query += " And upper(NomeCliente) like '%"+Cliente.ToUpper()+"%'	";
            }
            if (DataInicio != "  /  /")
            {
                query += " AND DataInicio >= '"+ DataInicio + "'";
            }
            if (DataFim != "  /  /")
            {
                query += " AND DataFim >= '" + DataFim + "'";
            }

            CarregaDados(query);
        }

        private void CarregaDados(string query)
        {
            try
            {
                dataGridView1.DataSource = DAL.ExecuteDataTable(query, CommandType.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Row = dataGridView1.CurrentRow;

            string teste = Row.Cells[0].Value.ToString();
            double valorInvestimento = Convert.ToDouble(Row.Cells[5].Value);
            //30 pessoas visualizam o anúncio original (não compartilhado).
            double qtd_alcance = valorInvestimento * 30;
            //A cada 100 pessoas que visualizam o anúncio 12 clicam nele
            double max_click = qtd_alcance * 0.12;
            //a cada 20 pessoas que clicam no anúncio 3 compartilham nas redes sociais.
            double qtd_compartilhamentos = (qtd_alcance / 20) * 3 * 4;
            //•	cada compartilhamento nas redes sociais gera 40 novas visualizações
            double max_compartilhamentos = qtd_compartilhamentos * 40;
            //quantidade máxima de pessoas que visualizarão o mesmo 
            double maximo_visualizacao = max_compartilhamentos + 160;

            label9.Text = "R$ " + valorInvestimento.ToString();
            label10.Text = maximo_visualizacao.ToString();
            label11.Text = max_click.ToString();
            label12.Text = max_compartilhamentos.ToString();

            panel1.Visible = true;

        }

    }
}
