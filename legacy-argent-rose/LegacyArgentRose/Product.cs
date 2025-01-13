namespace ArgentRose;

public class Product
{
    private const int MinimumQuality = 0;
    private const int MaximumQuality = 50;
    
    private readonly string _description;
    private int _sellIn;
    private int _quality;

    public Product(string description, int sellIn, int quality)
    {
        _description = description;
        _sellIn = sellIn;
        _quality = quality;
    }

    public void DecreaseQualityBy(int decrement)
    {
        for (var i = 0; i < decrement; i++)
        {
            if (_quality > MinimumQuality)
            {
                _quality -= 1;
            }
            
            if (_sellIn < 0)
            {
                if (_quality > MinimumQuality)
                {
                    _quality -= 1;
                }
            }
        }
    }

    public void IncreaseQualityBy(int increment)
    {
        for (var i = 0; i < increment; i++)
        {
            if (_quality < MaximumQuality)
            {
                _quality += 1;   
            }
            
            if (_sellIn < 0)
            {
                if (_quality < MaximumQuality)
                {
                    _quality += 1;   
                }
            }
        }
    }

    public void DropQualityToMinimum()
    {
        _quality = MinimumQuality;
    }

    public void DecreaseSellIn()
    {
        _sellIn -= 1;
    }

    public bool IsLanzaroteWine()
    {
        return _description == "Lanzarote Wine";
    }

    public bool IsTheatrePasses()
    {
        return _description == "Theatre Passes";
    }

    public string Description => _description;

    public int SellIn => _sellIn;

    public int Quality => _quality;

    protected bool Equals(Product other)
    {
        return _description == other._description && _sellIn == other._sellIn && _quality == other._quality;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_description, _sellIn, _quality);
    }

    public override string ToString()
    {
        return
            $"{nameof(Description)}: {Description}, {nameof(SellIn)}: {SellIn}, {nameof(Quality)}: {Quality}";
    }
}