using NUnit.Framework;
using static ArgentRose.Tests.ArgentRoseStoreForTesting;

namespace ArgentRose.Tests;

public class ArgentRoseStoreTest
{
    [Test]
    public void Expired_Theatre_Passes_Drop_Quality_To_Zero()
    {
        var store = StoreIncluding(TheatrePasses(0, 5));

        store.Update();

        Assert.That(
            store.SavedInventory,
            Is.EqualTo(InventoryIncluding(TheatrePasses(-1, 0))));
    }

    private static Product TheatrePasses(int sellIn, int quality)
    {
        return new Product("Theatre Passes", sellIn, quality);
    }

    private static List<Product> InventoryIncluding(params Product[] products)
    {
        return products.ToList();
    }
}

public class ArgentRoseStoreForTesting : ArgentRoseStore
{
    private List<Product> _savedInventory;
    private readonly List<Product> _initialInventory;

    public static ArgentRoseStoreForTesting StoreIncluding(params Product[] products)
    {
        return new ArgentRoseStoreForTesting(products.ToList());
    }
    
    public List<Product> SavedInventory => _savedInventory;

    private ArgentRoseStoreForTesting(List<Product> initialInventory)
    {
        _savedInventory = new List<Product>();
        _initialInventory = initialInventory;
    }

    protected override List<Product> GetInventoryFromDb()
    {
        return _initialInventory;
    }

    protected override void SaveInventory(List<Product> inventory)
    {
        _savedInventory = inventory;
    }
}