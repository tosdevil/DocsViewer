using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace DocsViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dbPath = "docs.db";

            if (!File.Exists(dbPath))
            {
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    string createTablesQuery = @"
                CREATE TABLE Documents (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    number INTEGER,
                    date TEXT NOT NULL,
                    total REAL NOT NULL DEFAULT 0,
                    note TEXT
                );

                CREATE TABLE ErrorLogs (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    error_message TEXT NOT NULL,
                    error_date TEXT NOT NULL
                );

                CREATE TABLE Specifications (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    docid INTEGER,
                    name TEXT NOT NULL,
                    total REAL NOT NULL DEFAULT 0,
                    FOREIGN KEY(docid) REFERENCES Documents(id) ON DELETE CASCADE
                );
            ";

                    using (SQLiteCommand command = new SQLiteCommand(createTablesQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Был создан файл docs.db.");
            }
            DbLoad();
        }

        void DbLoad()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand selectData = new SQLiteCommand("SELECT id, number, total FROM Documents;", connection))
                {
                    listBox_docs.Items.Clear();
                    using (SQLiteDataReader reader = selectData.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int docId = reader.GetInt32(0);
                            string docNumber = reader["number"].ToString();
                            decimal currentTotal = reader.GetDecimal(2);

                            decimal newTotal = CalculateTotalForDocument(docId, connection);

                            if (newTotal != currentTotal)
                            {
                                UpdateDocumentTotal(docId, newTotal, connection);
                            }

                            listBox_docs.Items.Add(docNumber);
                        }
                    }
                }
            }
        }

        decimal CalculateTotalForDocument(int docId, SQLiteConnection connection)
        {
            decimal total = 0;

            using (SQLiteCommand selectSpecs = new SQLiteCommand("SELECT total FROM Specifications WHERE docid = @docid;", connection))
            {
                selectSpecs.Parameters.AddWithValue("@docid", docId);
                using (SQLiteDataReader reader = selectSpecs.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        total += reader.GetDecimal(0);
                    }
                }
            }

            return total;
        }

        void UpdateDocumentTotal(int docId, decimal newTotal, SQLiteConnection connection)
        {
            using (SQLiteCommand updateTotal = new SQLiteCommand("UPDATE Documents SET total = @total WHERE id = @id;", connection))
            {
                updateTotal.Parameters.AddWithValue("@total", newTotal);
                updateTotal.Parameters.AddWithValue("@id", docId);
                updateTotal.ExecuteNonQuery();
            }
        }


        void SpecLoad(int doc_num)
        {
            int doc_id = 0;
            
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand searchid = new SQLiteCommand($"SELECT * FROM Documents WHERE number = {doc_num};", connection))
                {
                    using (SQLiteDataReader reader = searchid.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doc_id = reader.GetInt32(0);
                        }
                    }
                }

                using (SQLiteCommand selectData = new SQLiteCommand($"SELECT * FROM Specifications WHERE docid = {doc_id};", connection))
                {
                    listBox_specs.Items.Clear();
                    using (SQLiteDataReader reader = selectData.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            listBox_specs.Items.Add(reader["name"]);
                        }
                    }
                }
            }
        }

        void DeleteSpecs(int doc_num)
        {
            int doc_id = 0;

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand searchIdCommand = new SQLiteCommand("SELECT Id FROM Documents WHERE number = @doc_num", connection))
                {
                    searchIdCommand.Parameters.AddWithValue("@doc_num", doc_num);
                    using (SQLiteDataReader reader = searchIdCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doc_id = reader.GetInt32(0);
                        }
                    }
                }

                if (doc_id != 0)
                {
                    using (SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM Specifications WHERE docid = @doc_id", connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@doc_id", doc_id);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Удалено {rowsAffected} записей, связанных с документом номер {doc_num}.");
                        }
                        else
                        {
                            MessageBox.Show("Нет записей для удаления.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Документ с данным номером не найден.");
                }
            }
        }


        private void textBox_doc_number_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_add_doc_Click(object sender, EventArgs e)
        {
            show_doc_editor();
            button_doc_add_proceed.Visible = true;
            button_edit_doc.Enabled = false;
            button_del_doc.Enabled = false;
        }

        void show_doc_editor()
        {
            textBox_doc_number.Visible = true;
            dateTimePicker_doc.Visible = true;
            textBox_doc_note.Visible = true;
            label_doc_number.Visible = true;
            label_doc_date.Visible = true;
            label_doc_note.Visible = true;
        }

        void close_doc_editor()
        {
            textBox_doc_number.Visible = false;
            dateTimePicker_doc.Visible = false;
            textBox_doc_note.Visible = false;
        }

        private void button_doc_add_proceed_Click(object sender, EventArgs e)
        {
            string docNumberText = textBox_doc_number.Text;
            DateTime docDate = dateTimePicker_doc.Value;
            string docNote = textBox_doc_note.Text;

            if (int.TryParse(docNumberText, out int docNumber))
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SQLiteCommand checkDocCommand = new SQLiteCommand("SELECT COUNT(*) FROM Documents WHERE number = @number;", connection))
                            {
                                checkDocCommand.Parameters.AddWithValue("@number", docNumber);
                                long count = (long)checkDocCommand.ExecuteScalar();

                                if (count > 0)
                                {
                                    throw new Exception("Документ с таким номером уже существует.");
                                }
                            }

                            using (SQLiteCommand insertDocCommand = new SQLiteCommand("INSERT INTO Documents (number, date, total, note) VALUES (@number, @date, @total, @note);", connection))
                            {
                                insertDocCommand.Parameters.AddWithValue("@number", docNumber);
                                insertDocCommand.Parameters.AddWithValue("@date", docDate.ToString("dd-MM-yyyy"));
                                insertDocCommand.Parameters.AddWithValue("@total", 0);
                                insertDocCommand.Parameters.AddWithValue("@note", docNote);

                                insertDocCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show("Документ успешно добавлен.");

                            textBox_doc_number.Visible = false;
                            dateTimePicker_doc.Visible = false;
                            textBox_doc_note.Visible = false;
                            button_doc_add_proceed.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            using (SQLiteCommand logErrorCommand = new SQLiteCommand("INSERT INTO ErrorLogs (error_message, error_date) VALUES (@error_message, @error_date);", connection))
                            {
                                logErrorCommand.Parameters.AddWithValue("@error_message", ex.Message);
                                logErrorCommand.Parameters.AddWithValue("@error_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                logErrorCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Ошибка при добавлении документа: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите корректный номер документа.");
            }
            DbLoad();
            close_doc_editor();
            button_doc_add_proceed.Visible = false;
            button_edit_doc.Enabled = true;
            button_del_doc.Enabled = true;
        }

        private void button_edit_doc_Click(object sender, EventArgs e)
        {
            if (listBox_docs.Text != null)
            {
                show_doc_editor();
                button_doc_edit_proceed.Visible = true;
                button_add_doc.Enabled = false;
                button_del_doc.Enabled = false;
                if (listBox_docs.SelectedItem != null)
                {
                    int selectedDocNumber = Convert.ToInt32(listBox_docs.SelectedItem);

                    using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                    {
                        connection.Open();

                        using (SQLiteCommand command = new SQLiteCommand("SELECT number, date, note FROM Documents WHERE number = @number", connection))
                        {
                            command.Parameters.AddWithValue("@number", selectedDocNumber);

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    textBox_doc_number.Text = reader["number"].ToString();
                                    dateTimePicker_doc.Value = DateTime.Parse(reader["date"].ToString());
                                    textBox_doc_note.Text = reader["note"].ToString();

                                    textBox_doc_number.Enabled = true;
                                    dateTimePicker_doc.Enabled = true;
                                    textBox_doc_note.Enabled = true;
                                    button_doc_edit_proceed.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Документ не найден.");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете документ для редакции.");
            }
            
        }

        private void button_doc_edit_proceed_Click(object sender, EventArgs e)
        {
            int editedDocNumber = Convert.ToInt32(textBox_doc_number.Text);
            DateTime editedDate = dateTimePicker_doc.Value;
            string editedNote = textBox_doc_note.Text;

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Documents WHERE number = @number AND number != @oldNumber", connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@number", editedDocNumber);
                            checkCommand.Parameters.AddWithValue("@oldNumber", Convert.ToInt32(listBox_docs.SelectedItem));

                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Документ с таким номером уже существует.");
                                
                                using (SQLiteCommand logErrorCommand = new SQLiteCommand("INSERT INTO ErrorLogs (ErrorMessage, Date) VALUES (@errorMessage, @date)", connection, transaction))
                                {
                                    logErrorCommand.Parameters.AddWithValue("@errorMessage", "Попытка обновить документ с дублирующим номером.");
                                    logErrorCommand.Parameters.AddWithValue("@date", DateTime.Now);
                                    logErrorCommand.ExecuteNonQuery();
                                }

                                transaction.Rollback();
                                return;
                            }
                        }

                        using (SQLiteCommand updateCommand = new SQLiteCommand("UPDATE Documents SET number = @number, date = @date, note = @note WHERE number = @oldNumber", connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@number", editedDocNumber);
                            updateCommand.Parameters.AddWithValue("@date", editedDate.ToString("dd-MM-yyyy"));
                            updateCommand.Parameters.AddWithValue("@note", editedNote);
                            updateCommand.Parameters.AddWithValue("@oldNumber", Convert.ToInt32(listBox_docs.SelectedItem));

                            updateCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Документ успешно обновлен.");

                        textBox_doc_number.Clear();
                        dateTimePicker_doc.Value = DateTime.Now;
                        textBox_doc_note.Clear();

                        textBox_doc_number.Enabled = false;
                        dateTimePicker_doc.Enabled = false;
                        textBox_doc_note.Enabled = false;
                        button_doc_edit_proceed.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при обновлении документа: " + ex.Message);
                        transaction.Rollback();
                    }
                }
            }

            DbLoad();
            close_doc_editor();
            button_doc_edit_proceed.Visible = false;
            button_add_doc.Enabled = true;
            button_del_doc.Enabled = true;
        }

        private void dataGridView_docs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(listBox_docs.Text, out int number))
            {
                SpecLoad(number);
                showDocData(number);
            }
        }

        void showDocData(int number)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT number, date, note, total FROM Documents WHERE number = @number;", connection))
                {
                    command.Parameters.AddWithValue("@number", number);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string docNumber = reader["number"].ToString();
                            string docDate = reader["date"].ToString();
                            string docNote = reader["note"] as string;
                            string docAmount = reader["total"].ToString();

                            label_doc_number.Text = $"Number: {docNumber}";
                            label_doc_date.Text = $"Date: {docDate}";

                            if (!string.IsNullOrEmpty(docNote))
                            {
                                label_doc_note.Text = $"Note: {docNote}";
                            }
                            else
                            {
                                label_doc_note.Text = "Note: -";
                            }

                            label_doc_amount.Text = $"Amount: {docAmount}";
                        }
                        else
                        {
                            label_doc_number.Text = "Number: -";
                            label_doc_date.Text = "Date: -";
                            label_doc_note.Text = "Note: -";
                            label_doc_amount.Text = "Amount: -";
                        }
                    }
                }
            }
        }

        void showSpecData(string specName)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT name, total FROM Specifications WHERE name = @name;", connection))
                {
                    command.Parameters.AddWithValue("@name", specName);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string specNameFromDb = reader["name"].ToString();
                            string specAmount = reader["total"].ToString();

                            label_spec_name.Text = $"Name: {specNameFromDb}";
                            label_spec_amount.Text = $"Amount: {specAmount}";
                        }
                        else
                        {
                            label_spec_name.Text = "Name: -";
                            label_spec_amount.Text = "Amount: -";
                        }
                    }
                }
            }
        }



        private void button_del_doc_Click(object sender, EventArgs e)
        {
            if (listBox_docs.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы уверены, что хотите удалить документ с номером {listBox_docs.Text}?",
                                                    "Подтверждение удаления",
                                                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                    {
                        connection.Open();
                        DeleteSpecs(Convert.ToInt32(listBox_docs.Text));
                        listBox_specs.Items.Clear();
                        using (SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM Documents WHERE number = @number", connection))
                        {
                            int number = Convert.ToInt32(listBox_docs.Text);
                            deleteCommand.Parameters.AddWithValue("@number", number);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Документ удален успешно.");
                            }
                            else
                            {
                                MessageBox.Show("Что-то пошло не так.");
                            }
                        }
                    }

                    DbLoad();
                }
            }
        }

        private void button_del_spec_Click(object sender, EventArgs e)
        {
            if (listBox_docs.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы уверены, что хотите удалить спецификацию {listBox_specs.Text}?",
                                                    "Подтверждение удаления",
                                                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                    {
                        connection.Open();
                        using (SQLiteCommand deleteCommandspec = new SQLiteCommand("DELETE FROM Specifications WHERE name = @name", connection))
                        {
                            string name = Convert.ToString(listBox_specs.Text);
                            deleteCommandspec.Parameters.AddWithValue("@name", name);
                            int rowsAffected = deleteCommandspec.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Спецификация удалена успешна.");
                            }
                            else
                            {
                                MessageBox.Show("Что-то пошло не так.");
                            }
                        }
                    }

                    SpecLoad(Convert.ToInt32(listBox_docs.Text));
                    DbLoad();
                }
            }
            else
            {
                MessageBox.Show("Выберете спецификацию для удаления.");
            }
        }

        private void listBox_specs_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSpecData(listBox_specs.Text);
        }

        private void button_add_spec_Click(object sender, EventArgs e)
        {
            if (listBox_docs.SelectedItem != null)
            {
                textBox_spec_name.Visible = true;
                textBox_spec_amount.Visible = true;
                button_spec_add_proceed.Visible = true;
            }
            else
            {
                MessageBox.Show("Выберите документ перед добавлением спецификации.");
            }
        }

        private void button_spec_add_proceed_Click(object sender, EventArgs e)
        {
            if (listBox_docs.SelectedItem != null)
            {
                string specName = textBox_spec_name.Text;
                if (decimal.TryParse(textBox_spec_amount.Text, out decimal specAmount))
                {
                    int docNumber = Convert.ToInt32(listBox_docs.Text);
                    int docId = GetDocumentId(docNumber);

                    using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                    {
                        connection.Open();
                        using (SQLiteCommand insertSpec = new SQLiteCommand(
                            "INSERT INTO Specifications (docid, name, total) VALUES (@docid, @name, @total);", connection))
                        {
                            insertSpec.Parameters.AddWithValue("@docid", docId);
                            insertSpec.Parameters.AddWithValue("@name", specName);
                            insertSpec.Parameters.AddWithValue("@total", specAmount);

                            try
                            {
                                insertSpec.ExecuteNonQuery();
                                MessageBox.Show("Спецификация успешно добавлена.");

                                textBox_spec_name.Text = string.Empty;
                                textBox_spec_amount.Text = string.Empty;
                                textBox_spec_name.Visible = false;
                                textBox_spec_amount.Visible = false;
                                button_spec_add_proceed.Visible = false;

                                SpecLoad(docNumber);
                                DbLoad();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка добавления спецификации: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректную сумму.");
                }
            }
            else
            {
                MessageBox.Show("Выберите документ перед добавлением спецификации.");
            }
        }

        private int GetDocumentId(int docNumber)
        {
            int docId = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT id FROM Documents WHERE number = @number;", connection))
                {
                    command.Parameters.AddWithValue("@number", docNumber);
                    docId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return docId;
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            DbLoad();
        }

        private void button_edit_spec_Click(object sender, EventArgs e)
        {
            if (listBox_specs.SelectedItem != null)
            {
                string selectedSpecName = listBox_specs.Text;

                using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Specifications WHERE name = @name", connection))
                    {
                        command.Parameters.AddWithValue("@name", selectedSpecName);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox_spec_name.Text = reader["name"].ToString();
                                textBox_spec_amount.Text = reader["total"].ToString();
                            }
                        }
                    }
                }

                textBox_spec_name.Visible = true;
                textBox_spec_amount.Visible = true;
                button_specs_proceed.Visible = true;
            }
            else
            {
                MessageBox.Show("Выберите спецификацию для редактирования.");
            }
        }

        private void button_specs_proceed_Click(object sender, EventArgs e)
        {
            if (listBox_docs.SelectedItem != null && listBox_specs.SelectedItem != null)
            {
                string selectedSpecName = listBox_specs.Text;
                string newSpecName = textBox_spec_name.Text;
                double newSpecAmount;

                if (!double.TryParse(textBox_spec_amount.Text, out newSpecAmount))
                {
                    MessageBox.Show("Введите корректное значение для суммы.");
                    return;
                }

                using (SQLiteConnection connection = new SQLiteConnection("Data Source=docs.db;Version=3;"))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand("UPDATE Specifications SET name = @newName, total = @newAmount WHERE name = @oldName", connection))
                    {
                        command.Parameters.AddWithValue("@newName", newSpecName);
                        command.Parameters.AddWithValue("@newAmount", newSpecAmount);
                        command.Parameters.AddWithValue("@oldName", selectedSpecName);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Спецификация успешно обновлена.");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении спецификации.");
                        }
                    }
                }

                SpecLoad(Convert.ToInt32(listBox_docs.Text));
                DbLoad();
                textBox_spec_name.Visible = false;
                textBox_spec_amount.Visible = false;
                button_specs_proceed.Visible = false;
            }
            else
            {
                MessageBox.Show("Документ или спецификация не выбраны.");
            }
        }
    }
}
