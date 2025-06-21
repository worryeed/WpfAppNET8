using System.Windows;
using WpfAppNET8.Data;
using WpfAppNET8.Models;

namespace WpfAppNET8.Views;

public partial class AddWindow : Window
{
    public AddWindow()
    {
        InitializeComponent();
        SetData();
    }

    private void SetData()
    {
        DemoContext demoContext = new DemoContext();

        foreach (var type in demoContext.MaterialTypes)
        {
            TypeMaterial.Items.Add(type.ТипМатериала);
        }

        TypeMaterial.SelectedItem = TypeMaterial.SelectedIndex = 0;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DemoContext demoContext = new DemoContext();

            if (demoContext.Materials.Any(m => m.НаименованиеМатериала == NameMaterial.Text))
            {
                MessageBox.Show("Материал с таким названием уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CurrentCount.Text, out int currentCount) && currentCount < 0)
            {
                MessageBox.Show("Введите корректное количество на складе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CountInPack.Text, out int countInPack) && currentCount < 0)
            {
                MessageBox.Show("Введите корректное количество в упаковке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(MinCount.Text, out int minCount) && currentCount < 0)
            {
                MessageBox.Show("Введите корректное минимальное количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(PriceOfOne.Text, out decimal price) && currentCount < 0)
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            demoContext.Materials.Add(new Material
            {
                НаименованиеМатериала = NameMaterial.Text,
                ТипМатериала = TypeMaterial.SelectedItem?.ToString() ?? string.Empty,
                КоличествоНаСкладе = currentCount,
                ЕдиницаИзмерения = Unit.Text,
                КоличествоВУпаковке = countInPack,
                МинимальноеКоличество = minCount,
                ЦенаЕдиницыМатериала = price
            });

            demoContext.SaveChanges();
            OpenMainWindow();
        }
        catch
        {
            MessageBox.Show("Ошибка при добавлении материала. Проверьте введенные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            OpenMainWindow();
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        OpenMainWindow();
    }

    private void OpenMainWindow()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}
