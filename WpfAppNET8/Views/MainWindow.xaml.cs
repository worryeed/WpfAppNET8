using System.Collections.ObjectModel;
using System.Windows;
using WpfAppNET8.Data;
using WpfAppNET8.Models;

namespace WpfAppNET8.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LoadData();
        MyListView.SelectionChanged += MyListView_SelectionChanged;
    }

    private void LoadData()
    {
        DemoContext demoContext = new DemoContext();

        foreach (var material in demoContext.Materials)
        {
            var price = GetPrice(material);

            var materialToAdd = new DataToView()
            {
                TypeWithName = $"{material.ТипМатериала} | {material.НаименованиеМатериала}",
                MinCount = $"Минимальное количество: {material.МинимальноеКоличество} {material.ЕдиницаИзмерения}",
                CurrentCount = $"Количество на складе: {material.КоличествоНаСкладе} {material.ЕдиницаИзмерения}",
                PriceWithUnit = $"Цена: {material.ЦенаЕдиницыМатериала:F2} р / Единица измерения: {material.ЕдиницаИзмерения}",
                PriceOfBatch = $"Стоимость партии: {price:F2} р"
            };

            MyListView.Items.Add(materialToAdd);
        }
    }

    private decimal GetPrice(Material material)
    {
        var countToBuyWithoutCeiling = material.МинимальноеКоличество - material.КоличествоНаСкладе;
        var countToBuy = Math.Ceiling(countToBuyWithoutCeiling / (decimal)material.КоличествоВУпаковке) * material.КоличествоВУпаковке;
        var price = countToBuy * material.ЦенаЕдиницыМатериала;

        if (price < 0)
        {
            price = 0;
        }

        return price;
    }

    private void MyListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        DemoContext demoContext = new DemoContext();
        var item = (MyListView.SelectedItem as DataToView)!;
        var name = item.TypeWithName.Split(" | ")[1];
        var material = demoContext.Materials.First(m => m.НаименованиеМатериала == name);

        var changeWindow = new ChangeWindow(material);
        changeWindow.Show();
        this.Close();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var addWindow = new AddWindow();
        addWindow.Show();
        this.Close();
    }
}