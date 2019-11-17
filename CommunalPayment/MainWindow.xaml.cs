using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommunalPayment
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/*
		Используя знания о групповых элементов управления сделайте программу, которая представляет собой выбор и оплату услуг ЖКХ. 

		В приложении нужно учитывать 
			ИИН человека, 
			улицу, 
			дом и квартиру, 
			номер телефона и 
			идентификатор услуги (вода/свет и так далее).
		Всю информацию сохраняйте в БД
		*/
		public MainWindow()
		{
			InitializeComponent();
		}

		private void payButtonClick(object sender, RoutedEventArgs e)
		{
			string type = ChoosenType();
			if (iinTB.Text == "" || streetTB.Text == "" || houseNumTB.Text == "" || phoneNumTB.Text == "")
			{
				MessageBox.Show("Необходимо заполнить все поля");
				return;
			}

			else if (type == "Не выбрано")
			{
				MessageBox.Show("Выберите оплачиваемую услугу");
				return;
			}
			else if(StringToInt(sumTB.Text) == 0)
			{
				MessageBox.Show("Введена неверная сумма (Нужно вводить только цифры)");
				return;
			}
			else if (StringToInt(flatNumTB.Text) == 0)
			{
				MessageBox.Show("Введен неверный номер квартиры (Нужно вводить только цифры)");
				return;
			}

			Bill bill = new Bill
			{
				IIN = iinTB.Text,
				Street = streetTB.Text,
				HouseNum = houseNumTB.Text,
				FlatNum = StringToInt(flatNumTB.Text), // Если у человека частный дом то думаю человек поймет что не нужно заполнять это поле
				PhoneNum = phoneNumTB.Text,
				Sum = StringToInt(sumTB.Text),
				Type = type
			};

			using (var context = new Context("Server=BorisHOME\\Boris;Database=CommunalPayment;Trusted_Connection=True;"))
			{
				context.Bills.Add(bill);
				context.SaveChanges();
			}
			MessageBox.Show("Успешно оплачено");
			this.Close();
		}

		private int StringToInt(string str)
		{
			try
			{
				int num = Int32.Parse(str);
				return num;
			}
			catch
			{
				return 0;
			}
		}

		private string ChoosenType()
		{
			if (waterRB.IsChecked == true) { return "Вода"; }
			else if (lightRB.IsChecked == true) { return "Электричество"; }
			else if (trashRB.IsChecked == true) { return "Вывоз мусора"; }
			else { return "Не выбрано"; }
		}
	}
}
