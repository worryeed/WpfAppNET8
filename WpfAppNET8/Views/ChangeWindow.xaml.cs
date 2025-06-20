using System.Windows;
using WpfAppNET8.Data;
using WpfAppNET8.Models;

namespace WpfAppNET8.Views;

public partial class ChangeWindow : Window
{
    private Material _material;

    public ChangeWindow(Material material)
    {
        InitializeComponent();
        _material = material;
        SetData();
    }

    private void SetData()
    {
        DemoContext demoContext = new DemoContext();

        NameMaterial.Text = _material.НаименованиеМатериала;

        foreach (var type in demoContext.MaterialTypes)
        {
            TypeMaterial.Items.Add(type.ТипМатериала);
        }

        TypeMaterial.SelectedItem = _material.ТипМатериала;

        CurrentCount.Text = _material.КоличествоНаСкладе.ToString();
        Unit.Text = _material.ЕдиницаИзмерения.ToString();
        CountInPack.Text = _material.КоличествоВУпаковке.ToString();
        MinCount.Text = _material.МинимальноеКоличество.ToString();
        PriceOfOne.Text = _material.ЦенаЕдиницыМатериала.ToString();
    }

    private void Change_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DemoContext demoContext = new DemoContext();

            if (!int.TryParse(CurrentCount.Text, out int currentCount))
            {
                MessageBox.Show("Введите корректное количество на складе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CountInPack.Text, out int countInPack))
            {
                MessageBox.Show("Введите корректное количество в упаковке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(MinCount.Text, out int minCount))
            {
                MessageBox.Show("Введите корректное минимальное количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(PriceOfOne.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            demoContext.Materials.Update(new Material
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
            MessageBox.Show("Ошибка при изменении материала. Проверьте введенные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
