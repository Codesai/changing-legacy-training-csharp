using NUnit.Framework;
using static ArgentRose.Tests.ArgentRoseStoreForTesting;

namespace ArgentRose.Tests;

public class ArgentRoseStoreTest
{
    [Test]
    public void Regular_Product_Decreases_Quality_By_Two()
    {
        var initialInventory = new List<Product> { new("RegularProduct", 1, 10) };
        var store = new ArgentRoseStoreForTesting(initialInventory);

        store.Update();

        var expectedInventory = new List<Product> { new("RegularProduct", 0, 8) };
        Assert.That(store.SavedInventory, Is.EqualTo(expectedInventory));
    }
}

public class ArgentRoseStoreForTesting : ArgentRoseStore
{
    private List<Product> _savedInventory;
    private readonly List<Product> _initialInventory;

    public ArgentRoseStoreForTesting(List<Product> initialInventory)
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

    public List<Product> SavedInventory => _savedInventory;
}