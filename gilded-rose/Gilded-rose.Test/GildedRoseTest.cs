using System.Collections.Generic;
using NUnit.Framework;

namespace Gilded_rose.Test
{
    public class GildedRoseTest
    {
        [Test]
        public void Regular_Item_Decrease_Quality_By_One()
        {
            var item = new Item("Regular", 10, 20);
            var gildedRose = new GildedRose( new List<Item> { item });
            
            gildedRose.UpdateQuality();
            
            Assert.That(item.Name, Is.EqualTo("Regular"));
            Assert.That(item.SellIn, Is.EqualTo(9));
            Assert.That(item.Quality, Is.EqualTo(19));
        }

    }
}